using System;
using System.Collections;

namespace ObjectEditor
{
	public class CollectionItem : AbstractValue
	{
		protected object Object;
		protected IList Data { get; set; }
		protected int Index { get; set; }

		public override Type Type { get { return Object.GetType(); } }

		public override object Value
		{
			get
			{
				return Object;
			}
			set
			{
				Object = value;
				Data[Index] = value;
			}
		}

		public CollectionItem(object Object, IList data, int index)
		{
			this.Object = Object;
			Data = data;
			Index = index;
		}

		public override string ToString()
		{
			return "";
		}
	}
}
