using AutoMapper;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaction logic for PurchaseRequestsPage.xaml
    /// </summary>
    public partial class PurchaseRequestsPage : Page
    {
        private readonly IAuthenticator _authenticator;
        private readonly IApiService _apiService;
        private readonly IMapper _mapper;
        private readonly PurchaseRequestsViewModel _purchaseViewModel;

        public PurchaseRequestsPage(IAuthenticator authenticator, IApiService apiService, IMapper mapper, PurchaseRequestsViewModel purchaseViewModel)
        {
            _authenticator= authenticator;
            _apiService = apiService;
            _mapper = mapper;
            _purchaseViewModel = purchaseViewModel;

            InitializeComponent();

            Loaded += PurchaseRequestsPage_Loaded;
        }

        private async void PurchaseRequestsPage_Loaded(object sender, RoutedEventArgs e)
        {
            List<TmaRequestModel> requestModels = await _apiService.GetAll<TmaRequestModel>(ApiConstants.TmaRequestUrl, _authenticator.AuthenticatedUser.Id);

            List<ItemModel> itemModels = await _apiService.GetAll<ItemModel>(ApiConstants.ItemAllUrl, _authenticator.AuthenticatedUser.Id);

            List<TmaRequestViewModel> requestViewModels = new();

                foreach (var model in requestModels)
                {
                    var requestViewModel = _mapper.Map<TmaRequestModel, TmaRequestViewModel>(model);

                   ItemModel itemModel = itemModels.FirstOrDefault(m => m.Id == requestViewModel.ItemId);

                   if (itemModel != null)
                   {
                       requestViewModel.ItemName = itemModel.Name;

                       requestViewModel.ItemGroup = itemModel.ItemGroup.ToString();

                       requestViewModel.ItemQuantity = itemModel.Quantity;

                       requestViewModel.StorageLocation = itemModel.StorageLocation;

                       requestViewModel.ContactPerson = itemModel.ContactPerson;

                       requestViewModel.ItemPhoto = itemModel.Photo;
                   }

                    requestViewModels.Add(requestViewModel);
                }

            _purchaseViewModel.InitNavigation(NavigationService);

            _purchaseViewModel.InitUser();

            _purchaseViewModel.InitItemModels(requestViewModels);

            DataContext = _purchaseViewModel;
        }
    }
}
