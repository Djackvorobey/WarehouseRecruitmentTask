using AutoMapper;
using System;
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
using TMA.Warehouse.ViewModels.RelayCommands;
using static TMA.Warehouse.Models.Enums.Enums;

namespace TMA.Warehouse.ViewModels
{
    public class ManageAccountsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly IAuthenticator _authenticator;
        private readonly IApiService _apiService;
        private readonly IMapper _mapper;

        public NavigationService _NavigationService { get; set; }

        private ObservableCollection<AccountViewModel> _accounts;
        public ObservableCollection<AccountViewModel> Accounts
        {
            get { return _accounts; }
            set
            {
                _accounts = value;
                OnPropertyChanged(nameof(Accounts));
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

        public RoleEnums[] MyEnumValues => (RoleEnums[])Enum.GetValues(typeof(RoleEnums));
        public RoleEnums SelectedEnum { get; set; }

        public ICommand UpdateCommand { get; private set; }
        public ICommand RemoveCommand { get; private set; }
        public ICommand BackCommand { get; private set; }

        public ManageAccountsViewModel(IAuthenticator authenticator, IApiService apiService, IMapper mapper)
        {
            _authenticator = authenticator;
            _apiService = apiService;
            _mapper = mapper;

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

        public void InitItemModels(List<AccountViewModel> accountViewModels)
        {
            Accounts = new ObservableCollection<AccountViewModel>();

            foreach (var model in accountViewModels)
            {
                Accounts.Add(model);
            }
        }

        private async void Update(object parameter)
        {
            var item = parameter as AccountViewModel;
            if (item != null)
            {
                AccountModel userAccountModel = _mapper.Map<AccountModel>(item);
                AccountModel AdminAccountModel = _mapper.Map<AccountModel>(_authenticator.AuthenticatedUser);

                ManageAccountRequestDto requestDto = new()
                    { UserAccount = userAccountModel, AdminAccount = AdminAccountModel };

                HttpResponseMessage response = await _apiService.Put(ApiConstants.AccountUpdateUrl, requestDto);

                if (response.IsSuccessStatusCode)
                {
                    var message = await response.Content.ReadAsStringAsync();

                    ManageAccountResponceDto responceDto = JsonSerializer.Deserialize<ManageAccountResponceDto>(message);

                    AccountViewModel userAccount = _mapper.Map<AccountViewModel>(responceDto.Account);

                    if (userAccount != null)
                    {
                        int index = Accounts.IndexOf(item);

                        Accounts.Remove(item);
                        Accounts.Insert(index, userAccount);
                    }

                    MessageBox.Show(responceDto.Message);
                }
            }
        }

        private async void Remove(object parameter)
        {
            var item = parameter as AccountViewModel;
            if (item != null)
            {
                AccountModel userAccountModel = _mapper.Map<AccountModel>(item);
                AccountModel AdminAccountModel = _mapper.Map<AccountModel>(_authenticator.AuthenticatedUser);

                ManageAccountRequestDto requestDto = new()
                    { UserAccount = userAccountModel, AdminAccount = AdminAccountModel };

                HttpResponseMessage response = await _apiService.Put(ApiConstants.AccountRemoveUrl, requestDto);

                if (response.IsSuccessStatusCode)
                {
                    var message = await response.Content.ReadAsStringAsync();

                    ManageAccountResponceDto responceDto = JsonSerializer.Deserialize<ManageAccountResponceDto>(message);

                    Accounts.Remove(item);

                    MessageBox.Show(responceDto.Message);
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

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
