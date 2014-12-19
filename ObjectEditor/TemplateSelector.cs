using System;
using System.Windows;
using System.Windows.Controls;

namespace ObjectEditor
{
    public class TemplateSelector : DataTemplateSelector
    {
        private readonly ObjectEditor _control;

        public TemplateSelector(ObjectEditor control)
        {
            _control = control;
        }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            string key;
            var isReadOnly = false;
            var editItem = item as AbstractValue;

            if (editItem != null)
            {
                if (editItem.Type.Name != "String" && editItem.Type.GetInterface("IEnumerable") != null)
                {
                    key = "Collection";
                }
                else key = editItem.Type.BaseType == typeof(Enum) ? "Enum" : editItem.Type.Name;

                if (editItem.Info != null && !editItem.Info.CanWrite)
                {
                    isReadOnly = true;
                    key += "ReadOnly";
                }
            }
            else return null;

            var resources = _control.Resources;
            var templateResources = _control.Template.Resources;

            if (!isReadOnly)
            {
                if (_control.IsReadOnly) key += "ReadOnly";
            }

            var template = resources[key];
            if (template == null)
            {
                if (_control.Templates != null && _control.Templates.Contains(key))
                {
                    try
                    {
                        var control = (FrameworkElement)(_control.TemplatedParent ?? _control.Parent);
                        if (control != null)
                        {
                            template = control.FindResource(key);
                        }
                    }
                    catch (ResourceReferenceKeyNotFoundException)
                    {
                        System.Diagnostics.Trace.WriteLine(string.Format("ObjectEditor: Template {0} not found.", key));
                    }
                    catch { }
                }

                if (template == null) template = templateResources[key];
            }

            return (DataTemplate)(template ?? templateResources["DefaultTemplate"]);
        }
    }
}
