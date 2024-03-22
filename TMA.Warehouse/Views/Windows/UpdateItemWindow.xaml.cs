using AutoMapper;
using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Windows;
using TMA.Warehouse.Api;
using TMA.Warehouse.Api.IApi;
using TMA.Warehouse.Authenticate;
using TMA.Warehouse.Models;
using TMA.Warehouse.Models.Dtos;
using TMA.Warehouse.Models.Enums;
using TMA.Warehouse.ViewModels;

namespace TMA.Warehouse.Views.Windows
{
    /// <summary>
    /// Interaction logic for UpdateItemWindow.xaml
    /// </summary>
    public partial class UpdateItemWindow : Window
    {
        private readonly IApiService _apiService;
        private readonly IMapper _mapper;
        private readonly IAuthenticator _authenticator;

        private ObservableCollection<ItemViewModel> Items;

        private ItemViewModel Item;
        public Enums.ItemGroup ItemGroupEnum { get; set; }
        public Enums.ItemGroup[] ItemGroupEnumValues => (Enums.ItemGroup[])Enum.GetValues(typeof(Enums.ItemGroup));

        public Enums.UnitOfMeasurement MeasurementEnum { get; set; }
        public Enums.UnitOfMeasurement[] MeasurementEnumValues => (Enums.UnitOfMeasurement[])Enum.GetValues(typeof(Enums.UnitOfMeasurement));

        public UpdateItemWindow(IApiService apiService, IMapper mapper, IAuthenticator authenticator)
        {
            _apiService = apiService;
            _mapper = mapper;
            _authenticator = authenticator;

            InitializeComponent();

            ItemGroupEnum = Enums.ItemGroup.None;
            MeasurementEnum = Enums.UnitOfMeasurement.None;
            DataContext = this;
        }

        public new void ShowDialog()
        {
            InitItemInfos();
            base.ShowDialog(); 
        }

        public void InitItems(ObservableCollection<ItemViewModel> items, ItemViewModel item)
        {
            Items = items;
            Item = item;
        }

        private async void UpdateItemButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (decimal.TryParse(PriceWithoutVatBox.Text, out decimal priceWithoutVat)
                && int.TryParse(QuantityBox.Text, out int quantity))
            {
                ItemViewModel itemViewModel = new ItemViewModel()
                {
                    Id = Item.Id,
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

                HttpResponseMessage response = await _apiService.Put(ApiConstants.ItemUrl, itemAccountDto);

                if (response.IsSuccessStatusCode)
                {
                    int index = Items.IndexOf(Item);
                    Items.Remove(Item);
                    Items.Insert(index, itemViewModel);
                }

                var message = await response.Content.ReadAsStringAsync();

                MessageBox.Show(message);

               Close();
            }
            else
            {
                MessageBox.Show("Data entered incorrectly");
            }
        }

        private void InitItemInfos()
        {
            ItemNameBox.Text = Item.Name;
            GroupBox.Text = Item.ItemGroup;
            if (Enum.TryParse<Enums.ItemGroup>(Item.ItemGroup, out Enums.ItemGroup group))
            {
                ItemGroupEnum = group;
            }
            if (Enum.TryParse<Enums.UnitOfMeasurement>(Item.UnitOfMeasurement, out Enums.UnitOfMeasurement measurement))
            {
                MeasurementEnum = measurement;
            }
            QuantityBox.Text = Item.Quantity.ToString();
            PriceWithoutVatBox.Text = Item.PriceWithoutVAT.ToString();
            StatusBox.Text = Item.Status;
            StorageLocation.Text = Item.StorageLocation;
            ContactPersonBox.Text = Item.ContactPerson;
        }

        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
