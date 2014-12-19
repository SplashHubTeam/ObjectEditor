using System.Windows;
using System.Windows.Controls;

namespace EnumEditor
{
    public class TemplateSelector : DataTemplateSelector
    {
        private readonly EnumEditor _control;

        public TemplateSelector(EnumEditor control)
        {
            _control = control;
        }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item == null) return null;

            var key = item.GetType().Name;

            var resources = _control.Resources;
            var templateResources = _control.Template.Resources;

            if (_control.IsReadOnly) key += "ReadOnly";

            var template = resources[key];

            if (template == null)
            {
                if (_control.Templates != null && _control.Templates.Contains(key))
                {
                    try
                    {
                        var control = (FrameworkElement)(_control.TemplatedParent ?? _control.Parent);
                        if (control != null) template = control.FindResource(key);
                    }
                    catch (ResourceReferenceKeyNotFoundException)
                    {
                        System.Diagnostics.Trace.WriteLine(string.Format("EnumEditor: Template {0} not found.", key));
                    }
                    catch { }
                }
            }

            return (DataTemplate)(template ?? templateResources[key]);
        }
    }
}
