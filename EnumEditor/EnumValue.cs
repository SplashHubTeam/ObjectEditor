using System;
using System.Collections.Generic;

namespace EnumEditor
{
    public class EnumValue : BaseEnumValue
    {
        private EnumData _value;
        public EnumData Value
        {
            get { return _value; }
            set
            {
                _value = value;

                var newValue = Enum.ToObject(Object.GetType(), value.Value);
                if (!Object.Equals(newValue))
                {
                    Object = newValue;

                    if (ObjectChanged != null) ObjectChanged(Object);
                }
            }
        }

        public EnumValue(object Object)
        {
            this.Object = Object;

            UpdateList();
        }

        private void UpdateList()
        {
            var enumType = Object.GetType();

            if (enumType.BaseType != typeof(Enum))
            {
                if (Items != null) Items.Clear();

                return;
            }

            var list = new List<EnumData>();

            var names = Enum.GetNames(enumType);
            var values = Enum.GetValues(enumType);

            for (var index = 0; index < names.Length; index++)
            {
                var name = names[index];
                var curData = new EnumData(name) { Value = values.GetValue(index), TranslatePrefix = TranslatePrefix };

                if (Object.Equals(curData.Value))
                {
                    Value = curData;
                }

                list.Add(curData);
            }

            Items = list;
        }
    }
}
