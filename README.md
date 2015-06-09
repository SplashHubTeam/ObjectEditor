# Object Editor

Object Editor is a visual editor which allows to edit any properties of any class. The main idea is to minimize the code and to possible simplify the editing and displaying of a class instance.


## Description of the features and using
Here is an example of using the component. 
There is a class to edit (ShowInView attribute is required to mark edited property):

```c#
public class TestPresenter : NotificationObject
{
	[ShowInView]
	public string Text { get; set; }

	[ShowInView]
	public int Int { get; set; }

	[ShowInView]
	public string IP { get; set; }

	public TestPresenter()
	{
		Text = "text";
		Int = 100;
		IP = "127.0.0.1";
	}
}
```

Let’s create UserControl to edit this class. Put the class instance as DataContext into the constructor:

```c#
public partial class TestPresenterView : UserControl
{
	public TestPresenterView(TestPresenter presenter)
	{
		InitializeComponent();

		DataContext = presenter;
	}
}
```

XAML has this code:

```xml
<UserControl ...>
    <UserControl.Resources>
        <Style x:Key="LabelStyle">
            <Setter Property="TextBlock.VerticalAlignment" Value="Center"/>
            <Setter Property="TextBlock.FontWeight" Value="Bold"/>
            <Setter Property="TextBlock.Margin" Value="5,5,5,5"/>
        </Style>

        <Style x:Key="ElementStyle">
            <Setter Property="Control.VerticalAlignment" Value="Center"/>
            <Setter Property="Control.Margin" Value="20,0,10,0"/>
        </Style>
    </UserControl.Resources>

    <Grid>
        <Grid.RowDefinitions>
            <RowDefinition Height="Auto"/>
            <RowDefinition Height="Auto"/>
            <RowDefinition Height="Auto"/>
            <RowDefinition Height="Auto"/>
            <RowDefinition Height="Auto"/>
            <RowDefinition Height="Auto"/>
        </Grid.RowDefinitions>

        <TextBlock Text="TestPresenter/Test" Style="{StaticResource LabelStyle}"/>
        <TextBox Text="{Binding Text}" Style="{StaticResource ElementStyle}" Grid.Row="1"/>

        <TextBlock Text="TestPresenter/Int" Style="{StaticResource LabelStyle}" Grid.Row="2"/>
        <TextBox Text="{Binding Int}" Style="{StaticResource ElementStyle}" Grid.Row="3"/>

        <TextBlock Text="TestPresenter/IP" Style="{StaticResource LabelStyle}" Grid.Row="4"/>
        <TextBox Text="{Binding IP}" Style="{StaticResource ElementStyle}" Grid.Row="5"/>
    </Grid>
</UserControl>
```

Picture 1 shows the form’s view.

![Picture 1](http://download-codeplex.sec.s-msft.com/Download?ProjectName=objecteditor&DownloadId=894198)

Picture 1.


You need to create a control to each property and use Binding. It takes long time even for a small class with simple same type properties.
Object Editor can simplify this process. XAML has this code with using Object Editor:

```xml
<UserControl ...>
    <ObjectEditor:ObjectEditor Value="{Binding}" IsReadOnly="False"/>
</UserControl>
```

Picture 2 shows the form’s view with default data template.

![Picture 2](http://download-codeplex.sec.s-msft.com/Download?ProjectName=objecteditor&DownloadId=894201)

Picture 2.

Object Editor can significantly simplify work with class which contains both simple and complex properties like lists, collections, classes etc.

Enum properties require special consideration. You need to create a special class to edit enum and flag properties. This class must contain all available values and creates a new flag after changing. There is an extra component Enum Editor to edit enum and flag properties.
For example, there is a class to edit:

```c#
public enum MyEnum
{
	Enum1,
	Enum2
}

[Flags]
public enum MyFlags
{
	FlagNone = 0,
	Flag1 = 1,
	Flag2 = 1 << 1
}

public class TestPresenter : NotificationObject
{
	[ShowInView]
	public MyEnum EditEnum { get; set; }

	[ShowInView]
	public MyFlags EditFlag { get; set; }

	public TestPresenter()
	{
	}
}
```

XAML has this code:

```xml
<UserControl ...>
    <ObjectEditor:ObjectEditor Value="{Binding}" IsReadOnly="False"/>
</UserControl>
```

The form view with enum и flag properties is in picture 3 and 4 accordingly.

![Picture 3](http://download-codeplex.sec.s-msft.com/Download?ProjectName=objecteditor&DownloadId=894203)

Picture 3.

![Picture 4](http://download-codeplex.sec.s-msft.com/Download?ProjectName=objecteditor&DownloadId=894204)

Picture 4.

Enum Editor generates available values as a list and manages changing of the value to create a new flag. If you choose MyFlags/FlagNone the current value automatically changes and value MyFlags/Flag1 unchecks (Picture 5).

![Picture 5](http://download-codeplex.sec.s-msft.com/Download?ProjectName=objecteditor&DownloadId=894205)

Picture 5.

The short list of Object Editor properties is as follows:

* object Value – edited object.
* ObservableCollection<BaseValue> Items – list of object properties.
* List<string> ShowAttributes – list of edited attributes. It contains ShowInView by default.
* bool IsReadOnly – read only flag (for read only templates).



## Templates

You can set your templates for both simple types and user classes.
For example, you have the class:

```c#
public class TestPresenter : NotificationObject
{
	[ShowInView]
	public string Name { get; set; }

	[ShowInView]
	public BitmapImage Pixmap { get { return @"D:\test.png"; } }

	public TestPresenter()
	{ 
		Name = "Test Name";
		Pixmap = new BitmapImage();
		Pixmap.BeginInit();
		Pixmap.UriSource = new Uri(@"D:\test.png");
		Pixmap.EndInit();
	}
}
```

XAML has the code:

```xml
<UserControl ...>
    <ObjectEditor:ObjectEditor Value="{Binding}" IsReadOnly="False"/>
</UserControl>
```

Picture 6 shows the form’s view.

![Picture 6](http://download-codeplex.sec.s-msft.com/Download?ProjectName=objecteditor&DownloadId=894206)

Picture 6.

Set a user template instead of the Default template. The changed XAML has the code:

```xml
<UserControl ...>
    <UserControl.Resources>
        <DataTemplate x:Key="BitmapImage">
            <Image Source="{Binding Value}" Width="100"/>
        </DataTemplate>
    </UserControl.Resources>

    <ObjectEditor:ObjectEditor Value="{Binding}" Templates="BitmapImage" IsReadOnly="False"/>
</UserControl>
```

Picture 7 shows the form’s view with the changes.

![Picture 7](http://download-codeplex.sec.s-msft.com/Download?ProjectName=objecteditor&DownloadId=894207)

Picture 7.

If you want to define template for any data type you need to add DataTemplate and set the name of type as a key (or name of type + ReadOnly for the read-only mode). Then you need to add this key to Templates property of the Object Editor, this way you will save time on useless datatemplate search (FindResource).

There is a way to avoid using Templates property – you can define templates in the Object Editor resources:

```xml
<UserControl ...>
    <ObjectEditor:ObjectEditor Value="{Binding}" IsReadOnly="False">
        <ObjectEditor:ObjectEditor.Resources>
            <DataTemplate x:Key="BitmapImage">
                <Image Source="{Binding Value}" Width="100"/>
            </DataTemplate>
        </ObjectEditor:ObjectEditor.Resources>
    </ObjectEditor:ObjectEditor>
</UserControl>
```

This way was chosen to strictly separate editable and read-only templates and to avoid program crashes  in case of using TwoWay Binding for read-only property.

There are templates that are defined by default:

* Boolean (BooleanReadOnly)
* String (StringReadOnly)
* Single (SingleReadOnly)
* Double (DoubleReadOnly)
* Int32 (Int32ReadOnly)
Enum type has special templates that can be redefined:

* EnumValue (EnumValueReadOnly)
* FlagsValue (FlagsValueReadOnly)

For example, if you want to override enum’s template it’s enough to define template and set “EnumValue” as a key:

```xml
<UserControl ...>
    <UserControl.Resources>
        <DataTemplate x:Key="EnumValue">
            <ListBox ItemsSource="{Binding Items}" SelectedItem="{Binding Value}"/>
        </DataTemplate>
    </UserControl.Resources>

    <ObjectEditor:ObjectEditor Value="{Binding}" Templates="EnumValue" IsReadOnly="False"/>
</UserControl>
```

Picture 8 shows the form’s view with enum’s template.

![Picture 8](http://download-codeplex.sec.s-msft.com/Download?ProjectName=objecteditor&DownloadId=894210)

Picture 8.

If there is no defined template for edited object, default template will be used.
For example, you have the class:

```c#
public enum MyEnum
{
	Enum1,
	Enum2
}

public class ListClass
{
	[ShowInView]
	public string Text { get; set; }

	[ShowInView
	public MyFlags EditFlag { get; set; }

	public ListClass()
	{
	}
}

public class TestPresenter : NotificationObject
{
	[ShowInView]
	public ListClass Class { get; set; }

	[ShowInView]
	public List<ListClass> ClassList { get; set; }

	public TestPresenter()
	{
		Class = new ListClass { Text = "Class", EditFlag = MyFlags.Flag1 };

		ClassList = new List<ListClass>
		{
			new ListClass { Text = "Class 1", EditFlag = MyFlags.Flag1 },
			new ListClass { Text = "Class 2", EditFlag = MyFlags.Flag2 }
		};
	}
}
```

XAML has the code:

```xml
<UserControl ...>
    <ObjectEditor:ObjectEditor Value="{Binding}" IsReadOnly="False"/>
</UserControl>
```

Picture 9 shows the form’s view for this class.

![Picture 9](http://download-codeplex.sec.s-msft.com/Download?ProjectName=objecteditor&DownloadId=894211)

Picture 9.

By default, such objects are displayed as collapsed items to avoid recursive building of tree properties because it takes long time.

Picture 10 shows the form’s view with expanded items.

![Picture 10](http://download-codeplex.sec.s-msft.com/Download?ProjectName=objecteditor&DownloadId=894212)

Picture 10

To change it, you need to define a template for such objects.

In some cases, you need to change default displaying composition, but it’s not enough to define user templates. For example, you want to change the following class composition:

```c#
public class TestPresenter : NotificationObject
{
	[ShowInView]
	public List<string> StringList { get; set; }

	public TestPresenter()
	{
		StringList = new List<string> { "text 1", "text 2" };
	}
}
```

XAML has the code:

```xml
<UserControl ...>
    <ObjectEditor:ObjectEditor Value="{Binding}" IsReadOnly="True"/>
</UserControl>
```

Picture 11 shows the form’s view with default data template.

![Picture 11](http://download-codeplex.sec.s-msft.com/Download?ProjectName=objecteditor&DownloadId=894213)

Picture 11

Class CollectionValue is responsible for the list displaying and you need to define new template in this case:

```xml
<UserControl ...> 
    <UserControl.Resources>
        <Style x:Key="LabelStyle">
            <Setter Property="TextBlock.VerticalAlignment" Value="Center"/>
            <Setter Property="TextBlock.FontWeight" Value="Bold"/>
            <Setter Property="TextBlock.Margin" Value="5,5,5,5"/>
        </Style>
        <DataTemplate DataType="{x:Type ObjectEditor:CollectionValue}">
            <StackPanel>
                <TextBlock Text="{Binding}" Style="{StaticResource LabelStyle}"/>

                <ListBox ItemsSource="{Binding Items}"/>
            </StackPanel>
        </DataTemplate>
    </UserControl.Resources>

    <ObjectEditor:ObjectEditor Value="{Binding}" IsReadOnly="True"/>
</UserControl>
```

Picture 12 shows the form’s view with redefined template.

![Picture 12](http://download-codeplex.sec.s-msft.com/Download?ProjectName=objecteditor&DownloadId=894214)

Picture 12

There are visual classes that you can also redefine:
* AbstractValue – base class to edit any property of object. 
* CollectionValue – class to edit a list property.
* ObjectEditor – the main class to edit object.?



## Other features

Also, you can specify a validator and order of displayed properties:

```c#
[ShowInView, Order(80), IntValidator(0, 10)]
public string Name { get; set; }
```

Validator must be inherited from the ValidatorAttribute class and overrides Rule property:

```c#
public class ValidatorAttribute : Attribute
{
	public virtual ValidationRule Rule { get; private set; }

	public ValidatorAttribute()
	{
		;
	}
}

public class IntRule : ValidationRule
{
    public int Max { get; set; }
    public int Min { get; set; }

    public IntRule()
    {
        Min = int.MinValue;
        Max = int.MaxValue;
    }

    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        int intValue = 0;
        
        try
        {
            if (!string.IsNullOrWhiteSpace((string)value))
            {
                intValue = int.Parse((string)value, cultureInfo);
            }
        }
        catch
        {
            return new ValidationResult(false, "not correct value");
        }
        if (intValue < Min || intValue > Max)
        {
            return new ValidationResult(false, string.Format("value is not in range [{0}..{1}]", Min, Max));
        }
        else
        {
            return new ValidationResult(true, null);
        }
    }
}

public class IntValidator : ValidatorAttribute
{
	public int Max { get; set; }
	public int Min { get; set; }

	public override ValidationRule Rule
	{
		get { return new IntRule { Min = Min, Max = Max }; }
	}

	public IntValidator()
	{
		Min = int.MinValue;
		Max = int.MaxValue;
	}

	public IntValidator(int min, int max)
	{
		Min = min;
		Max = max;
	}
}

[ShowInView, Order(4), IntValidator(0, 10)]
public int Number { get; set; }
```

This way was chosen because there is no possibility to use universal attribute to any validation rule and you can’t set an object as a constructor parameter in attributes (exception: an attribute argument must be a constant expression, typeof expression or array creation expression of an attribute parameter type).

Default templates use validation attribute automatically. If you want to use this feature in your templates you need to use ValidatorBinding instead of Binding: 

```xml
<UserControl ...>
    <UserControl.Resources>
        <DataTemplate x:Key="String">
            <TextBlock Text="{ObjectEditor:ValidatorBinding Value}" FontWeight="Bold"
                       HorizontalAlignment="Center"/>
        </DataTemplate>
    </UserControl.Resources>

   ...

</UserControl>
```

ShowAttributes property allows separate properties and displays each group into a different view. For example:

```c#
public class TestPresenter : NotificationObject
{
	[ErrorMessage]
	public string ErrorText1 { get; set; }

	[ErrorMessage]
	public string ErrorText2 { get; set; }

	[Params]
	public int Param1 { get; set; }

	[Params]
	public int Param2 { get; set; }

	public TestPresenter()
	{
		ErrorText1 = "Error 1";
		ErrorText2 = "Error 2";
		Param1 = 100;
		Param2 = 200;
	}
}
```

You can separate displaying parameters and errors for this class:

```xml
<UserControl ...>
    <UserControl.Resources>
        <DataTemplate x:Key="StringReadOnly">
            <TextBlock Text="{Binding Value}" Foreground="Red"
                       FontWeight="Bold" HorizontalAlignment="Center"/>
        </DataTemplate>
    </UserControl.Resources>

    <Grid>
        <Grid.RowDefinitions>
            <RowDefinition/>
            <RowDefinition Height="Auto"/>
            <RowDefinition/>
        </Grid.RowDefinitions>

        <ObjectEditor:ObjectEditor Value="{Binding}" IsReadOnly="False" ShowAttributes="Params"/>
        
        <Button Grid.Row="1" Padding="10,5,10,5" HorizontalAlignment="Center">
            <TextBlock Text="Button"/>
        </Button>

        <ObjectEditor:ObjectEditor Value="{Binding}" Templates="StringReadOnly"
                                   ShowAttributes="ErrorMessage" Grid.Row="2"/>

    </Grid>
</UserControl>
```

Picture 13 shows the form’s view.

![Picture 13](http://download-codeplex.sec.s-msft.com/Download?ProjectName=objecteditor&DownloadId=894215)

Picture 13



## Localization

Object Editor uses Localization Library to simplify localization process. This library keeps translations in xml-files.

This library is small and simple in use. You can set translation path by XPath:

```xml
<UserControl xmlns:Langlib="clr-namespace:LocalizationLibrary;assembly=LocalizationLibrary" ...> 
    <TextBlock Text="{Langlib:Title Buttons/CloseButton}"/>
    <TextBlock Text="{Langlib:Title XPath=Buttons/CloseButton}"/>
</UserControl>
```

There are a few available functions to use in LocalizationLibrary:

* static string[] GetLangs(string path = null) – return list of available languages.
* static void SetLang(string lang) – set current language.
* static string GetTitle(string xpath) – get translation by XPath.
* static string GetTitle(string xpath, string defaultTitle) – get translation by XPath with default value.

XML-files must have this kind of structure:

```xml
<?xml version="1.0" encoding="utf-8" ?>
<LanguagesResource culture="en-GB">
    <Buttons>
        <CloseButton>Close</CloseButton>
    </Buttons>
</LanguagesResource>
```

It contains LanguagesResource node with the culture attribute.
Translations files are kept in the Languages directory which is near the exe-file.
You can create a translation file per each language. The list of available languages will be created from the file names. For example, there are two translation files for English and Russian:

EN.xml
RU.xml

List of available languages will contain two elements: EN, RU which can be used to set current interface language by using SetLang function. 
You can also create folders with translation files:

EN\
        Tables.xml
        Dialogs.xml
RU\
        Tables.xml
        Dialogs.xml

List of available languages will be created from folder names, as written above.



## Automatic localization

Object Editor uses LocalizationLibrary for automation and simplification of the localization process.

Using the aforementioned example to show this feature:

```c#
public class TestPresenter : NotificationObject
{
	[ShowInView]
	public string Text { get; set; }

	[ShowInView]
	public int Int { get; set; }

	[ShowInView]
	public string IP { get; set; }

	public TestPresenter()
	{
		Text = "text";
		Int = 100;
		IP = "127.0.0.1";
	}
}
```

```xml
<UserControl ...>
    <ObjectEditor:ObjectEditor Value="{Binding}" IsReadOnly="False"/>
</UserControl>
```

Picture 14 shows the form’s view.

![Picture 14](http://download-codeplex.sec.s-msft.com/Download?ProjectName=objecteditor&DownloadId=894216)

Picture 14

This interface can be localized by translation file which contains TestPresenter node with Test, Int, IP translating sections:

```xml
<?xml version="1.0" encoding="utf-8" ?>
<LanguagesResource culture="en-GB">
    <TestPresenter>
        <Test>Test</Test>
        <Int>Int</Int>
        <IP>IP</IP>
    </TestPresenter>
</LanguagesResource>
```

![Picture 15](http://download-codeplex.sec.s-msft.com/Download?ProjectName=objecteditor&DownloadId=894217)

Picture 15

Object Editor uses the following algorithm to create a translation path: class name of editable object {"+ / +"} property name.

You can set any translation prefix to Object Editor by using property string TranslatePrefix.

As a result, the full translation path is combined in the following order: 
translate prefix (if it exists) {"+ / +"} class name of editable object {"+ / +"} property name.

If there is no translation for the current language, the interface displays the full translation path which must be added to the translation file. This is well illustrated in all the above screenshots.
