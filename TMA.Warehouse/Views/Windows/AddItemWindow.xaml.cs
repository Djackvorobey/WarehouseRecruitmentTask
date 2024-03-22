using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text.Json;
using System.Windows;
using AutoMapper;
using TMA.Warehouse.Api;
using TMA.Warehouse.Api.IApi;
using TMA.Warehouse.Authenticate;
using TMA.Warehouse.Models;
using TMA.Warehouse.Models.Dtos;
using TMA.Warehouse.ViewModels;
using static TMA.Warehouse.Models.Enums.Enums;

namespace TMA.Warehouse.Views.Windows
{
    /// <summary>
    /// Interaction logic for AddItemWindow.xaml
    /// </summary>
    public partial class AddItemWindow : Window
    {
        private readonly IApiService _apiService;
        private readonly IMapper _mapper;
        private readonly IAuthenticator _authenticator;

        private ObservableCollection<ItemViewModel> Items;
        public ItemGroup ItemGroupEnum { get; set; }
        public ItemGroup[] ItemGroupEnumValues => (ItemGroup[])Enum.GetValues(typeof(ItemGroup));

        public UnitOfMeasurement MeasurementEnum { get; set; }
        public UnitOfMeasurement[] MeasurementEnumValues => (UnitOfMeasurement[])Enum.GetValues(typeof(UnitOfMeasurement));

        public AddItemWindow(IApiService apiService, IMapper mapper, IAuthenticator authenticator)
        {
            _apiService = apiService;
            _mapper = mapper;
            _authenticator = authenticator;

            InitializeComponent();

            ItemGroupEnum = ItemGroup.None;
            MeasurementEnum = UnitOfMeasurement.None;
            DataContext = this;
        }

        public void InitItems(ObservableCollection<ItemViewModel> items)
        {
            Items=items;
        }

        private async void AddItemButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(ItemNameBox.Text)
                && !String.IsNullOrEmpty(StatusBox.Text)
                && decimal.TryParse(PriceWithoutVatBox.Text, out decimal priceWithoutVat)
                && int.TryParse(QuantityBox.Text, out int quantity))
            {
                ItemViewModel itemViewModel = new ItemViewModel()
                {
                    Name = ItemNameBox.Text,
                    ItemGroup = GroupBox.Text,
                    Status = StatusBox.Text,
                    ContactPerson = ContactPersonBox.Text,
                    StorageLocation = StorageLocation.Text,
                    UnitOfMeasurement = MeasurementBox.Text,
                    PriceWithoutVAT = priceWithoutVat,
                    Quantity = quantity,
                    Photo = ""
                };

               ItemModel itemModel = _mapper.Map<ItemViewModel, ItemModel>(itemViewModel);

               AccountModel accountModel = _mapper.Map<User, AccountModel>(_authenticator.AuthenticatedUser);

               ItemAccountDto itemAccountDto = new ItemAccountDto()
                   { AccountModel = accountModel, ItemModel = itemModel };

              HttpResponseMessage response = await _apiService.PostAsString(ApiConstants.ItemUrl, itemAccountDto);

              ResponseIdModel responseModel = new ResponseIdModel();

                  if (response.IsSuccessStatusCode)
                  {
                      var responseContent = await response.Content.ReadAsStringAsync();

                       responseModel = JsonSerializer.Deserialize<ResponseIdModel>(responseContent);

                      itemViewModel.Id = responseModel.ItemId.ToString();

                      Items.Add(itemViewModel);
                  }

                var message = responseModel.Message;

                MessageBox.Show(message);

                Close();
            }
            else
            {
                MessageBox.Show("Data entered incorrectly");
            }
        }

        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
