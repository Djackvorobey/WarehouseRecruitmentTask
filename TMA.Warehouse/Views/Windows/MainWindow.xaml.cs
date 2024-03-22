using System.Windows;
using TMA.Warehouse.Views.Pages;

namespace TMA.Warehouse.Views.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(AuthenticationPage authenticationPage)
        {
            InitializeComponent();
            mainFrame.Navigate(authenticationPage);
        }
    }
}
