using System;
using System.ComponentModel;
using LocalizationLibrary;

namespace EnumEditor
{
    public class EnumData : INotifyPropertyChanged
    {
        public string TranslatePrefix { get; set; }

        public EnumData()
        {

        }

        public EnumData(string enumType)
        {
            EnumType = enumType;
        }

        public override string ToString()
        {
            var section = TranslatePrefix;
            if (!string.IsNullOrEmpty(section)) section += "/";
            section += EnumType;

            return Lang.GetTitle(section);
        }

        public string EnumType { get; set; }
        public object Value { get; set; }

        private bool _isChecked;
        public bool IsChecked
        {
            get { return _isChecked; }
            set
            {
                _isChecked = value;

                InvokePropertyChanged("IsChecked");
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
