using System.Windows;
using ObjectEditorTest.Presenter;

namespace ObjectEditorTest.View
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new TestPresenter();
        }
    }
}
