using System;
using System.Collections.Generic;

namespace EnumEditor
{
    public class FlagsValue : BaseEnumValue
    {
        private bool _isSkip;
        public List<EnumData> Value { get; set; }

        public FlagsValue(object value)
        {
            _isSkip = false;
            Object = value;

            UpdateList();
        }

        private void UpdateList()
        {
            var enumType = Object.GetType();

            if (enumType.BaseType != typeof(Enum))
            {
                if (Items != null) Items.Clear();
                if (Value.Count > 0) Value.Clear();

                return;
            }

            var list = new List<EnumData>();
            Value = new List<EnumData>();

            var newValue = (Enum)Object;

            var names = Enum.GetNames(enumType);
            var values = Enum.GetValues(enumType);
            for (var index = 0; index < names.Length; index++)
            {
                var name = names[index];
                var curData = new EnumData(name) { Value = (int)values.GetValue(index), TranslatePrefix = TranslatePrefix };

                var curFlag = (Enum)Enum.Parse(enumType, name);
                var isNull = (int)Convert.ChangeType(curFlag, typeof(int)) == 0;

                if (!isNull && newValue.HasFlag(curFlag) || newValue.Equals(curFlag))
                {
                    curData.IsChecked = true;

                    Value.Add(curData);
                }

                curData.PropertyChanged += CurDataPropertyChanged;

                list.Add(curData);
            }

            Items = list;

            InvokePropertyChanged("Value");
        }

        void CurDataPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("IsChecked"))
            {
                var value = sender as EnumData;
                if (value != null)
                {
                    FlagChanged(value.IsChecked, value.Value);
                }
            }
        }

        public void FlagChanged(bool isActive, object objectValue)
        {
            if (_isSkip) return;

            try
            {
                _isSkip = true;

                var res = 0;

                var value = (int)objectValue;

                if (value != 0)
                {
                    res = (int)Object;
                    if (isActive) res |= value;
                    else res &= ~value;
                }

                var newValue = Enum.ToObject(Object.GetType(), res);

                Object = newValue;

                if (ObjectChanged != null) ObjectChanged(Object);

                Value = new List<EnumData>();

                foreach (var item in Items)
                {
                    var checkFlag = ((res & (int)item.Value) != 0);

                    if (res == 0 && (int)item.Value == 0)
                    {
                        if (!item.IsChecked) item.IsChecked = true;
                        Value.Add(item);
                    }
                    else if (!item.IsChecked.Equals(checkFlag)) item.IsChecked = checkFlag;

                    if (checkFlag) Value.Add(item);
                }

                _isSkip = false;

                InvokePropertyChanged("Value");
            }
            catch { }
        }
    }
}
