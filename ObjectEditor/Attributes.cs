using System;

namespace ObjectEditor
{
	public class ShowInView : Attribute
	{

	}

	public class Order : Attribute
	{
		public uint OrderValue { get; set; }

		public Order(uint value)
		{
			OrderValue = value;
		}

		public override string ToString()
		{
			return OrderValue.ToString("000000");
		}
	}

	public class Validator : Attribute
	{
		public object Rule { get; set; }

		public Validator(object rule = null)
		{
			Rule = rule;
		}

		public Validator(string s)
		{
		}
	}
}
