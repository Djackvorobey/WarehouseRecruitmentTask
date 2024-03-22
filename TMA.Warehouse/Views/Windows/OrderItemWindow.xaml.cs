using System;
using System.Net.Http;
using System.Windows;
using TMA.Warehouse.Api;
using TMA.Warehouse.Api.IApi;
using TMA.Warehouse.Authenticate;
using TMA.Warehouse.Models;
using TMA.Warehouse.Models.Enums;
using TMA.Warehouse.ViewModels;

namespace TMA.Warehouse.Views.Windows
{
    /// <summary>
    /// Interaction logic for OrderItemWindow.xaml
    /// </summary>
    public partial class OrderItemWindow : Window
    {
        private ItemViewModel Item;

        private readonly IAuthenticator _authenticator;
        private readonly IApiService _apiService;
        public OrderItemWindow(IAuthenticator authenticator, IApiService apiService)
        {
            _authenticator = authenticator;
            _apiService = apiService;

            InitializeComponent();
        }
        public new void ShowDialog()
        {
            InitItemInfos();
            base.ShowDialog();
        }
        public void InitItem(ItemViewModel itemViewModel)
        {
            Item = itemViewModel;
        }

        private async void SendOrderButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(QuantityBox.Text, out int quantity)
                && Enum.TryParse<Enums.UnitOfMeasurement>(Item.UnitOfMeasurement, out Enums.UnitOfMeasurement measurement))
            {
                TmaRequestModel requestModel = new TmaRequestModel()
                {
                    ItemId = int.Parse(Item.Id),
                    PriceWithoutVAT = Item.PriceWithoutVAT,
                    Status = Enums.Status.New,
                    EmployeeName = _authenticator.AuthenticatedUser.Name,
                    Quantity = quantity,
                    UnitOfMeasurement = measurement,
                    Comment = CommentBox.Text
                };

                HttpResponseMessage responce = await _apiService.PostAsString(ApiConstants.TmaRequestUrl, requestModel);

                if (responce != null)
                {
                    
                    var message = await responce.Content.ReadAsStringAsync();

                    MessageBox.Show(message);

                    if (responce.IsSuccessStatusCode)
                    {
                        Close();
                    }
                }
            }
        }
        private void InitItemInfos()
        {
            ItemNameBox.Text = Item.Name;
            MeasurementBox.Text = Item.UnitOfMeasurement;
            PriceWithoutVatBox.Text = Item.PriceWithoutVAT.ToString();
        }

        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
