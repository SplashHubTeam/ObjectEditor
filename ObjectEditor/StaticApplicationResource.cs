using System;
using System.Windows;
using System.Windows.Markup;

namespace ObjectEditor
{
	[MarkupExtensionReturnType(typeof(object))]
	public class StaticApplicationResource : MarkupExtension
	{
		public StaticApplicationResource(object resourceKey)
		{
			ResourceKey = resourceKey;
		}

		[ConstructorArgument("resourceKey")]
		public object ResourceKey { get; set; }

		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			return ResourceKey == null ? null : Application.Current.TryFindResource(ResourceKey);
		}
	}
}
