using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using TMA.Warehouse.Api;
using TMA.Warehouse.Api.IApi;
using TMA.Warehouse.Authenticate;
using TMA.Warehouse.ViewModels;
using TMA.Warehouse.Views.Pages;
using TMA.Warehouse.Views.Windows;

namespace TMA.Warehouse
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IServiceProvider _serviceProvider;
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            IServiceCollection services = new ServiceCollection();
            ConfigureServices(services);

            _serviceProvider = services.BuildServiceProvider();

            MainWindow mainView = _serviceProvider.GetRequiredService<MainWindow>();
            mainView.Show();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<MainWindow>();

            services.AddSingleton<AuthenticationPage>();
            services.AddSingleton<RegistrationPage>();
            services.AddSingleton<HomePage>();
            services.AddSingleton<GoodsPage>();
            services.AddSingleton<PurchaseRequestsPage>();
            services.AddSingleton<ManageAccountsPage>();

            services.AddTransient<AddItemWindow>();
            services.AddTransient<UpdateItemWindow>();
            services.AddTransient<OrderItemWindow>();

            services.AddScoped<GoodsViewModel>();
            services.AddScoped<PurchaseRequestsViewModel>();
            services.AddScoped<HomePageViewModel>();
            services.AddScoped<ManageAccountsViewModel>();

            services.AddSingleton<IApiService, ApiService>();

            services.AddSingleton<IAuthenticator, Authenticator>();

            services.AddAutoMapper(typeof(Models.AutoMapper.AutoMapper));
        }
    }
}
