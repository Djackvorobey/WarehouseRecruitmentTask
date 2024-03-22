using System;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;
using TMA.Warehouse.Api;
using TMA.Warehouse.Api.IApi;
using TMA.Warehouse.Models;
using static TMA.Warehouse.Models.Enums.Enums;

namespace TMA.Warehouse.Views.Pages
{
    /// <summary>
    /// Interaction logic for RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        public RoleEnums[] MyEnumValues => (RoleEnums[])Enum.GetValues(typeof(RoleEnums));
        public RoleEnums SelectedEnum { get; set; }

        public readonly IApiService _apiService;
        public RegistrationPage(IApiService apiService)
        {
            _apiService = apiService;

            InitializeComponent();

            SelectedEnum = RoleEnums.Employee;
            DataContext = this;
        }

        private async void RegisterButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(NameBox.Text)) { MessageBox.Show("Please write your name"); return; }

            RoleEnums selectedValue = (RoleEnums)RolesComboBox.SelectedItem;

            AccountModel accountModel = new AccountModel() { Name = NameBox.Text, RoleEnum = selectedValue };

            HttpResponseMessage response = await _apiService.PostAsString(ApiConstants.RegisterUrl, accountModel);

            var message = await response.Content.ReadAsStringAsync();

            MessageBox.Show(message);

            NavigationService.GoBack();
        }
    }
}
