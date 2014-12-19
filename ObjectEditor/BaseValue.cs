using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows.Controls;
using LocalizationLibrary;

namespace ObjectEditor
{
    /// <summary>
    /// Базовый класс для редактирования свойства объекта.
    /// </summary>
    public class BaseValue : AbstractValue, INotifyPropertyChanged
    {
        protected object Object;
        protected PropertyInfo Property;

        public string TranslatePrefix { get; set; }
        public override PropertyInfo Info { get { return Property; } }
        public override Type Type { get { return Property.PropertyType; } }

        public override object Value
        {
            get
            {
                return Property.GetValue(Object, null);
            }
            set
            {
                //var a = Property.GetAccessors(false);
                Property.SetValue(Object, value, null);
            }
        }

        public List<string> Attributes
        {
            get
            {
                var res = Attribute.GetCustomAttributes(Property).Select(i => i.GetType().Name).ToList();
                return res;
            }
        }

        public ValidationRule Validator
        {
            get
            {
                if (Property == null) return null;

                var validator = Attribute.GetCustomAttribute(Property, typeof(ValidatorAttribute)) as ValidatorAttribute;

                return validator == null ? null : validator.Rule;
            }
        }

        public BaseValue() { }

        public BaseValue(object Object, PropertyInfo property)
        {
            this.Object = Object;
            Property = property;
        }

        public override string ToString()
        {
            var section = TranslatePrefix;
            if (!string.IsNullOrEmpty(section)) section += "/";
            section += Property.Name;

            return Lang.GetTitle(section);
        }

        // override object.Equals
        public override bool Equals(object obj)
        {
            if (obj == null || (GetType() != obj.GetType() && GetType().BaseType != obj.GetType()))
            {
                return false;
            }

            var compareObj = obj as BaseValue;
            return compareObj != null && Property == compareObj.Property;
        }

        public virtual void UpdateValue(object Object)
        {
            this.Object = Object;

            InvokePropertyChanged("Value");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void InvokePropertyChanged(string name)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(name));
        }
    }
}
