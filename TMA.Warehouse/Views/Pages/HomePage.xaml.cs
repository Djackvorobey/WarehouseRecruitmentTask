using System.Windows;
using System.Windows.Controls;
using TMA.Warehouse.ViewModels;

namespace TMA.Warehouse.Views.Pages
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        private readonly HomePageViewModel _homePageViewModel;

        public HomePage(HomePageViewModel homePageViewModel)
        {
            _homePageViewModel = homePageViewModel;
            InitializeComponent();

            Loaded += HomePage_Loaded;
        }

        private void HomePage_Loaded(object sender, RoutedEventArgs e)
        {
            _homePageViewModel.InitUser();

            _homePageViewModel.InitNavigation(NavigationService);

            DataContext = _homePageViewModel;
        }
    }
}
