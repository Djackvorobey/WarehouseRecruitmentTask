using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;
using AutoMapper;
using TMA.Warehouse.Api;
using TMA.Warehouse.Api.IApi;
using TMA.Warehouse.Authenticate;
using TMA.Warehouse.Models;
using TMA.Warehouse.ViewModels.RelayCommands;
using TMA.Warehouse.Views.Windows;

namespace TMA.Warehouse.ViewModels
{
    public class GoodsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<ItemViewModel> _items;
        public ObservableCollection<ItemViewModel> Items
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

        public NavigationService _NavigationService { get; set; }

        public ICommand OrderCommand { get; private set; }
        public ICommand AddCommand { get; private set; }
        public ICommand UpdateCommand { get; private set; }
        public ICommand RemoveCommand { get; private set; }
        public ICommand BackCommand { get; private set; }

        private AddItemWindow _addItemWindow;
        private UpdateItemWindow _updateItemWindow;
        private OrderItemWindow _orderItemWindow;

        private readonly IAuthenticator _authenticator;
        private readonly IApiService _apiService;
        private readonly IMapper _mapper;

        public GoodsViewModel(IAuthenticator authenticator,
            IMapper mapper,
            IApiService apiService,
            AddItemWindow addItemWindow,
            UpdateItemWindow updateItemWindow, 
            OrderItemWindow orderItemWindow)
        {
            _addItemWindow = addItemWindow;
            _authenticator = authenticator;
            _updateItemWindow = updateItemWindow;
            _orderItemWindow = orderItemWindow;
            _apiService = apiService;
            _mapper = mapper;


            OrderCommand = new RelayCommand(Order);
            AddCommand = new RelayCommand(Add);
            UpdateCommand = new RelayCommand(Update);
            RemoveCommand = new RelayCommand(Remove);
            BackCommand = new RelayCommand(Back);
        }

        public void InitNavigation(NavigationService navigationService)
        {
            _NavigationService = navigationService;
        }
        public void InitUser()
        {
            MyUser = _authenticator.AuthenticatedUser;
        }

        public void InitItemModels(List<ItemViewModel> itemModels)
        {
            Items = new ObservableCollection<ItemViewModel>();

            foreach (var model in itemModels)
            {
                Items.Add(model);
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Order(object parameter)
        {
            var item = parameter as ItemViewModel;
            if (item != null)
            {
                _orderItemWindow = new OrderItemWindow(_authenticator, _apiService);
                _orderItemWindow.InitItem(item);
                _orderItemWindow.ShowDialog();
            }
        }

        private void Add(object parameter)
        {
            _addItemWindow = new AddItemWindow(_apiService, _mapper, _authenticator);
            _addItemWindow.InitItems(Items);
            _addItemWindow.ShowDialog();
        }

        private void Update(object parameter)
        {
            var item = parameter as ItemViewModel;
            if (item != null)
            {
                _updateItemWindow = new UpdateItemWindow(_apiService, _mapper, _authenticator);
                _updateItemWindow.InitItems(Items, item);
                _updateItemWindow.ShowDialog();
            }
        }

        private async void Remove(object parameter)
        {
            var item = parameter as ItemViewModel;
            if (item != null)
            {
                int itemId = int.Parse(item.Id);

                HttpResponseMessage response = await _apiService.Delete(ApiConstants.ItemUrl, itemId);
                if (response != null)
                {
                    if (response.IsSuccessStatusCode)
                    {
                        Items.Remove(item);
                    }
                    var message = await response.Content.ReadAsStringAsync();

                    MessageBox.Show(message);
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
    }
}
