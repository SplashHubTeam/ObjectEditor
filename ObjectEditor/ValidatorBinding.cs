using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;

namespace ObjectEditor
{
    //Расширение для Binding, позволяющее автоматически использовать валидатор, установленный для поля класса.
    public class ValidatorBinding : MarkupExtension
    {
        public string Path { get; set; }
        public IValueConverter Converter { get; set; }
        public ValidationRule ValidatorRule { get; set; }
        public UpdateSourceTrigger UpdateSourceTrigger { get; set; }

        public ValidatorBinding()
        {
            Path = "";
        }

        public ValidatorBinding(string path)
        {
            Path = path;
        }

        private Binding _binding;

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            var p = (IProvideValueTarget)serviceProvider;

            if (!(p.TargetObject is DependencyObject))
            {
                return this;
            }

            var element = (FrameworkElement)p.TargetObject;
            var property = (DependencyProperty)p.TargetProperty;

            if (element != null && property != null)
            {
                _binding = new Binding(Path) { Converter = Converter, UpdateSourceTrigger = UpdateSourceTrigger };

                UpdateContext(element);

                element.DataContextChanged += ElementOnDataContextChanged;

                return _binding.ProvideValue(serviceProvider);
            }

            return this;
        }

        private void UpdateContext(FrameworkElement element)
        {
            if (element == null || element.DataContext == null) return;

            var baseValue = element.DataContext as BaseValue;
            var validator = baseValue == null ? null : baseValue.Validator;

            _binding.ValidationRules.Clear();

            if (validator != null)
            {
                _binding.ValidationRules.Add(validator);
            }
            else if (ValidatorRule != null)
            {
                _binding.ValidationRules.Add(ValidatorRule);
            }
        }

        private void ElementOnDataContextChanged(object sender, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            UpdateContext(sender as FrameworkElement);
        }
    }
}
