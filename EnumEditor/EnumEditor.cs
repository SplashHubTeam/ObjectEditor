using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace EnumEditor
{
    /// <summary>
    /// 
	/// Класс для редактирования типов enum и flags.
    ///
    /// </summary>
    public class EnumEditor : Control, INotifyPropertyChanged
    {
        public static DependencyProperty ValueProperty;
        public static DependencyProperty TranslatePrefixProperty;
		public static DependencyProperty IsReadOnlyProperty;
		public static DependencyProperty TemplatesProperty;

        public object Value
        {
            get { return GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public object EditValue { get; set; }
		public bool IsReadOnly
		{
			get { return (bool) GetValue(IsReadOnlyProperty); }
			set { SetValue(IsReadOnlyProperty, value); }
		}

        private bool _isSkip;

        public string TranslatePrefix
        {
            get { return (string) GetValue(TranslatePrefixProperty); }
            set { SetValue(TranslatePrefixProperty, value); }
        }

		/// <summary>
		/// Список шаблонов, которые необходимо использовать, чтобы в холостую не производить поиск ресурсов,
		/// что занимет значительное время, т.к. просматривается все дерево.
		/// Можно задавать как строку, раздели шаблоны запятой.
		/// </summary>
		[TypeConverter(typeof(StringToListConverter))]
		public List<string> Templates
		{
			get { return (List<string>)GetValue(TemplatesProperty); }
			set { SetValue(TemplatesProperty, value); }
		}

		public DataTemplateSelector TemplateSelector { get; private set; }

		/// <summary>
		/// 
		/// </summary>
		//public DataTemplateSelector ReadOnlySelector { get; private set; }

        static EnumEditor()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(EnumEditor), new FrameworkPropertyMetadata(typeof(EnumEditor)));

			ValueProperty = DependencyProperty.Register("Value", typeof(object), typeof(EnumEditor), new FrameworkPropertyMetadata(null, ValuePropertyChanged) { BindsTwoWayByDefault = true});
            TranslatePrefixProperty = DependencyProperty.Register("TranslatePrefix", typeof(string), typeof(EnumEditor), new PropertyMetadata(null, TranslatePrefixPropertyChanged));
			IsReadOnlyProperty = DependencyProperty.Register("IsReadOnly", typeof(bool), typeof(EnumEditor), new PropertyMetadata(false));
			TemplatesProperty = DependencyProperty.Register("Templates", typeof(List<string>), typeof(EnumEditor), new PropertyMetadata(null));
        }

        private static void TranslatePrefixPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as EnumEditor;

            if (control == null || control.EditValue == null) return;

            var value = (BaseEnumValue) control.EditValue;
            value.TranslatePrefix = control.GetTranslatePrefix(control.Value);
        }

        public EnumEditor()
        {
            _isSkip = false;

			TemplateSelector = new TemplateSelector(this);
        }

        private static void ValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as EnumEditor;

            if (control == null || control._isSkip) return;

            if (e.NewValue == null)
            {
                control.EditValue = null;

                return;
            }

            var enumType = e.NewValue.GetType();
//            control._valueType = enumType;

            if (enumType.BaseType != typeof(Enum))
            {
                control.EditValue = null;

                return;
            }

            var isFlag = false;

            var attr = Attribute.GetCustomAttributes(enumType);
            foreach (var attribute in attr)
            {
                if (attribute is FlagsAttribute)
                {
                    isFlag = true;
                    break;
                }
            }

            var newValue = (Enum)e.NewValue;

            //control.IsFlagEnum = isFlag;

            var translatePrefix = control.GetTranslatePrefix(e.NewValue);

            BaseEnumValue editValue = null;

			if (isFlag) editValue = new FlagsValue(newValue);
            else editValue = new EnumValue(newValue);

            editValue.TranslatePrefix = translatePrefix;

			editValue.ObjectChanged += control.ObjectChanged;

            control.EditValue = editValue;

            control.InvokePropertyChanged("EditValue");
        }

		private void ObjectChanged(object obj)
        {
            _isSkip = true;
            var newValue = Enum.ToObject(Value.GetType(), obj);

            SetValue(ValueProperty, newValue);

            _isSkip = false;
        }

        public string GetTranslatePrefix(object value)
        {
            var translatePrefix = TranslatePrefix;
            if (!string.IsNullOrEmpty(translatePrefix)) translatePrefix += "/";
            if (value != null) translatePrefix += value.GetType().Name;

            return translatePrefix;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void InvokePropertyChanged(string name)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(name));
        }
    }
}
