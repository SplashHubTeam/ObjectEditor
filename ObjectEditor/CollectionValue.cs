using System.Collections;
using System.Collections.ObjectModel;
using System.Reflection;

namespace ObjectEditor
{
    /// <summary>
    /// Класс для редактирования свойства объекта типа коллекции.
    /// </summary>
    public class CollectionValue : BaseValue
    {
		protected IList Data { get; set; }
		public ObservableCollection<CollectionItem> Items { get; set; }

		public override object Value
		{
			get
			{
				return Property.GetValue(Object, null);
			}
			set
			{
				Property.SetValue(Object, value, null);
				UpdateValue(Object);
			}
		}

        public CollectionValue(object Object, PropertyInfo property) : base(Object, property)
        {
			Items = new ObservableCollection<CollectionItem>();

			UpdateValue(Object);
        }

		public override void UpdateValue(object Object)
		{
			this.Object = Object;
			Data = Value as IList;

			if (Items != null) Items.Clear();

			if (Data == null)
			{
				InvokePropertyChanged("Value");
				return;
			}

			if (Object != null && Data != null)
			{
				var index = 0;
				foreach (var item in Data)
				{
					Items.Add(new CollectionItem(item, Data, index++));
				}
			}

			InvokePropertyChanged("Value");
		}
    }
}
