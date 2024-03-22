using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using AutoMapper;
using TMA.Warehouse.Api;
using TMA.Warehouse.Api.IApi;
using TMA.Warehouse.Authenticate;
using TMA.Warehouse.Models;
using TMA.Warehouse.ViewModels;

namespace TMA.Warehouse.Views.Pages
{
    /// <summary>
    /// Interaction logic for GoodsPage.xaml
    /// </summary>
    public partial class GoodsPage : Page
    {
        private readonly IAuthenticator _authenticator;
        private readonly IApiService _apiService;
        private readonly IMapper _mapper;
        private readonly GoodsViewModel _goodsViewModel;

        public GoodsPage(IAuthenticator authenticator, IApiService apiService, IMapper mapper, GoodsViewModel goodsViewModel)
        {
            _authenticator = authenticator;
            _apiService = apiService;
            _mapper = mapper;
            _goodsViewModel = goodsViewModel;

            InitializeComponent();

            Loaded += GoodsPage_Loaded;
        }

        private async void GoodsPage_Loaded(object sender, RoutedEventArgs e)
        {
           List<ItemModel> itemModels = await _apiService.GetAll<ItemModel>(ApiConstants.ItemAllUrl, _authenticator.AuthenticatedUser.Id);

           List<ItemViewModel> itemViewModels = new();

           foreach (var itemModel in itemModels)
           { 
               itemViewModels.Add( _mapper.Map<ItemModel, ItemViewModel>(itemModel));
           }

           _goodsViewModel.InitUser();
           
           _goodsViewModel.InitNavigation(NavigationService);

           _goodsViewModel.InitItemModels(itemViewModels);

            DataContext = _goodsViewModel;
        }
    }
}
