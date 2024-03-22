using AutoMapper;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Text.Json;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;
using TMA.Warehouse.Api;
using TMA.Warehouse.Api.IApi;
using TMA.Warehouse.Authenticate;
using TMA.Warehouse.Models;
using TMA.Warehouse.Models.Dtos;
using TMA.Warehouse.Models.Enums;
using TMA.Warehouse.ViewModels.RelayCommands;
using TMA.Warehouse.Views.Pages;

namespace TMA.Warehouse.ViewModels
{
    public class PurchaseRequestsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<TmaRequestViewModel> _items;

        public ObservableCollection<TmaRequestViewModel> Items
        {
            get { return _items; }
            set
            {
                _items = value;
                OnPropertyChanged(nameof(Items));
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
        public ICommand ConfirmCommand { get; private set; }
        public ICommand RejectCommand { get; private set; }
        public ICommand OrderCommand { get; private set; }
        public ICommand BackCommand { get; private set; }

        private readonly IAuthenticator _authenticator;
        private readonly IMapper _mapper;
        private readonly IApiService _apiService;
        private readonly GoodsPage _goodsPage;

        private NavigationService _NavigationService { get; set; }


        public PurchaseRequestsViewModel(IAuthenticator authenticator,
            IMapper mapper,
            IApiService apiService,
            GoodsPage goodsPage)
        {
            _authenticator = authenticator;
            _mapper = mapper;
            _apiService = apiService;
            _goodsPage = goodsPage;

            ConfirmCommand = new RelayCommand(Confirm);
            RejectCommand = new RelayCommand(Reject);
            OrderCommand = new RelayCommand(CreateOrder);
            BackCommand = new RelayCommand(Back);
        }

        public void InitUser()
        {
            MyUser = _authenticator.AuthenticatedUser;
        }

        public void InitNavigation(NavigationService NavigationService)
        {
            _NavigationService = NavigationService;
        }
        public void InitItemModels(List<TmaRequestViewModel> requstViewModels)
        {
            Items = new ObservableCollection<TmaRequestViewModel>();

            foreach (var model in requstViewModels)
            {
                Items.Add(model);
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private async void Confirm(object parameter)
        {
            var item = parameter as TmaRequestViewModel;
            if (item != null)
            {
                TmaRequestModel requestModel = _mapper.Map<TmaRequestViewModel, TmaRequestModel>(item);

                AccountModel accountModel = _mapper.Map<User, AccountModel>(_authenticator.AuthenticatedUser);

                TmaRequestAccountDto tmeRequestAccountDto = new TmaRequestAccountDto()
                    { AccountModel = accountModel, TmaRequestModel = requestModel };

                HttpResponseMessage response =
                    await _apiService.Put(ApiConstants.TmaRequestConfirmUrl, tmeRequestAccountDto);

                if (response != null)
                {
                    var message = await response.Content.ReadAsStringAsync();

                    var responseModel = JsonSerializer.Deserialize<ResponseTmaRequestModel>(message);

                    if (response.IsSuccessStatusCode)
                    {
                        TmaRequestViewModel requestViewModel =
                            _mapper.Map<TmaRequestModel, TmaRequestViewModel>(responseModel.TmaRequestModel);

                        HttpResponseMessage responseItem =
                            await _apiService.GetById(ApiConstants.ItemUrl, requestViewModel.ItemId);

                        if (responseItem.IsSuccessStatusCode)
                        {
                            var itemMessage = await responseItem.Content.ReadAsStringAsync();

                            ItemModel itemModel = JsonSerializer.Deserialize<ItemModel>(itemMessage);

                            requestViewModel.ItemName = itemModel.Name;

                            requestViewModel.ItemGroup = itemModel.ItemGroup.ToString();

                            requestViewModel.ItemQuantity = itemModel.Quantity;

                            requestViewModel.StorageLocation = itemModel.StorageLocation;

                            requestViewModel.ContactPerson = itemModel.ContactPerson;

                            requestViewModel.ItemPhoto = itemModel.Photo;
                        }

                        int index = Items.IndexOf(item);

                        Items.Remove(item);
                        Items.Insert(index, requestViewModel);
                    }

                    MessageBox.Show(responseModel.Message);
                }
            }
        }

        private async void Reject(object parameter)
        {
            var item = parameter as TmaRequestViewModel;
            if (item != null)
            {
                TmaRequestModel requestModel = _mapper.Map<TmaRequestViewModel, TmaRequestModel>(item);

                AccountModel accountModel = _mapper.Map<User, AccountModel>(_authenticator.AuthenticatedUser);

                TmaRequestAccountDto tmeRequestAccountDto = new TmaRequestAccountDto()
                    { AccountModel = accountModel, TmaRequestModel = requestModel };

                HttpResponseMessage response =
                    await _apiService.Put(ApiConstants.TmaRequestRejectUrl, tmeRequestAccountDto);

                if (response != null)
                {
                    var message = await response.Content.ReadAsStringAsync();

                    var responseModel = JsonSerializer.Deserialize<ResponseTmaRequestModel>(message);

                    if (response.IsSuccessStatusCode)
                    {
                        TmaRequestViewModel requestViewModel =
                            _mapper.Map<TmaRequestModel, TmaRequestViewModel>(responseModel.TmaRequestModel);

                        HttpResponseMessage responseItem =
                            await _apiService.GetById(ApiConstants.ItemUrl, requestViewModel.ItemId);

                        if (responseItem.IsSuccessStatusCode)
                        {
                            var itemMessage = await responseItem.Content.ReadAsStringAsync();

                            ItemModel itemModel = JsonSerializer.Deserialize<ItemModel>(itemMessage);

                            requestViewModel.ItemName = itemModel.Name;

                            requestViewModel.ItemGroup = itemModel.ItemGroup.ToString();

                            requestViewModel.ItemQuantity = itemModel.Quantity;

                            requestViewModel.StorageLocation = itemModel.StorageLocation;

                            requestViewModel.ContactPerson = itemModel.ContactPerson;

                            requestViewModel.ItemPhoto = itemModel.Photo;
                        }

                        int index = Items.IndexOf(item);

                        Items.Remove(item);
                        Items.Insert(index, requestViewModel);
                    }

                    MessageBox.Show(responseModel.Message);
                }
            }
        }

        private void Back(object parameter)
        {
            if (_NavigationService.CanGoBack)
            {
                _NavigationService.GoBack();
            }
        }
        private void CreateOrder(object parameter)
        {
            _NavigationService.Navigate(_goodsPage);
        }
    }
}
