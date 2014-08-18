using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace EnumEditor
{
    public class BaseEnumValue : INotifyPropertyChanged
    {
	    protected object Object;

        public Action<object> ObjectChanged;
/*
        private object _value;
        public object Value
        {
            get { return _value; }
            set
            {
                _value = value;
                InvokePropertyChanged("Value");
            }
        }
*/
        private string _translatePrefix;
        public string TranslatePrefix
        {
            get { return _translatePrefix; }
            set
            {
                _translatePrefix = value;

                foreach (var enumData in Items)
                {
                    enumData.TranslatePrefix = TranslatePrefix;
                }
            }
        }

	    private List<EnumData> _items;
        public List<EnumData> Items
        {
            get { return _items; }
            set
            {
                _items = value;
                InvokePropertyChanged("Items");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void InvokePropertyChanged(string name)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(name));
        }
    }
}
