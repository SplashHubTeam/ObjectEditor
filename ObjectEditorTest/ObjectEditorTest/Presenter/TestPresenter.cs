using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using LocalizationLibrary;
using ObjectEditor;

namespace ObjectEditorTest.Presenter
{
    public enum MyEnum
    {
        Enum1,
        Enum2
    }

    public class TestAttribute : Attribute
    {
    }

    [Flags]
    public enum MyFlags
    {
        FlagNone = 0,
        Flag1 = 1,
        Flag2 = 1 << 1
    }

    public class ListClass
    {
        [ShowInView, Order(1), Test]
        public string Text { get; set; }

        [ShowInView, Order(2)]
        public MyFlags EditFlag { get; set; }

        public ListClass()
        {
            Text = "test";
        }
    }

    public class ErrorMessage : Attribute { }
    public class Params : Attribute { }

    public class TestPresenter
    {
        private MyEnum _editEnum;
        private MyFlags _editFlag;

        [ShowInView, Order(1)]
        public Orientation Orientation { get; set; }

        [ShowInView, Order(2)]
        public Orientation ChildOrientation { get; set; }

        [ShowInView, Order(3)]
        public bool IsDescriptionVisible { get; set; }

        [ShowInView, Order(4)]
        public string Text { get; set; }

        [ShowInView, Order(5)]
        public List<string> StringList { get; set; }

        [ShowInView, Order(6)]
        public MyEnum EditEnum
        {
            get { return _editEnum; }
            private set
            {
                _editEnum = value;
                EnumList.Remove(EnumList.Last());
                EnumList.Add(value);
            }
        }

        [ShowInView, Order(7)]
        public ObservableCollection<MyEnum> EnumList { get; set; }

        [ShowInView, Order(8)]
        public MyFlags EditFlag
        {
            get { return _editFlag; }
            set { _editFlag = value; }
        }

        [ShowInView, Order(9)]
        public List<MyFlags> FlagsList { get; set; }

        [ShowInView, Order(10)]
        public int Int { get; set; }

        [ShowInView, Order(12)]
        public string IP { get; set; }


        [ShowInView, Order(11)]
        public List<int> IntList { get; set; }

        [ShowInView, Order(12)]
        public List<ListClass> ClassList { get; set; }

        [ShowInView, Order(0)]
        public ListClass Class { get; private set; }

        [ShowInView, Order(80)]
        public string Name { get; set; }


        public TestPresenter()
        {
            Lang.SetLang("EN");

            Name = "Test Name";

            Class = new ListClass { Text = "Class", EditFlag = MyFlags.Flag1 };

            Orientation = Orientation.Vertical;
            ChildOrientation = Orientation.Vertical;
            IsDescriptionVisible = true;

            Text = "text";
            IP = "127.0.0.1";

            _editFlag = MyFlags.Flag1 | MyFlags.Flag2;

            Int = 100;

            StringList = new List<string> { "text 1", "text 2" };
            EnumList = new ObservableCollection<MyEnum> { MyEnum.Enum1, MyEnum.Enum2 };
            FlagsList = new List<MyFlags> { MyFlags.Flag1, MyFlags.Flag2 };
            IntList = new List<int> { 10, 20 };
            ClassList = new List<ListClass>
			{
				new ListClass { Text = "Class 1", EditFlag = MyFlags.Flag1 },
				new ListClass { Text = "Class 2", EditFlag = MyFlags.Flag2 }
			};
        }
    }
}