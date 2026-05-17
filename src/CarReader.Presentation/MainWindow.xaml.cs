using System.Windows;
using CarReader.Presentation.ViewModels;

namespace CarReader.Presentation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new CarReaderViewModel();
        }
    }
}
