using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Navigation;
using TMA.Warehouse.Authenticate;
using TMA.Warehouse.Models;
using TMA.Warehouse.ViewModels.RelayCommands;
using TMA.Warehouse.Views.Pages;

namespace TMA.Warehouse.ViewModels
{
    public class HomePageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly IAuthenticator _authenticator;
        private readonly PurchaseRequestsPage _purchaseRequestsPage;
        private readonly GoodsPage _goodsPage;
        private readonly ManageAccountsPage _manageAccountsPage;
        public NavigationService _NavigationService { get; set; }

        private string _welcomeMessage;
        public string WelcomeMessage
        {
            get { return _welcomeMessage; }
            set
            {
                if (_welcomeMessage != value)
                {
                    _welcomeMessage = value;
                    OnPropertyChanged(nameof(WelcomeMessage));
                }
            }
        }

        private User _myUser;
        public User MyUser
        {
            get { return _myUser; }
            set
            {
                if (_myUser != value)
                {
                    _myUser = value;
                    OnPropertyChanged(nameof(MyUser));
                }
            }
        }

        public ICommand OrdersCommand { get; private set; }
        public ICommand GoodsCommand { get; private set; }
        public ICommand RolesCommand { get; private set; }
        public ICommand LogoutCommand { get; private set; }
        public HomePageViewModel(IAuthenticator authenticator,
                                 PurchaseRequestsPage purchaseRequestsPage,
                                 GoodsPage goodsPage,
                                 ManageAccountsPage manageAccountsPage)
        {
            _authenticator = authenticator;
            _purchaseRequestsPage = purchaseRequestsPage;
            _goodsPage = goodsPage;
            _manageAccountsPage = manageAccountsPage;

            OrdersCommand = new RelayCommand(Orders);
            GoodsCommand = new RelayCommand(Goods);
            RolesCommand = new RelayCommand(Roles);
            LogoutCommand = new RelayCommand(Logout);
        }
        public void InitNavigation(NavigationService navigationService)
        {
            _NavigationService = navigationService;
        }
        public void InitUser()
        {
            MyUser = _authenticator.AuthenticatedUser;
            WelcomeMessage = $"Welcome {MyUser.RoleEnum.ToString()} {MyUser.Name}";
        }
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Orders(object parameter)
        {
            _NavigationService.Navigate(_purchaseRequestsPage);
        }

        private void Goods(object parameter)
        {
            _NavigationService.Navigate(_goodsPage);
        }
        private void Roles(object parameter)
        {
            _NavigationService.Navigate(_manageAccountsPage);
        }

        private void Logout(object parameter)
        {
            _authenticator.AuthenticatedUser = new User();
            _NavigationService.GoBack();
        }
    }
}
