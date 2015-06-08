<h1>Object Editor</h1>
<p>Object Editor is a visual editor which allows to edit any properties of any class. The main idea is to minimize the code and to possible simplify the editing and displaying of a class instance.<br /><br /></p>
<h2>Description of the features and using</h2>
<p>Here is an example of using the component. <br />There is a class to edit (ShowInView attribute is required to mark edited property):<br /><br /></p>
<div style="color: black; background-color: white;">
<pre><span style="color: blue;">public</span> <span style="color: blue;">class</span> TestPresenter : NotificationObject
{
	[ShowInView]
	<span style="color: blue;">public</span> <span style="color: blue;">string</span> Text { <span style="color: blue;">get</span>; <span style="color: blue;">set</span>; }

	[ShowInView]
	<span style="color: blue;">public</span> <span style="color: blue;">int</span> Int { <span style="color: blue;">get</span>; <span style="color: blue;">set</span>; }

	[ShowInView]
	<span style="color: blue;">public</span> <span style="color: blue;">string</span> IP { <span style="color: blue;">get</span>; <span style="color: blue;">set</span>; }

	<span style="color: blue;">public</span> TestPresenter()
	{
		Text = <span style="color: #a31515;">"text"</span>;
		Int = 100;
		IP = <span style="color: #a31515;">"127.0.0.1"</span>;
	}
}
</pre>
</div>
<p><br />Let&rsquo;s create UserControl to edit this class. Put the class instance as DataContext into the constructor:<br /><br /></p>
<div style="color: black; background-color: white;">
<pre><span style="color: blue;">public</span> <span style="color: blue;">partial</span> <span style="color: blue;">class</span> TestPresenterView : UserControl
{
	<span style="color: blue;">public</span> TestPresenterView(TestPresenter presenter)
	{
		InitializeComponent();

		DataContext = presenter;
	}
}
</pre>
</div>
<p><br />XAML has this code:<br /><br /></p>
<div style="color: black; background-color: white;">
<pre><span style="color: blue;">&lt;</span><span style="color: #a31515;">UserControl</span> <span style="color: red;">...</span><span style="color: blue;">&gt;</span>
    <span style="color: blue;">&lt;</span><span style="color: #a31515;">UserControl.Resources</span><span style="color: blue;">&gt;</span>
        <span style="color: blue;">&lt;</span><span style="color: #a31515;">Style</span> <span style="color: red;">x:Key</span><span style="color: blue;">=</span><span style="color: black;">"</span><span style="color: blue;">LabelStyle</span><span style="color: black;">"</span><span style="color: blue;">&gt;</span>
            <span style="color: blue;">&lt;</span><span style="color: #a31515;">Setter</span> <span style="color: red;">Property</span><span style="color: blue;">=</span><span style="color: black;">"</span><span style="color: blue;">TextBlock.VerticalAlignment</span><span style="color: black;">"</span> <span style="color: red;">Value</span><span style="color: blue;">=</span><span style="color: black;">"</span><span style="color: blue;">Center</span><span style="color: black;">"</span><span style="color: blue;">/&gt;</span>
            <span style="color: blue;">&lt;</span><span style="color: #a31515;">Setter</span> <span style="color: red;">Property</span><span style="color: blue;">=</span><span style="color: black;">"</span><span style="color: blue;">TextBlock.FontWeight</span><span style="color: black;">"</span> <span style="color: red;">Value</span><span style="color: blue;">=</span><span style="color: black;">"</span><span style="color: blue;">Bold</span><span style="color: black;">"</span><span style="color: blue;">/&gt;</span>
            <span style="color: blue;">&lt;</span><span style="color: #a31515;">Setter</span> <span style="color: red;">Property</span><span style="color: blue;">=</span><span style="color: black;">"</span><span style="color: blue;">TextBlock.Margin</span><span style="color: black;">"</span> <span style="color: red;">Value</span><span style="color: blue;">=</span><span style="color: black;">"</span><span style="color: blue;">5,5,5,5</span><span style="color: black;">"</span><span style="color: blue;">/&gt;</span>
        <span style="color: blue;">&lt;/</span><span style="color: #a31515;">Style</span><span style="color: blue;">&gt;</span>

        <span style="color: blue;">&lt;</span><span style="color: #a31515;">Style</span> <span style="color: red;">x:Key</span><span style="color: blue;">=</span><span style="color: black;">"</span><span style="color: blue;">ElementStyle</span><span style="color: black;">"</span><span style="color: blue;">&gt;</span>
            <span style="color: blue;">&lt;</span><span style="color: #a31515;">Setter</span> <span style="color: red;">Property</span><span style="color: blue;">=</span><span style="color: black;">"</span><span style="color: blue;">Control.VerticalAlignment</span><span style="color: black;">"</span> <span style="color: red;">Value</span><span style="color: blue;">=</span><span style="color: black;">"</span><span style="color: blue;">Center</span><span style="color: black;">"</span><span style="color: blue;">/&gt;</span>
            <span style="color: blue;">&lt;</span><span style="color: #a31515;">Setter</span> <span style="color: red;">Property</span><span style="color: blue;">=</span><span style="color: black;">"</span><span style="color: blue;">Control.Margin</span><span style="color: black;">"</span> <span style="color: red;">Value</span><span style="color: blue;">=</span><span style="color: black;">"</span><span style="color: blue;">20,0,10,0</span><span style="color: black;">"</span><span style="color: blue;">/&gt;</span>
        <span style="color: blue;">&lt;/</span><span style="color: #a31515;">Style</span><span style="color: blue;">&gt;</span>
    <span style="color: blue;">&lt;/</span><span style="color: #a31515;">UserControl.Resources</span><span style="color: blue;">&gt;</span>

    <span style="color: blue;">&lt;</span><span style="color: #a31515;">Grid</span><span style="color: blue;">&gt;</span>
        <span style="color: blue;">&lt;</span><span style="color: #a31515;">Grid.RowDefinitions</span><span style="color: blue;">&gt;</span>
            <span style="color: blue;">&lt;</span><span style="color: #a31515;">RowDefinition</span> <span style="color: red;">Height</span><span style="color: blue;">=</span><span style="color: black;">"</span><span style="color: blue;">Auto</span><span style="color: black;">"</span><span style="color: blue;">/&gt;</span>
            <span style="color: blue;">&lt;</span><span style="color: #a31515;">RowDefinition</span> <span style="color: red;">Height</span><span style="color: blue;">=</span><span style="color: black;">"</span><span style="color: blue;">Auto</span><span style="color: black;">"</span><span style="color: blue;">/&gt;</span>
            <span style="color: blue;">&lt;</span><span style="color: #a31515;">RowDefinition</span> <span style="color: red;">Height</span><span style="color: blue;">=</span><span style="color: black;">"</span><span style="color: blue;">Auto</span><span style="color: black;">"</span><span style="color: blue;">/&gt;</span>
            <span style="color: blue;">&lt;</span><span style="color: #a31515;">RowDefinition</span> <span style="color: red;">Height</span><span style="color: blue;">=</span><span style="color: black;">"</span><span style="color: blue;">Auto</span><span style="color: black;">"</span><span style="color: blue;">/&gt;</span>
            <span style="color: blue;">&lt;</span><span style="color: #a31515;">RowDefinition</span> <span style="color: red;">Height</span><span style="color: blue;">=</span><span style="color: black;">"</span><span style="color: blue;">Auto</span><span style="color: black;">"</span><span style="color: blue;">/&gt;</span>
            <span style="color: blue;">&lt;</span><span style="color: #a31515;">RowDefinition</span> <span style="color: red;">Height</span><span style="color: blue;">=</span><span style="color: black;">"</span><span style="color: blue;">Auto</span><span style="color: black;">"</span><span style="color: blue;">/&gt;</span>
        <span style="color: blue;">&lt;/</span><span style="color: #a31515;">Grid.RowDefinitions</span><span style="color: blue;">&gt;</span>

        <span style="color: blue;">&lt;</span><span style="color: #a31515;">TextBlock</span> <span style="color: red;">Text</span><span style="color: blue;">=</span><span style="color: black;">"</span><span style="color: blue;">TestPresenter/Test</span><span style="color: black;">"</span> <span style="color: red;">Style</span><span style="color: blue;">=</span><span style="color: black;">"</span><span style="color: blue;">{StaticResource LabelStyle}</span><span style="color: black;">"</span><span style="color: blue;">/&gt;</span>
        <span style="color: blue;">&lt;</span><span style="color: #a31515;">TextBox</span> <span style="color: red;">Text</span><span style="color: blue;">=</span><span style="color: black;">"</span><span style="color: blue;">{Binding Text}</span><span style="color: black;">"</span> <span style="color: red;">Style</span><span style="color: blue;">=</span><span style="color: black;">"</span><span style="color: blue;">{StaticResource ElementStyle}</span><span style="color: black;">"</span> <span style="color: red;">Grid.Row</span><span style="color: blue;">=</span><span style="color: black;">"</span><span style="color: blue;">1</span><span style="color: black;">"</span><span style="color: blue;">/&gt;</span>

        <span style="color: blue;">&lt;</span><span style="color: #a31515;">TextBlock</span> <span style="color: red;">Text</span><span style="color: blue;">=</span><span style="color: black;">"</span><span style="color: blue;">TestPresenter/Int</span><span style="color: black;">"</span> <span style="color: red;">Style</span><span style="color: blue;">=</span><span style="color: black;">"</span><span style="color: blue;">{StaticResource LabelStyle}</span><span style="color: black;">"</span> <span style="color: red;">Grid.Row</span><span style="color: blue;">=</span><span style="color: black;">"</span><span style="color: blue;">2</span><span style="color: black;">"</span><span style="color: blue;">/&gt;</span>
        <span style="color: blue;">&lt;</span><span style="color: #a31515;">TextBox</span> <span style="color: red;">Text</span><span style="color: blue;">=</span><span style="color: black;">"</span><span style="color: blue;">{Binding Int}</span><span style="color: black;">"</span> <span style="color: red;">Style</span><span style="color: blue;">=</span><span style="color: black;">"</span><span style="color: blue;">{StaticResource ElementStyle}</span><span style="color: black;">"</span> <span style="color: red;">Grid.Row</span><span style="color: blue;">=</span><span style="color: black;">"</span><span style="color: blue;">3</span><span style="color: black;">"</span><span style="color: blue;">/&gt;</span>

        <span style="color: blue;">&lt;</span><span style="color: #a31515;">TextBlock</span> <span style="color: red;">Text</span><span style="color: blue;">=</span><span style="color: black;">"</span><span style="color: blue;">TestPresenter/IP</span><span style="color: black;">"</span> <span style="color: red;">Style</span><span style="color: blue;">=</span><span style="color: black;">"</span><span style="color: blue;">{StaticResource LabelStyle}</span><span style="color: black;">"</span> <span style="color: red;">Grid.Row</span><span style="color: blue;">=</span><span style="color: black;">"</span><span style="color: blue;">4</span><span style="color: black;">"</span><span style="color: blue;">/&gt;</span>
        <span style="color: blue;">&lt;</span><span style="color: #a31515;">TextBox</span> <span style="color: red;">Text</span><span style="color: blue;">=</span><span style="color: black;">"</span><span style="color: blue;">{Binding IP}</span><span style="color: black;">"</span> <span style="color: red;">Style</span><span style="color: blue;">=</span><span style="color: black;">"</span><span style="color: blue;">{StaticResource ElementStyle}</span><span style="color: black;">"</span> <span style="color: red;">Grid.Row</span><span style="color: blue;">=</span><span style="color: black;">"</span><span style="color: blue;">5</span><span style="color: black;">"</span><span style="color: blue;">/&gt;</span>
    <span style="color: blue;">&lt;/</span><span style="color: #a31515;">Grid</span><span style="color: blue;">&gt;</span>
<span style="color: blue;">&lt;/</span><span style="color: #a31515;">UserControl</span><span style="color: blue;">&gt;</span>
</pre>
</div>
<p><br />Picture 1 shows the form&rsquo;s view.<br /><br /></p>
<blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote><img title="1.png" src="http://download-codeplex.sec.s-msft.com/Download?ProjectName=objecteditor&amp;DownloadId=894198" alt="1.png" /></blockquote>
</blockquote>
</blockquote>
</blockquote>
</blockquote>
</blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote>Picture 1.</blockquote>
</blockquote>
</blockquote>
</blockquote>
</blockquote>
</blockquote>
</blockquote>
</blockquote>
</blockquote>
<p><br />You need to create a control to each property and use Binding. It takes long time even for a small class with simple same type properties.<br />Object Editor can simplify this process. XAML has this code with using Object Editor:<br /><br /></p>
<div style="color: black; background-color: white;">
<pre><span style="color: blue;">&lt;</span><span style="color: #a31515;">UserControl</span> <span style="color: red;">...</span><span style="color: blue;">&gt;</span>
    <span style="color: blue;">&lt;</span><span style="color: #a31515;">ObjectEditor</span><span style="color: blue;">:</span><span style="color: #a31515;">ObjectEditor</span> <span style="color: red;">Value</span><span style="color: blue;">=</span><span style="color: black;">"</span><span style="color: blue;">{Binding}</span><span style="color: black;">"</span> <span style="color: red;">IsReadOnly</span><span style="color: blue;">=</span><span style="color: black;">"</span><span style="color: blue;">False</span><span style="color: black;">"</span><span style="color: blue;">/&gt;</span>
<span style="color: blue;">&lt;/</span><span style="color: #a31515;">UserControl</span><span style="color: blue;">&gt;</span>
</pre>
</div>
<p><br />Picture 2 shows the form&rsquo;s view with default data template.<br /><br /></p>
<blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote><img title="2.png" src="http://download-codeplex.sec.s-msft.com/Download?ProjectName=objecteditor&amp;DownloadId=894201" alt="2.png" /></blockquote>
</blockquote>
</blockquote>
</blockquote>
</blockquote>
</blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote>Picture 2.</blockquote>
</blockquote>
</blockquote>
</blockquote>
</blockquote>
</blockquote>
</blockquote>
</blockquote>
</blockquote>
<p>Object Editor can significantly simplify work with class which contains both simple and complex properties like lists, collections, classes etc.<br /><br />Enum properties require special consideration. You need to create a special class to edit enum and flag properties. This class must contain all available values and creates a new flag after changing. There is an extra component Enum Editor to edit enum and flag properties.<br />For example, there is a class to edit:<br /><br /></p>
<div style="color: black; background-color: white;">
<pre><span style="color: blue;">public</span> <span style="color: blue;">enum</span> MyEnum
{
	Enum1,
	Enum2
}

[Flags]
<span style="color: blue;">public</span> <span style="color: blue;">enum</span> MyFlags
{
	FlagNone = 0,
	Flag1 = 1,
	Flag2 = 1 &lt;&lt; 1
}

<span style="color: blue;">public</span> <span style="color: blue;">class</span> TestPresenter : NotificationObject
{
	[ShowInView]
	<span style="color: blue;">public</span> MyEnum EditEnum { <span style="color: blue;">get</span>; <span style="color: blue;">set</span>; }

	[ShowInView]
	<span style="color: blue;">public</span> MyFlags EditFlag { <span style="color: blue;">get</span>; <span style="color: blue;">set</span>; }

	<span style="color: blue;">public</span> TestPresenter()
	{
	}
}
</pre>
</div>
<p><br />XAML has this code:<br /><br /></p>
<div style="color: black; background-color: white;">
<pre><span style="color: blue;">&lt;</span><span style="color: #a31515;">UserControl</span> <span style="color: red;">...</span><span style="color: blue;">&gt;</span>
    <span style="color: blue;">&lt;</span><span style="color: #a31515;">ObjectEditor</span><span style="color: blue;">:</span><span style="color: #a31515;">ObjectEditor</span> <span style="color: red;">Value</span><span style="color: blue;">=</span><span style="color: black;">"</span><span style="color: blue;">{Binding}</span><span style="color: black;">"</span> <span style="color: red;">IsReadOnly</span><span style="color: blue;">=</span><span style="color: black;">"</span><span style="color: blue;">False</span><span style="color: black;">"</span><span style="color: blue;">/&gt;</span>
<span style="color: blue;">&lt;/</span><span style="color: #a31515;">UserControl</span><span style="color: blue;">&gt;</span>
</pre>
</div>
<p><br />The form view with enum и flag properties is in picture 3 and 4 accordingly.<br /><br /></p>
<blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote><img title="3.png" src="http://download-codeplex.sec.s-msft.com/Download?ProjectName=objecteditor&amp;DownloadId=894203" alt="3.png" /></blockquote>
</blockquote>
</blockquote>
</blockquote>
</blockquote>
</blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote>Picture 3.</blockquote>
</blockquote>
</blockquote>
</blockquote>
</blockquote>
</blockquote>
</blockquote>
</blockquote>
</blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote><img title="4.png" src="http://download-codeplex.sec.s-msft.com/Download?ProjectName=objecteditor&amp;DownloadId=894204" alt="4.png" /></blockquote>
</blockquote>
</blockquote>
</blockquote>
</blockquote>
</blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote>Picture 4.</blockquote>
</blockquote>
</blockquote>
</blockquote>
</blockquote>
</blockquote>
</blockquote>
</blockquote>
</blockquote>
<p>Enum Editor generates available values as a list and manages changing of the value to create a new flag. If you choose MyFlags/FlagNone the current value automatically changes and value MyFlags/Flag1 unchecks (Picture 5).<br /><br /></p>
<blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote><img title="5.png" src="http://download-codeplex.sec.s-msft.com/Download?ProjectName=objecteditor&amp;DownloadId=894205" alt="5.png" /></blockquote>
</blockquote>
</blockquote>
</blockquote>
</blockquote>
</blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote>Picture 5.</blockquote>
</blockquote>
</blockquote>
</blockquote>
</blockquote>
</blockquote>
</blockquote>
</blockquote>
</blockquote>
<p>The short list of Object Editor properties is as follows:<br /><br />object Value &ndash; edited object.<br />ObservableCollection&lt;BaseValue&gt; Items &ndash; list of object properties.<br />List&lt;string&gt; ShowAttributes &ndash; list of edited attributes. It contains ShowInView by default.<br />bool IsReadOnly &ndash; read only flag (for read only templates).<br /><br /><br /></p>
<h2>Templates</h2>
<p>You can set your templates for both simple types and user classes.<br />For example, you have the class:<br /><br /></p>
<div style="color: black; background-color: white;">
<pre><span style="color: blue;">public</span> <span style="color: blue;">class</span> TestPresenter : NotificationObject
{
	[ShowInView]
	<span style="color: blue;">public</span> <span style="color: blue;">string</span> Name { <span style="color: blue;">get</span>; <span style="color: blue;">set</span>; }

	[ShowInView]
	<span style="color: blue;">public</span> BitmapImage Pixmap { <span style="color: blue;">get</span> { <span style="color: blue;">return</span> <span style="color: #a31515;">@"D:\test.png"</span>; } }

	<span style="color: blue;">public</span> TestPresenter()
	{ 
		Name = <span style="color: #a31515;">"Test Name"</span>;
		Pixmap = <span style="color: blue;">new</span> BitmapImage();
		Pixmap.BeginInit();
		Pixmap.UriSource = <span style="color: blue;">new</span> Uri(<span style="color: #a31515;">@"D:\test.png"</span>);
		Pixmap.EndInit();
	}
}
</pre>
</div>
<p><br />XAML has the code:<br /><br /></p>
<div style="color: black; background-color: white;">
<pre><span style="color: blue;">&lt;</span><span style="color: #a31515;">UserControl</span> <span style="color: red;">...</span><span style="color: blue;">&gt;</span>
    <span style="color: blue;">&lt;</span><span style="color: #a31515;">ObjectEditor</span><span style="color: blue;">:</span><span style="color: #a31515;">ObjectEditor</span> <span style="color: red;">Value</span><span style="color: blue;">=</span><span style="color: black;">"</span><span style="color: blue;">{Binding}</span><span style="color: black;">"</span> <span style="color: red;">IsReadOnly</span><span style="color: blue;">=</span><span style="color: black;">"</span><span style="color: blue;">False</span><span style="color: black;">"</span><span style="color: blue;">/&gt;</span>
<span style="color: blue;">&lt;/</span><span style="color: #a31515;">UserControl</span><span style="color: blue;">&gt;</span>
</pre>
</div>
<p><br />Picture 6 shows the form&rsquo;s view.<br /><br /></p>
<blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote><img title="6.png" src="http://download-codeplex.sec.s-msft.com/Download?ProjectName=objecteditor&amp;DownloadId=894206" alt="6.png" /></blockquote>
</blockquote>
</blockquote>
</blockquote>
</blockquote>
</blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote>Picture 6.</blockquote>
</blockquote>
</blockquote>
</blockquote>
</blockquote>
</blockquote>
</blockquote>
</blockquote>
</blockquote>
<p>Set a user template instead of the Default template. The changed XAML has the code:<br /><br /></p>
<div style="color: black; background-color: white;">
<pre><span style="color: blue;">&lt;</span><span style="color: #a31515;">UserControl</span> <span style="color: red;">...</span><span style="color: blue;">&gt;</span>
    <span style="color: blue;">&lt;</span><span style="color: #a31515;">UserControl.Resources</span><span style="color: blue;">&gt;</span>
        <span style="color: blue;">&lt;</span><span style="color: #a31515;">DataTemplate</span> <span style="color: red;">x:Key</span><span style="color: blue;">=</span><span style="color: black;">"</span><span style="color: blue;">BitmapImage</span><span style="color: black;">"</span><span style="color: blue;">&gt;</span>
            <span style="color: blue;">&lt;</span><span style="color: #a31515;">Image</span> <span style="color: red;">Source</span><span style="color: blue;">=</span><span style="color: black;">"</span><span style="color: blue;">{Binding Value}</span><span style="color: black;">"</span> <span style="color: red;">Width</span><span style="color: blue;">=</span><span style="color: black;">"</span><span style="color: blue;">100</span><span style="color: black;">"</span><span style="color: blue;">/&gt;</span>
        <span style="color: blue;">&lt;/</span><span style="color: #a31515;">DataTemplate</span><span style="color: blue;">&gt;</span>
    <span style="color: blue;">&lt;/</span><span style="color: #a31515;">UserControl.Resources</span><span style="color: blue;">&gt;</span>

    <span style="color: blue;">&lt;</span><span style="color: #a31515;">ObjectEditor</span><span style="color: blue;">:</span><span style="color: #a31515;">ObjectEditor</span> <span style="color: red;">Value</span><span style="color: blue;">=</span><span style="color: black;">"</span><span style="color: blue;">{Binding}</span><span style="color: black;">"</span> <span style="color: red;">Templates</span><span style="color: blue;">=</span><span style="color: black;">"</span><span style="color: blue;">BitmapImage</span><span style="color: black;">"</span> <span style="color: red;">IsReadOnly</span><span style="color: blue;">=</span><span style="color: black;">"</span><span style="color: blue;">False</span><span style="color: black;">"</span><span style="color: blue;">/&gt;</span>
<span style="color: blue;">&lt;/</span><span style="color: #a31515;">UserControl</span><span style="color: blue;">&gt;</span>
</pre>
</div>
<p><br />Picture 7 shows the form&rsquo;s view with the changes.<br /><br /></p>
<blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote><img title="7.png" src="http://download-codeplex.sec.s-msft.com/Download?ProjectName=objecteditor&amp;DownloadId=894207" alt="7.png" /></blockquote>
</blockquote>
</blockquote>
</blockquote>
</blockquote>
</blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote>Picture 7.</blockquote>
</blockquote>
</blockquote>
</blockquote>
</blockquote>
</blockquote>
</blockquote>
</blockquote>
</blockquote>
<p>If you want to define template for any data type you need to add DataTemplate and set the name of type as a key (or name of type + ReadOnly for the read-only mode). Then you need to add this key to Templates property of the Object Editor, this way you will save time on useless datatemplate search (FindResource).<br /><br />There is a way to avoid using Templates property &ndash; you can define templates in the Object Editor resources:<br /><br /></p>
<div style="color: black; background-color: white;">
<pre><span style="color: blue;">&lt;</span><span style="color: #a31515;">UserControl</span> <span style="color: red;">...</span><span style="color: blue;">&gt;</span>
    <span style="color: blue;">&lt;</span><span style="color: #a31515;">ObjectEditor</span><span style="color: blue;">:</span><span style="color: #a31515;">ObjectEditor</span> <span style="color: red;">Value</span><span style="color: blue;">=</span><span style="color: black;">"</span><span style="color: blue;">{Binding}</span><span style="color: black;">"</span> <span style="color: red;">IsReadOnly</span><span style="color: blue;">=</span><span style="color: black;">"</span><span style="color: blue;">False</span><span style="color: black;">"</span><span style="color: blue;">&gt;</span>
        <span style="color: blue;">&lt;</span><span style="color: #a31515;">ObjectEditor</span><span style="color: blue;">:</span><span style="color: #a31515;">ObjectEditor.Resources</span><span style="color: blue;">&gt;</span>
            <span style="color: blue;">&lt;</span><span style="color: #a31515;">DataTemplate</span> <span style="color: red;">x:Key</span><span style="color: blue;">=</span><span style="color: black;">"</span><span style="color: blue;">BitmapImage</span><span style="color: black;">"</span><span style="color: blue;">&gt;</span>
                <span style="color: blue;">&lt;</span><span style="color: #a31515;">Image</span> <span style="color: red;">Source</span><span style="color: blue;">=</span><span style="color: black;">"</span><span style="color: blue;">{Binding Value}</span><span style="color: black;">"</span> <span style="color: red;">Width</span><span style="color: blue;">=</span><span style="color: black;">"</span><span style="color: blue;">100</span><span style="color: black;">"</span><span style="color: blue;">/&gt;</span>
            <span style="color: blue;">&lt;/</span><span style="color: #a31515;">DataTemplate</span><span style="color: blue;">&gt;</span>
        <span style="color: blue;">&lt;/</span><span style="color: #a31515;">ObjectEditor</span><span style="color: blue;">:</span><span style="color: #a31515;">ObjectEditor.Resources</span><span style="color: blue;">&gt;</span>
    <span style="color: blue;">&lt;/</span><span style="color: #a31515;">ObjectEditor</span><span style="color: blue;">:</span><span style="color: #a31515;">ObjectEditor</span><span style="color: blue;">&gt;</span>
<span style="color: blue;">&lt;/</span><span style="color: #a31515;">UserControl</span><span style="color: blue;">&gt;</span>
</pre>
</div>
<p><br />This way was chosen to strictly separate editable and read-only templates and to avoid program crashes in case of using TwoWay Binding for read-only property.<br /><br />There are templates that are defined by default:</p>
<ul>
<li>Boolean (BooleanReadOnly)</li>
<li>String (StringReadOnly)</li>
<li>Single (SingleReadOnly)</li>
<li>Double (DoubleReadOnly)</li>
<li>Int32 (Int32ReadOnly)</li>
</ul>
<p>Enum type has special templates that can be redefined:</p>
<ul>
<li>EnumValue (EnumValueReadOnly)</li>
<li>FlagsValue (FlagsValueReadOnly)</li>
</ul>
<p><br />For example, if you want to override enum&rsquo;s template it&rsquo;s enough to define template and set &ldquo;EnumValue&rdquo; as a key:<br /><br /></p>
<div style="color: black; background-color: white;">
<pre><span style="color: blue;">&lt;</span><span style="color: #a31515;">UserControl</span> <span style="color: red;">...</span><span style="color: blue;">&gt;</span>
    <span style="color: blue;">&lt;</span><span style="color: #a31515;">UserControl.Resources</span><span style="color: blue;">&gt;</span>
        <span style="color: blue;">&lt;</span><span style="color: #a31515;">DataTemplate</span> <span style="color: red;">x:Key</span><span style="color: blue;">=</span><span style="color: black;">"</span><span style="color: blue;">EnumValue</span><span style="color: black;">"</span><span style="color: blue;">&gt;</span>
            <span style="color: blue;">&lt;</span><span style="color: #a31515;">ListBox</span> <span style="color: red;">ItemsSource</span><span style="color: blue;">=</span><span style="color: black;">"</span><span style="color: blue;">{Binding Items}</span><span style="color: black;">"</span> <span style="color: red;">SelectedItem</span><span style="color: blue;">=</span><span style="color: black;">"</span><span style="color: blue;">{Binding Value}</span><span style="color: black;">"</span><span style="color: blue;">/&gt;</span>
        <span style="color: blue;">&lt;/</span><span style="color: #a31515;">DataTemplate</span><span style="color: blue;">&gt;</span>
    <span style="color: blue;">&lt;/</span><span style="color: #a31515;">UserControl.Resources</span><span style="color: blue;">&gt;</span>

    <span style="color: blue;">&lt;</span><span style="color: #a31515;">ObjectEditor</span><span style="color: blue;">:</span><span style="color: #a31515;">ObjectEditor</span> <span style="color: red;">Value</span><span style="color: blue;">=</span><span style="color: black;">"</span><span style="color: blue;">{Binding}</span><span style="color: black;">"</span> <span style="color: red;">Templates</span><span style="color: blue;">=</span><span style="color: black;">"</span><span style="color: blue;">EnumValue</span><span style="color: black;">"</span> <span style="color: red;">IsReadOnly</span><span style="color: blue;">=</span><span style="color: black;">"</span><span style="color: blue;">False</span><span style="color: black;">"</span><span style="color: blue;">/&gt;</span>
<span style="color: blue;">&lt;/</span><span style="color: #a31515;">UserControl</span><span style="color: blue;">&gt;</span>
</pre>
</div>
<p><br />Picture 8 shows the form&rsquo;s view with enum&rsquo;s template.<br /><br /></p>
<blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote><img title="8.png" src="http://download-codeplex.sec.s-msft.com/Download?ProjectName=objecteditor&amp;DownloadId=894210" alt="8.png" /></blockquote>
</blockquote>
</blockquote>
</blockquote>
</blockquote>
</blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote>Picture 8.</blockquote>
</blockquote>
</blockquote>
</blockquote>
</blockquote>
</blockquote>
</blockquote>
</blockquote>
</blockquote>
<p>If there is no defined template for edited object, default template will be used.<br />For example, you have the class:<br /><br /></p>
<div style="color: black; background-color: white;">
<pre><span style="color: blue;">public</span> <span style="color: blue;">enum</span> MyEnum
{
	Enum1,
	Enum2
}

<span style="color: blue;">public</span> <span style="color: blue;">class</span> ListClass
{
	[ShowInView]
	<span style="color: blue;">public</span> <span style="color: blue;">string</span> Text { <span style="color: blue;">get</span>; <span style="color: blue;">set</span>; }

	[ShowInView
	<span style="color: blue;">public</span> MyFlags EditFlag { <span style="color: blue;">get</span>; <span style="color: blue;">set</span>; }

	<span style="color: blue;">public</span> ListClass()
	{
	}
}

<span style="color: blue;">public</span> <span style="color: blue;">class</span> TestPresenter : NotificationObject
{
	[ShowInView]
	<span style="color: blue;">public</span> ListClass Class { <span style="color: blue;">get</span>; <span style="color: blue;">set</span>; }

	[ShowInView]
	<span style="color: blue;">public</span> List&lt;ListClass&gt; ClassList { <span style="color: blue;">get</span>; <span style="color: blue;">set</span>; }

	<span style="color: blue;">public</span> TestPresenter()
	{
		Class = <span style="color: blue;">new</span> ListClass { Text = <span style="color: #a31515;">"Class"</span>, EditFlag = MyFlags.Flag1 };

		ClassList = <span style="color: blue;">new</span> List&lt;ListClass&gt;
		{
			<span style="color: blue;">new</span> ListClass { Text = <span style="color: #a31515;">"Class 1"</span>, EditFlag = MyFlags.Flag1 },
			<span style="color: blue;">new</span> ListClass { Text = <span style="color: #a31515;">"Class 2"</span>, EditFlag = MyFlags.Flag2 }
		};
	}
}
</pre>
</div>
<p><br />XAML has the code:<br /><br /></p>
<div style="color: black; background-color: white;">
<pre><span style="color: blue;">&lt;</span><span style="color: #a31515;">UserControl</span> <span style="color: red;">...</span><span style="color: blue;">&gt;</span>
    <span style="color: blue;">&lt;</span><span style="color: #a31515;">ObjectEditor</span><span style="color: blue;">:</span><span style="color: #a31515;">ObjectEditor</span> <span style="color: red;">Value</span><span style="color: blue;">=</span><span style="color: black;">"</span><span style="color: blue;">{Binding}</span><span style="color: black;">"</span> <span style="color: red;">IsReadOnly</span><span style="color: blue;">=</span><span style="color: black;">"</span><span style="color: blue;">False</span><span style="color: black;">"</span><span style="color: blue;">/&gt;</span>
<span style="color: blue;">&lt;/</span><span style="color: #a31515;">UserControl</span><span style="color: blue;">&gt;</span>
</pre>
</div>
<p><br />Picture 9 shows the form&rsquo;s view for this class.<br /><br /></p>
<blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote><img title="9.png" src="http://download-codeplex.sec.s-msft.com/Download?ProjectName=objecteditor&amp;DownloadId=894211" alt="9.png" /></blockquote>
</blockquote>
</blockquote>
</blockquote>
</blockquote>
</blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote>Picture 9.</blockquote>
</blockquote>
</blockquote>
</blockquote>
</blockquote>
</blockquote>
</blockquote>
</blockquote>
</blockquote>
<p>By default, such objects are displayed as collapsed items to avoid recursive building of tree properties because it takes long time.<br /><br />Picture 10 shows the form&rsquo;s view with expanded items.<br /><br /></p>
<blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote><img title="10.png" src="http://download-codeplex.sec.s-msft.com/Download?ProjectName=objecteditor&amp;DownloadId=894212" alt="10.png" /></blockquote>
</blockquote>
</blockquote>
</blockquote>
</blockquote>
</blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote>Picture 10</blockquote>
</blockquote>
</blockquote>
</blockquote>
</blockquote>
</blockquote>
</blockquote>
</blockquote>
</blockquote>
<p>To change it, you need to define a template for such objects.<br /><br />In some cases, you need to change default displaying composition, but it&rsquo;s not enough to define user templates. For example, you want to change the following class composition:<br /><br /></p>
<div style="color: black; background-color: white;">
<pre><span style="color: blue;">public</span> <span style="color: blue;">class</span> TestPresenter : NotificationObject
{
	[ShowInView]
	<span style="color: blue;">public</span> List&lt;<span style="color: blue;">string</span>&gt; StringList { <span style="color: blue;">get</span>; <span style="color: blue;">set</span>; }

	<span style="color: blue;">public</span> TestPresenter()
	{
		StringList = <span style="color: blue;">new</span> List&lt;<span style="color: blue;">string</span>&gt; { <span style="color: #a31515;">"text 1"</span>, <span style="color: #a31515;">"text 2"</span> };
	}
}
</pre>
</div>
<p><br />XAML has the code:<br /><br /></p>
<div style="color: black; background-color: white;">
<pre><span style="color: blue;">&lt;</span><span style="color: #a31515;">UserControl</span> <span style="color: red;">...</span><span style="color: blue;">&gt;</span>
    <span style="color: blue;">&lt;</span><span style="color: #a31515;">ObjectEditor</span><span style="color: blue;">:</span><span style="color: #a31515;">ObjectEditor</span> <span style="color: red;">Value</span><span style="color: blue;">=</span><span style="color: black;">"</span><span style="color: blue;">{Binding}</span><span style="color: black;">"</span> <span style="color: red;">IsReadOnly</span><span style="color: blue;">=</span><span style="color: black;">"</span><span style="color: blue;">True</span><span style="color: black;">"</span><span style="color: blue;">/&gt;</span>
<span style="color: blue;">&lt;/</span><span style="color: #a31515;">UserControl</span><span style="color: blue;">&gt;</span>
</pre>
</div>
<p><br />Picture 11 shows the form&rsquo;s view with default data template.<br /><br /></p>
<blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote><img title="11.png" src="http://download-codeplex.sec.s-msft.com/Download?ProjectName=objecteditor&amp;DownloadId=894213" alt="11.png" /></blockquote>
</blockquote>
</blockquote>
</blockquote>
</blockquote>
</blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote>Picture 11</blockquote>
</blockquote>
</blockquote>
</blockquote>
</blockquote>
</blockquote>
</blockquote>
</blockquote>
</blockquote>
<p>Class CollectionValue is responsible for the list displaying and you need to define new template in this case:<br /><br /></p>
<div style="color: black; background-color: white;">
<pre><span style="color: blue;">&lt;</span><span style="color: #a31515;">UserControl</span> <span style="color: red;">...</span><span style="color: blue;">&gt;</span> 
    <span style="color: blue;">&lt;</span><span style="color: #a31515;">UserControl.Resources</span><span style="color: blue;">&gt;</span>
        <span style="color: blue;">&lt;</span><span style="color: #a31515;">Style</span> <span style="color: red;">x:Key</span><span style="color: blue;">=</span><span style="color: black;">"</span><span style="color: blue;">LabelStyle</span><span style="color: black;">"</span><span style="color: blue;">&gt;</span>
            <span style="color: blue;">&lt;</span><span style="color: #a31515;">Setter</span> <span style="color: red;">Property</span><span style="color: blue;">=</span><span style="color: black;">"</span><span style="color: blue;">TextBlock.VerticalAlignment</span><span style="color: black;">"</span> <span style="color: red;">Value</span><span style="color: blue;">=</span><span style="color: black;">"</span><span style="color: blue;">Center</span><span style="color: black;">"</span><span style="color: blue;">/&gt;</span>
            <span style="color: blue;">&lt;</span><span style="color: #a31515;">Setter</span> <span style="color: red;">Property</span><span style="color: blue;">=</span><span style="color: black;">"</span><span style="color: blue;">TextBlock.FontWeight</span><span style="color: black;">"</span> <span style="color: red;">Value</span><span style="color: blue;">=</span><span style="color: black;">"</span><span style="color: blue;">Bold</span><span style="color: black;">"</span><span style="color: blue;">/&gt;</span>
            <span style="color: blue;">&lt;</span><span style="color: #a31515;">Setter</span> <span style="color: red;">Property</span><span style="color: blue;">=</span><span style="color: black;">"</span><span style="color: blue;">TextBlock.Margin</span><span style="color: black;">"</span> <span style="color: red;">Value</span><span style="color: blue;">=</span><span style="color: black;">"</span><span style="color: blue;">5,5,5,5</span><span style="color: black;">"</span><span style="color: blue;">/&gt;</span>
        <span style="color: blue;">&lt;/</span><span style="color: #a31515;">Style</span><span style="color: blue;">&gt;</span>
        <span style="color: blue;">&lt;</span><span style="color: #a31515;">DataTemplate</span> <span style="color: red;">DataType</span><span style="color: blue;">=</span><span style="color: black;">"</span><span style="color: blue;">{x:Type ObjectEditor:CollectionValue}</span><span style="color: black;">"</span><span style="color: blue;">&gt;</span>
            <span style="color: blue;">&lt;</span><span style="color: #a31515;">StackPanel</span><span style="color: blue;">&gt;</span>
                <span style="color: blue;">&lt;</span><span style="color: #a31515;">TextBlock</span> <span style="color: red;">Text</span><span style="color: blue;">=</span><span style="color: black;">"</span><span style="color: blue;">{Binding}</span><span style="color: black;">"</span> <span style="color: red;">Style</span><span style="color: blue;">=</span><span style="color: black;">"</span><span style="color: blue;">{StaticResource LabelStyle}</span><span style="color: black;">"</span><span style="color: blue;">/&gt;</span>

                <span style="color: blue;">&lt;</span><span style="color: #a31515;">ListBox</span> <span style="color: red;">ItemsSource</span><span style="color: blue;">=</span><span style="color: black;">"</span><span style="color: blue;">{Binding Items}</span><span style="color: black;">"</span><span style="color: blue;">/&gt;</span>
            <span style="color: blue;">&lt;/</span><span style="color: #a31515;">StackPanel</span><span style="color: blue;">&gt;</span>
        <span style="color: blue;">&lt;/</span><span style="color: #a31515;">DataTemplate</span><span style="color: blue;">&gt;</span>
    <span style="color: blue;">&lt;/</span><span style="color: #a31515;">UserControl.Resources</span><span style="color: blue;">&gt;</span>

    <span style="color: blue;">&lt;</span><span style="color: #a31515;">ObjectEditor</span><span style="color: blue;">:</span><span style="color: #a31515;">ObjectEditor</span> <span style="color: red;">Value</span><span style="color: blue;">=</span><span style="color: black;">"</span><span style="color: blue;">{Binding}</span><span style="color: black;">"</span> <span style="color: red;">IsReadOnly</span><span style="color: blue;">=</span><span style="color: black;">"</span><span style="color: blue;">True</span><span style="color: black;">"</span><span style="color: blue;">/&gt;</span>
<span style="color: blue;">&lt;/</span><span style="color: #a31515;">UserControl</span><span style="color: blue;">&gt;</span>
</pre>
</div>
<p><br />Picture 12 shows the form&rsquo;s view with redefined template.<br /><br /></p>
<blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote><img title="12.png" src="http://download-codeplex.sec.s-msft.com/Download?ProjectName=objecteditor&amp;DownloadId=894214" alt="12.png" /></blockquote>
</blockquote>
</blockquote>
</blockquote>
</blockquote>
</blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote>Picture 12</blockquote>
</blockquote>
</blockquote>
</blockquote>
</blockquote>
</blockquote>
</blockquote>
</blockquote>
</blockquote>
<p>There are visual classes that you can also redefine:</p>
<ul>
<li>AbstractValue &ndash; base class to edit any property of object.</li>
<li>CollectionValue &ndash; class to edit a list property.</li>
<li>ObjectEditor &ndash; the main class to edit object.&emsp;</li>
</ul>
<p><br /><br /></p>
<h2>Other features</h2>
<p>Also, you can specify a validator and order of displayed properties:<br /><br /></p>
<div style="color: black; background-color: white;">
<pre>[ShowInView, Order(80), IntValidator(0, 10)]
<span style="color: blue;">public</span> <span style="color: blue;">string</span> Name { <span style="color: blue;">get</span>; <span style="color: blue;">set</span>; }
</pre>
</div>
<p><br />Validator must be inherited from the ValidatorAttribute class and overrides Rule property:<br /><br /></p>
<div style="color: black; background-color: white;">
<pre><span style="color: blue;">public</span> <span style="color: blue;">class</span> ValidatorAttribute : Attribute
{
	<span style="color: blue;">public</span> <span style="color: blue;">virtual</span> ValidationRule Rule { <span style="color: blue;">get</span>; <span style="color: blue;">private</span> <span style="color: blue;">set</span>; }

	<span style="color: blue;">public</span> ValidatorAttribute()
	{
		;
	}
}

<span style="color: blue;">public</span> <span style="color: blue;">class</span> IntRule : ValidationRule
{
    <span style="color: blue;">public</span> <span style="color: blue;">int</span> Max { <span style="color: blue;">get</span>; <span style="color: blue;">set</span>; }
    <span style="color: blue;">public</span> <span style="color: blue;">int</span> Min { <span style="color: blue;">get</span>; <span style="color: blue;">set</span>; }

    <span style="color: blue;">public</span> IntRule()
    {
        Min = <span style="color: blue;">int</span>.MinValue;
        Max = <span style="color: blue;">int</span>.MaxValue;
    }

    <span style="color: blue;">public</span> <span style="color: blue;">override</span> ValidationResult Validate(<span style="color: blue;">object</span> value, CultureInfo cultureInfo)
    {
        <span style="color: blue;">int</span> intValue = 0;
        
        <span style="color: blue;">try</span>
        {
            <span style="color: blue;">if</span> (!<span style="color: blue;">string</span>.IsNullOrWhiteSpace((<span style="color: blue;">string</span>)value))
            {
                intValue = <span style="color: blue;">int</span>.Parse((<span style="color: blue;">string</span>)value, cultureInfo);
            }
        }
        <span style="color: blue;">catch</span>
        {
            <span style="color: blue;">return</span> <span style="color: blue;">new</span> ValidationResult(<span style="color: blue;">false</span>, <span style="color: #a31515;">"not correct value"</span>);
        }
        <span style="color: blue;">if</span> (intValue &lt; Min || intValue &gt; Max)
        {
            <span style="color: blue;">return</span> <span style="color: blue;">new</span> ValidationResult(<span style="color: blue;">false</span>, <span style="color: blue;">string</span>.Format(<span style="color: #a31515;">"value is not in range [{0}..{1}]"</span>, Min, Max));
        }
        <span style="color: blue;">else</span>
        {
            <span style="color: blue;">return</span> <span style="color: blue;">new</span> ValidationResult(<span style="color: blue;">true</span>, <span style="color: blue;">null</span>);
        }
    }
}

<span style="color: blue;">public</span> <span style="color: blue;">class</span> IntValidator : ValidatorAttribute
{
	<span style="color: blue;">public</span> <span style="color: blue;">int</span> Max { <span style="color: blue;">get</span>; <span style="color: blue;">set</span>; }
	<span style="color: blue;">public</span> <span style="color: blue;">int</span> Min { <span style="color: blue;">get</span>; <span style="color: blue;">set</span>; }

	<span style="color: blue;">public</span> <span style="color: blue;">override</span> ValidationRule Rule
	{
		<span style="color: blue;">get</span> { <span style="color: blue;">return</span> <span style="color: blue;">new</span> IntRule { Min = Min, Max = Max }; }
	}

	<span style="color: blue;">public</span> IntValidator()
	{
		Min = <span style="color: blue;">int</span>.MinValue;
		Max = <span style="color: blue;">int</span>.MaxValue;
	}

	<span style="color: blue;">public</span> IntValidator(<span style="color: blue;">int</span> min, <span style="color: blue;">int</span> max)
	{
		Min = min;
		Max = max;
	}
}

[ShowInView, Order(4), IntValidator(0, 10)]
<span style="color: blue;">public</span> <span style="color: blue;">int</span> Number { <span style="color: blue;">get</span>; <span style="color: blue;">set</span>; }
</pre>
</div>
<p><br />This way was chosen because there is no possibility to use universal attribute to any validation rule and you can&rsquo;t set an object as a constructor parameter in attributes (exception: an attribute argument must be a constant expression, typeof expression or array creation expression of an attribute parameter type).<br /><br />Default templates use validation attribute automatically. If you want to use this feature in your templates you need to use ValidatorBinding instead of Binding: <br /><br /></p>
<div style="color: black; background-color: white;">
<pre><span style="color: blue;">&lt;</span><span style="color: #a31515;">UserControl</span> <span style="color: red;">...</span><span style="color: blue;">&gt;</span>
    <span style="color: blue;">&lt;</span><span style="color: #a31515;">UserControl.Resources</span><span style="color: blue;">&gt;</span>
        <span style="color: blue;">&lt;</span><span style="color: #a31515;">DataTemplate</span> <span style="color: red;">x:Key</span><span style="color: blue;">=</span><span style="color: black;">"</span><span style="color: blue;">String</span><span style="color: black;">"</span><span style="color: blue;">&gt;</span>
            <span style="color: blue;">&lt;</span><span style="color: #a31515;">TextBlock</span> <span style="color: red;">Text</span><span style="color: blue;">=</span><span style="color: black;">"</span><span style="color: blue;">{ObjectEditor:ValidatorBinding Value}</span><span style="color: black;">"</span> <span style="color: red;">FontWeight</span><span style="color: blue;">=</span><span style="color: black;">"</span><span style="color: blue;">Bold</span><span style="color: black;">"</span>
                       <span style="color: red;">HorizontalAlignment</span><span style="color: blue;">=</span><span style="color: black;">"</span><span style="color: blue;">Center</span><span style="color: black;">"</span><span style="color: blue;">/&gt;</span>
        <span style="color: blue;">&lt;/</span><span style="color: #a31515;">DataTemplate</span><span style="color: blue;">&gt;</span>
    <span style="color: blue;">&lt;/</span><span style="color: #a31515;">UserControl.Resources</span><span style="color: blue;">&gt;</span>

   ...

<span style="color: blue;">&lt;/</span><span style="color: #a31515;">UserControl</span><span style="color: blue;">&gt;</span>
</pre>
</div>
<p><br />ShowAttributes property allows separate properties and displays each group into a different view. For example:<br /><br /></p>
<div style="color: black; background-color: white;">
<pre><span style="color: blue;">public</span> <span style="color: blue;">class</span> TestPresenter : NotificationObject
{
	[ErrorMessage]
	<span style="color: blue;">public</span> <span style="color: blue;">string</span> ErrorText1 { <span style="color: blue;">get</span>; <span style="color: blue;">set</span>; }

	[ErrorMessage]
	<span style="color: blue;">public</span> <span style="color: blue;">string</span> ErrorText2 { <span style="color: blue;">get</span>; <span style="color: blue;">set</span>; }

	[Params]
	<span style="color: blue;">public</span> <span style="color: blue;">int</span> Param1 { <span style="color: blue;">get</span>; <span style="color: blue;">set</span>; }

	[Params]
	<span style="color: blue;">public</span> <span style="color: blue;">int</span> Param2 { <span style="color: blue;">get</span>; <span style="color: blue;">set</span>; }

	<span style="color: blue;">public</span> TestPresenter()
	{
		ErrorText1 = <span style="color: #a31515;">"Error 1"</span>;
		ErrorText2 = <span style="color: #a31515;">"Error 2"</span>;
		Param1 = 100;
		Param2 = 200;
	}
}
</pre>
</div>
<p><br />You can separate displaying parameters and errors for this class:<br /><br /></p>
<div style="color: black; background-color: white;">
<pre><span style="color: blue;">&lt;</span><span style="color: #a31515;">UserControl</span> <span style="color: red;">...</span><span style="color: blue;">&gt;</span>
    <span style="color: blue;">&lt;</span><span style="color: #a31515;">UserControl.Resources</span><span style="color: blue;">&gt;</span>
        <span style="color: blue;">&lt;</span><span style="color: #a31515;">DataTemplate</span> <span style="color: red;">x:Key</span><span style="color: blue;">=</span><span style="color: black;">"</span><span style="color: blue;">StringReadOnly</span><span style="color: black;">"</span><span style="color: blue;">&gt;</span>
            <span style="color: blue;">&lt;</span><span style="color: #a31515;">TextBlock</span> <span style="color: red;">Text</span><span style="color: blue;">=</span><span style="color: black;">"</span><span style="color: blue;">{Binding Value}</span><span style="color: black;">"</span> <span style="color: red;">Foreground</span><span style="color: blue;">=</span><span style="color: black;">"</span><span style="color: blue;">Red</span><span style="color: black;">"</span>
                       <span style="color: red;">FontWeight</span><span style="color: blue;">=</span><span style="color: black;">"</span><span style="color: blue;">Bold</span><span style="color: black;">"</span> <span style="color: red;">HorizontalAlignment</span><span style="color: blue;">=</span><span style="color: black;">"</span><span style="color: blue;">Center</span><span style="color: black;">"</span><span style="color: blue;">/&gt;</span>
        <span style="color: blue;">&lt;/</span><span style="color: #a31515;">DataTemplate</span><span style="color: blue;">&gt;</span>
    <span style="color: blue;">&lt;/</span><span style="color: #a31515;">UserControl.Resources</span><span style="color: blue;">&gt;</span>

    <span style="color: blue;">&lt;</span><span style="color: #a31515;">Grid</span><span style="color: blue;">&gt;</span>
        <span style="color: blue;">&lt;</span><span style="color: #a31515;">Grid.RowDefinitions</span><span style="color: blue;">&gt;</span>
            <span style="color: blue;">&lt;</span><span style="color: #a31515;">RowDefinition</span><span style="color: blue;">/&gt;</span>
            <span style="color: blue;">&lt;</span><span style="color: #a31515;">RowDefinition</span> <span style="color: red;">Height</span><span style="color: blue;">=</span><span style="color: black;">"</span><span style="color: blue;">Auto</span><span style="color: black;">"</span><span style="color: blue;">/&gt;</span>
            <span style="color: blue;">&lt;</span><span style="color: #a31515;">RowDefinition</span><span style="color: blue;">/&gt;</span>
        <span style="color: blue;">&lt;/</span><span style="color: #a31515;">Grid.RowDefinitions</span><span style="color: blue;">&gt;</span>

        <span style="color: blue;">&lt;</span><span style="color: #a31515;">ObjectEditor</span><span style="color: blue;">:</span><span style="color: #a31515;">ObjectEditor</span> <span style="color: red;">Value</span><span style="color: blue;">=</span><span style="color: black;">"</span><span style="color: blue;">{Binding}</span><span style="color: black;">"</span> <span style="color: red;">IsReadOnly</span><span style="color: blue;">=</span><span style="color: black;">"</span><span style="color: blue;">False</span><span style="color: black;">"</span> <span style="color: red;">ShowAttributes</span><span style="color: blue;">=</span><span style="color: black;">"</span><span style="color: blue;">Params</span><span style="color: black;">"</span><span style="color: blue;">/&gt;</span>
        
        <span style="color: blue;">&lt;</span><span style="color: #a31515;">Button</span> <span style="color: red;">Grid.Row</span><span style="color: blue;">=</span><span style="color: black;">"</span><span style="color: blue;">1</span><span style="color: black;">"</span> <span style="color: red;">Padding</span><span style="color: blue;">=</span><span style="color: black;">"</span><span style="color: blue;">10,5,10,5</span><span style="color: black;">"</span> <span style="color: red;">HorizontalAlignment</span><span style="color: blue;">=</span><span style="color: black;">"</span><span style="color: blue;">Center</span><span style="color: black;">"</span><span style="color: blue;">&gt;</span>
            <span style="color: blue;">&lt;</span><span style="color: #a31515;">TextBlock</span> <span style="color: red;">Text</span><span style="color: blue;">=</span><span style="color: black;">"</span><span style="color: blue;">Button</span><span style="color: black;">"</span><span style="color: blue;">/&gt;</span>
        <span style="color: blue;">&lt;/</span><span style="color: #a31515;">Button</span><span style="color: blue;">&gt;</span>

        <span style="color: blue;">&lt;</span><span style="color: #a31515;">ObjectEditor</span><span style="color: blue;">:</span><span style="color: #a31515;">ObjectEditor</span> <span style="color: red;">Value</span><span style="color: blue;">=</span><span style="color: black;">"</span><span style="color: blue;">{Binding}</span><span style="color: black;">"</span> <span style="color: red;">Templates</span><span style="color: blue;">=</span><span style="color: black;">"</span><span style="color: blue;">StringReadOnly</span><span style="color: black;">"</span>
                                   <span style="color: red;">ShowAttributes</span><span style="color: blue;">=</span><span style="color: black;">"</span><span style="color: blue;">ErrorMessage</span><span style="color: black;">"</span> <span style="color: red;">Grid.Row</span><span style="color: blue;">=</span><span style="color: black;">"</span><span style="color: blue;">2</span><span style="color: black;">"</span><span style="color: blue;">/&gt;</span>

    <span style="color: blue;">&lt;/</span><span style="color: #a31515;">Grid</span><span style="color: blue;">&gt;</span>
<span style="color: blue;">&lt;/</span><span style="color: #a31515;">UserControl</span><span style="color: blue;">&gt;</span>
</pre>
</div>
<p><br />Picture 13 shows the form&rsquo;s view.<br /><br /></p>
<blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote><img title="13.png" src="http://download-codeplex.sec.s-msft.com/Download?ProjectName=objecteditor&amp;DownloadId=894215" alt="13.png" /></blockquote>
</blockquote>
</blockquote>
</blockquote>
</blockquote>
</blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote>Picture 13</blockquote>
</blockquote>
</blockquote>
</blockquote>
</blockquote>
</blockquote>
</blockquote>
</blockquote>
</blockquote>
<p>&nbsp;</p>
<h2>Localization</h2>
<p>Object Editor uses Localization Library to simplify localization process. This library keeps translations in xml-files.<br /><br />This library is small and simple in use. You can set translation path by XPath:<br /><br /></p>
<div style="color: black; background-color: white;">
<pre><span style="color: blue;">&lt;</span><span style="color: #a31515;">UserControl</span> <span style="color: red;">xmlns:Langlib</span><span style="color: blue;">=</span><span style="color: black;">"</span><span style="color: blue;">clr-namespace:LocalizationLibrary;assembly=LocalizationLibrary</span><span style="color: black;">"</span> <span style="color: red;">...</span><span style="color: blue;">&gt;</span> 
    <span style="color: blue;">&lt;</span><span style="color: #a31515;">TextBlock</span> <span style="color: red;">Text</span><span style="color: blue;">=</span><span style="color: black;">"</span><span style="color: blue;">{Langlib:Title Buttons/CloseButton}</span><span style="color: black;">"</span><span style="color: blue;">/&gt;</span>
    <span style="color: blue;">&lt;</span><span style="color: #a31515;">TextBlock</span> <span style="color: red;">Text</span><span style="color: blue;">=</span><span style="color: black;">"</span><span style="color: blue;">{Langlib:Title XPath=Buttons/CloseButton}</span><span style="color: black;">"</span><span style="color: blue;">/&gt;</span>
<span style="color: blue;">&lt;/</span><span style="color: #a31515;">UserControl</span><span style="color: blue;">&gt;</span>
</pre>
</div>
<p><br />There are a few available functions to use in LocalizationLibrary:</p>
<ul>
<li>static string[] GetLangs(string path = null) &ndash; return list of available languages.</li>
<li>static void SetLang(string lang) &ndash; set current language.</li>
<li>static string GetTitle(string xpath) &ndash; get translation by XPath.</li>
<li>static string GetTitle(string xpath, string defaultTitle) &ndash; get translation by XPath with default value.</li>
</ul>
<p><br />XML-files must have this kind of structure:<br /><br /></p>
<div style="color: black; background-color: white;">
<pre><span style="color: blue;">&lt;?</span><span style="color: #a31515;">xml</span> <span style="color: red;">version</span><span style="color: blue;">=</span><span style="color: black;">"</span><span style="color: blue;">1.0</span><span style="color: black;">"</span> <span style="color: red;">encoding</span><span style="color: blue;">=</span><span style="color: black;">"</span><span style="color: blue;">utf-8</span><span style="color: black;">"</span> <span style="color: blue;">?&gt;</span>
<span style="color: blue;">&lt;</span><span style="color: #a31515;">LanguagesResource</span> <span style="color: red;">culture</span><span style="color: blue;">=</span><span style="color: black;">"</span><span style="color: blue;">en-GB</span><span style="color: black;">"</span><span style="color: blue;">&gt;</span>
    <span style="color: blue;">&lt;</span><span style="color: #a31515;">Buttons</span><span style="color: blue;">&gt;</span>
        <span style="color: blue;">&lt;</span><span style="color: #a31515;">CloseButton</span><span style="color: blue;">&gt;</span>Close<span style="color: blue;">&lt;/</span><span style="color: #a31515;">CloseButton</span><span style="color: blue;">&gt;</span>
    <span style="color: blue;">&lt;/</span><span style="color: #a31515;">Buttons</span><span style="color: blue;">&gt;</span>
<span style="color: blue;">&lt;/</span><span style="color: #a31515;">LanguagesResource</span><span style="color: blue;">&gt;</span>
</pre>
</div>
<p><br />It contains LanguagesResource node with the culture attribute.<br />Translations files are kept in the Languages directory which is near the exe-file.<br />You can create a translation file per each language. The list of available languages will be created from the file names. For example, there are two translation files for English and Russian:<br /><br />EN.xml<br />RU.xml<br /><br />List of available languages will contain two elements: EN, RU which can be used to set current interface language by using SetLang function. <br />You can also create folders with translation files:<br /><br />EN\<br /> Tables.xml<br /> Dialogs.xml<br />RU\<br /> Tables.xml<br /> Dialogs.xml<br /><br />List of available languages will be created from folder names, as written above.<br /><br /><br /></p>
<h2>Automatic localization</h2>
<p>Object Editor uses LocalizationLibrary for automation and simplification of the localization process.<br /><br />Using the aforementioned example to show this feature:<br /><br /></p>
<div style="color: black; background-color: white;">
<pre><span style="color: blue;">public</span> <span style="color: blue;">class</span> TestPresenter : NotificationObject
{
	[ShowInView]
	<span style="color: blue;">public</span> <span style="color: blue;">string</span> Text { <span style="color: blue;">get</span>; <span style="color: blue;">set</span>; }

	[ShowInView]
	<span style="color: blue;">public</span> <span style="color: blue;">int</span> Int { <span style="color: blue;">get</span>; <span style="color: blue;">set</span>; }

	[ShowInView]
	<span style="color: blue;">public</span> <span style="color: blue;">string</span> IP { <span style="color: blue;">get</span>; <span style="color: blue;">set</span>; }

	<span style="color: blue;">public</span> TestPresenter()
	{
		Text = <span style="color: #a31515;">"text"</span>;
		Int = 100;
		IP = <span style="color: #a31515;">"127.0.0.1"</span>;
	}
}
</pre>
</div>
<p>&nbsp;</p>
<div style="color: black; background-color: white;">
<pre><span style="color: blue;">&lt;</span><span style="color: #a31515;">UserControl</span> <span style="color: red;">...</span><span style="color: blue;">&gt;</span>
    <span style="color: blue;">&lt;</span><span style="color: #a31515;">ObjectEditor</span><span style="color: blue;">:</span><span style="color: #a31515;">ObjectEditor</span> <span style="color: red;">Value</span><span style="color: blue;">=</span><span style="color: black;">"</span><span style="color: blue;">{Binding}</span><span style="color: black;">"</span> <span style="color: red;">IsReadOnly</span><span style="color: blue;">=</span><span style="color: black;">"</span><span style="color: blue;">False</span><span style="color: black;">"</span><span style="color: blue;">/&gt;</span>
<span style="color: blue;">&lt;/</span><span style="color: #a31515;">UserControl</span><span style="color: blue;">&gt;</span>
</pre>
</div>
<p><br />Picture 14 shows the form&rsquo;s view.<br /><br /></p>
<blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote><img title="14.png" src="http://download-codeplex.sec.s-msft.com/Download?ProjectName=objecteditor&amp;DownloadId=894216" alt="14.png" /></blockquote>
</blockquote>
</blockquote>
</blockquote>
</blockquote>
</blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote>Picture 14</blockquote>
</blockquote>
</blockquote>
</blockquote>
</blockquote>
</blockquote>
</blockquote>
</blockquote>
</blockquote>
<p>This interface can be localized by translation file which contains TestPresenter node with Test, Int, IP translating sections:<br /><br /></p>
<div style="color: black; background-color: white;">
<pre><span style="color: blue;">&lt;?</span><span style="color: #a31515;">xml</span> <span style="color: red;">version</span><span style="color: blue;">=</span><span style="color: black;">"</span><span style="color: blue;">1.0</span><span style="color: black;">"</span> <span style="color: red;">encoding</span><span style="color: blue;">=</span><span style="color: black;">"</span><span style="color: blue;">utf-8</span><span style="color: black;">"</span> <span style="color: blue;">?&gt;</span>
<span style="color: blue;">&lt;</span><span style="color: #a31515;">LanguagesResource</span> <span style="color: red;">culture</span><span style="color: blue;">=</span><span style="color: black;">"</span><span style="color: blue;">en-GB</span><span style="color: black;">"</span><span style="color: blue;">&gt;</span>
    <span style="color: blue;">&lt;</span><span style="color: #a31515;">TestPresenter</span><span style="color: blue;">&gt;</span>
        <span style="color: blue;">&lt;</span><span style="color: #a31515;">Test</span><span style="color: blue;">&gt;</span>Test<span style="color: blue;">&lt;/</span><span style="color: #a31515;">Test</span><span style="color: blue;">&gt;</span>
        <span style="color: blue;">&lt;</span><span style="color: #a31515;">Int</span><span style="color: blue;">&gt;</span>Int<span style="color: blue;">&lt;/</span><span style="color: #a31515;">Int</span><span style="color: blue;">&gt;</span>
        <span style="color: blue;">&lt;</span><span style="color: #a31515;">IP</span><span style="color: blue;">&gt;</span>IP<span style="color: blue;">&lt;/</span><span style="color: #a31515;">IP</span><span style="color: blue;">&gt;</span>
    <span style="color: blue;">&lt;/</span><span style="color: #a31515;">TestPresenter</span><span style="color: blue;">&gt;</span>
<span style="color: blue;">&lt;/</span><span style="color: #a31515;">LanguagesResource</span><span style="color: blue;">&gt;</span>
</pre>
</div>
<p>&nbsp;</p>
<blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote><img title="15.png" src="http://download-codeplex.sec.s-msft.com/Download?ProjectName=objecteditor&amp;DownloadId=894217" alt="15.png" /></blockquote>
</blockquote>
</blockquote>
</blockquote>
</blockquote>
</blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote>
<blockquote>Picture 15</blockquote>
</blockquote>
</blockquote>
</blockquote>
</blockquote>
</blockquote>
</blockquote>
</blockquote>
</blockquote>
<p>Object Editor uses the following algorithm to create a translation path: class name of editable object + / + property name.<br /><br />You can set any translation prefix to Object Editor by using property string TranslatePrefix.<br /><br />As a result, the full translation path is combined in the following order: <br />translate prefix (if it exists) + / + class name of editable object + / + property name.<br /><br />If there is no translation for the current language, the interface displays the full translation path which must be added to the translation file. This is well illustrated in all the above screenshots.</p>
