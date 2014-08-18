using System;
using System.Windows.Controls;

namespace ObjectEditor
{
	public class ValidatorAttribute : Attribute
	{
		public virtual ValidationRule Rule { get; private set; }

		public ValidatorAttribute()
		{
			;
		}
	}
}
