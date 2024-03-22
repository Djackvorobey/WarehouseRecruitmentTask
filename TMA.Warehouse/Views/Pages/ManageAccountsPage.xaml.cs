using AutoMapper;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using TMA.Warehouse.Api;
using TMA.Warehouse.Api.IApi;
using TMA.Warehouse.Authenticate;
using TMA.Warehouse.Models;
using TMA.Warehouse.ViewModels;

namespace TMA.Warehouse.Views.Pages
{
    /// <summary>
    /// Interaction logic for ManageAccountsPage.xaml
    /// </summary>
    public partial class ManageAccountsPage : Page
    {
        private readonly IAuthenticator _authenticator;
        private readonly IApiService _apiService;
        private readonly IMapper _mapper;
        private readonly ManageAccountsViewModel _manageAccountsViewModel;

        public ManageAccountsPage(IAuthenticator authenticator, IApiService apiService, IMapper mapper, ManageAccountsViewModel manageAccountsViewModel)
        {
            _authenticator = authenticator;
            _apiService = apiService;
            _mapper = mapper;
            _manageAccountsViewModel = manageAccountsViewModel;

            InitializeComponent();

            Loaded += OnLoaded;
        }

        private async void OnLoaded(object sender, RoutedEventArgs e)
        {
            List<AccountModel> accountModels = await _apiService.GetAll<AccountModel>(ApiConstants.AccountAllUrl, _authenticator.AuthenticatedUser.Id);

            List<AccountViewModel> accountViewModels = new();

            foreach (var accountModel in accountModels)
            {
                accountViewModels.Add(_mapper.Map<AccountModel, AccountViewModel>(accountModel));
            }

            _manageAccountsViewModel.InitUser();

            _manageAccountsViewModel.InitNavigation(NavigationService);

            _manageAccountsViewModel.InitItemModels(accountViewModels);

            DataContext = _manageAccountsViewModel;
        }
    }
}
