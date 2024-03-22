using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TMA.Warehouse.Api;
using TMA.Warehouse.Api.IApi;
using TMA.Warehouse.Authenticate;
using TMA.Warehouse.Models;

namespace TMA.Warehouse.Views.Pages
{
    /// <summary>
    /// Interaction logic for AuthenticationPage.xaml
    /// </summary>
    public partial class AuthenticationPage : Page
    {
        
        private readonly HomePage _homePage;
        private readonly RegistrationPage _registrationPage;
        private IAuthenticator _authenticator;
        private readonly IApiService _apiService;
        public AuthenticationPage(RegistrationPage registrationPage,
            HomePage homePage,
            IAuthenticator authenticator,
            IApiService apiService)
        {
            _homePage = homePage;
            _authenticator = authenticator;
            _apiService = apiService;
            _registrationPage = registrationPage;


            InitializeComponent();
        }

        private void CreateAccountLabel_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(_registrationPage);
        }
        private async void LoginButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(NameBox.Text)) { MessageBox.Show("Please write your name"); return; }

            AccountModel accountModel = new AccountModel() { Name = NameBox.Text };

            AccountModel accountModelResponce = await _apiService.Post(ApiConstants.AuthenticateUrl, accountModel);

            if (accountModelResponce == null)
            {
                return;
            }

            _authenticator.AuthenticatedUser = new User()
                { Id = accountModelResponce.Id.ToString(), Name = accountModelResponce.Name, RoleEnum = accountModelResponce.RoleEnum };

            NavigationService.Navigate(_homePage);
        }
    }
}
