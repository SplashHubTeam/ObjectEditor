using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using EnumEditor;

namespace ObjectEditor
{
    /// <summary>
    /// 
    /// Класс для редактирования объекта.
    ///
    /// </summary>
    public class ObjectEditor : Control
    {
        public static DependencyProperty ValueProperty;
        public static DependencyProperty TranslatePrefixProperty;
        public static DependencyProperty ShowAttributesProperty;
        public static DependencyProperty OrientationProperty;
        public static DependencyProperty ChildOrientationProperty;
        public static DependencyProperty IsDescriptionVisibleProperty;
        public static DependencyProperty DescriptionMarginProperty;
        public static DependencyProperty ChildMarginProperty;
        public static DependencyProperty TemplatesProperty;
        public static DependencyProperty IsReadOnlyProperty;

        /// <summary>
        /// Редактируемый объект.
        /// </summary>
        public object Value
        {
            get { return GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        /// <summary>
        /// Префикс для перевода полей объекта.
        /// Перевод для поля объекта должен содержаться в секции TranslatePrefix + Имя класса объекта + Названия свойства объекта.
        /// В шаблоне по-умолчанию TranslatePrefix передается дальше в EnumEditor без изменений.
        /// </summary>
        public string TranslatePrefix
        {
            get { return (string)GetValue(TranslatePrefixProperty); }
            set { SetValue(TranslatePrefixProperty, value); }
        }

        /// <summary>
        /// Список полей объекта.
        /// </summary>
        public ObservableCollection<BaseValue> Items { get; set; }

        /// <summary>
        /// Список атрибутов поля объекта, которые будут редактируемы (попадут в Items).
        /// </summary>
        [TypeConverter(typeof(StringToListConverter))]
        public List<string> ShowAttributes
        {
            get { return (List<string>)GetValue(ShowAttributesProperty); }
            set { SetValue(ShowAttributesProperty, value); }
        }

        /// <summary>
        /// Ориентация элементов непосредственно внутри ObjectEditor.
        /// </summary>
        public Orientation Orientation
        {
            get { return (Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        /// <summary>
        /// Ориентация остальных элементов.
        /// </summary>
        public Orientation ChildOrientation
        {
            get { return (Orientation)GetValue(ChildOrientationProperty); }
            set { SetValue(ChildOrientationProperty, value); }
        }

        /// <summary>
        /// Флаг видимости текста описания поля.
        /// </summary>
        public bool IsDescriptionVisible
        {
            get { return (bool)GetValue(IsDescriptionVisibleProperty); }
            set { SetValue(IsDescriptionVisibleProperty, value); }
        }

        /// <summary>
        /// Отступы для описания элемента
        /// </summary>
        public Thickness DescriptionMargin
        {
            get { return (Thickness)GetValue(DescriptionMarginProperty); }
            set { SetValue(DescriptionMarginProperty, value); }
        }

        /// <summary>
        /// Отступы для редактора значения элемента
        /// </summary>
        public Thickness ChildMargin
        {
            get { return (Thickness)GetValue(ChildMarginProperty); }
            set { SetValue(ChildMarginProperty, value); }
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

        /// <summary>
        /// Для выбора шаблона по типу элемента. У шаблона необходимо будет указать в качестве ключа название типа.
        /// Выбор шаблона не подходит для коллекции.
        /// </summary>
        public DataTemplateSelector TemplateSelector { get; private set; }

        /// <summary>
        /// Флаг только для отображения (будут выбраны шаблоны только для отображения).
        /// </summary>
        //public DataTemplateSelector ReadOnlySelector { get; private set; }

        private bool _isRoot;

        public bool IsReadOnly
        {
            get { return (bool)GetValue(IsReadOnlyProperty); }
            set { SetValue(IsReadOnlyProperty, value); }
        }

        static ObjectEditor()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ObjectEditor), new FrameworkPropertyMetadata(typeof(ObjectEditor)));

            ValueProperty = DependencyProperty.Register("Value", typeof(object), typeof(ObjectEditor), new PropertyMetadata(null, ValuePropertyChanged));
            TranslatePrefixProperty = DependencyProperty.Register("TranslatePrefix", typeof(string), typeof(ObjectEditor), new PropertyMetadata(null, UpdateTranslatePrefixProperty));
            ShowAttributesProperty = DependencyProperty.Register("ShowAttributes", typeof(List<string>), typeof(ObjectEditor), new PropertyMetadata(new List<string> { "ShowInView" }));
            OrientationProperty = DependencyProperty.Register("Orientation", typeof(Orientation), typeof(ObjectEditor), new PropertyMetadata(Orientation.Vertical));
            ChildOrientationProperty = DependencyProperty.Register("ChildOrientation", typeof(Orientation), typeof(ObjectEditor), new PropertyMetadata(Orientation.Vertical));
            IsDescriptionVisibleProperty = DependencyProperty.Register("IsDescriptionVisible", typeof(bool), typeof(ObjectEditor), new PropertyMetadata(true));
            DescriptionMarginProperty = DependencyProperty.Register("DescriptionMargin", typeof(Thickness), typeof(ObjectEditor), new PropertyMetadata(new Thickness(5, 5, 5, 5)));
            ChildMarginProperty = DependencyProperty.Register("ChildMargin", typeof(Thickness), typeof(ObjectEditor), new PropertyMetadata(new Thickness(20, 0, 10, 0)));
            TemplatesProperty = DependencyProperty.Register("Templates", typeof(List<string>), typeof(ObjectEditor), new PropertyMetadata(null));
            IsReadOnlyProperty = DependencyProperty.Register("IsReadOnly", typeof(bool), typeof(ObjectEditor), new PropertyMetadata(true));
        }

        private static void ValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as ObjectEditor;

            if (control == null) return;

            //! Если новый объект отличен по типу от предыдущего - перезаполнить данные.
            if (control.Items.Count > 0 && (e.NewValue == null || e.OldValue == null || e.NewValue.GetType() != e.OldValue.GetType()))
            {
                control.Items.Clear();
            }

            var newBaseValue = e.NewValue as AbstractValue;
            //проверка на корневой узел
            if (!control._isRoot)
            {
                var oldValue = e.OldValue as AbstractValue;

                control._isRoot = oldValue == null || oldValue.Parent == null || newBaseValue == null || newBaseValue.Parent == null;
            }

            if (e.NewValue == null) return;

            var obj = (newBaseValue != null) ? newBaseValue.Value : e.NewValue;

            if (obj == null) return;

            var objType = obj.GetType();
            var properties = objType.GetProperties();

            //проверка на то, что элемент уже редактировался.
            if (control._isRoot && newBaseValue != null)
            {
                var parent = newBaseValue.Parent;

                while (parent != null)
                {
                    if (parent == obj) return;
                    parent = (parent as AbstractValue != null) ? (parent as AbstractValue).Parent : null;
                }
            }

            if (obj.GetType().GetInterface("INotifyPropertyChanged") != null)
            {
                var notify = (INotifyPropertyChanged)obj;
                notify.PropertyChanged += control.NotifyOnPropertyChanged;
            }

            var index = 0;
            var keys = new string[properties.Length];
            foreach (var property in properties)
            {
                var order = Attribute.GetCustomAttribute(property, typeof(Order));

                if (order != null) keys[index] = order.ToString();
                else keys[index] = property.Name;

                index++;
            }

            Array.Sort(keys, properties);

            var translatePrefix = control.UpdateTranslatePrefix(obj);

            foreach (var property in properties)
            {
                try
                {
                    //если ссылка на самого себя - пропустить.
                    if (property.GetValue(obj, null) == obj) continue;
                }
                catch (Exception)
                {
                    continue;
                }

                var attr = Attribute.GetCustomAttributes(property);

                var isAdd = (control.ShowAttributes == null || control.ShowAttributes.Count == 0);

                if (!isAdd)
                    foreach (var attribute in attr)
                    {
                        var name = attribute.GetType().Name;

                        isAdd = control.ShowAttributes.Contains(name);
                        if (isAdd) break;
                    }

                if (!isAdd) continue;

                var newValue = CreateValueByProperty(obj, property);

                newValue.Parent = obj;
                newValue.TranslatePrefix = translatePrefix;

                if (control.Items.Contains(newValue))
                {
                    index = control.Items.IndexOf(newValue);

                    var oldValue = control.Items[index];

                    if (!oldValue.Value.Equals(newValue.Value))
                    {
                        control.Items[index].UpdateValue(obj);
                    }
                }
                else control.Items.Add(newValue);
            }
        }

        private void NotifyOnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            foreach (var item in Items)
            {
                if (item.Info.Name.Equals(args.PropertyName))
                {
                    var collectionValue = item as CollectionValue;
                    if (collectionValue != null)
                    {
                        collectionValue.UpdateValue(sender);
                    }
                    else item.InvokePropertyChanged("Value");

                    break;
                }
            }
        }

        private static BaseValue CreateValueByProperty(object obj, PropertyInfo property)
        {
            if (property.PropertyType != typeof(string) && property.PropertyType.GetInterface("IEnumerable") != null)
            {
                return new CollectionValue(obj, property);
            }

            return new BaseValue(obj, property);
        }

        public ObjectEditor()
        {
            _isRoot = false;
            Items = new ObservableCollection<BaseValue>();
            TemplateSelector = new TemplateSelector(this);
        }

        private static void UpdateTranslatePrefixProperty(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as ObjectEditor;

            if (control == null) return;
            control.UpdateTranslatePrefix(control.Value);
        }

        public string UpdateTranslatePrefix(object value)
        {
            var translatePrefix = TranslatePrefix;

            if (!string.IsNullOrEmpty(TranslatePrefix)) translatePrefix += "/";
            if (value != null) translatePrefix += value.GetType().Name;

            foreach (var item in Items)
            {
                item.TranslatePrefix = translatePrefix;
            }

            return translatePrefix;
        }
    }
}
