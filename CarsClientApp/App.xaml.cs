using CarsClientApp.Configuration;
using CarsClientApp.Services;
using CarsClientApp.Services.Abstract;
using CarsClientApp.ViewModel;
using CarsClientServices.Services;
using CarsClientServices.Services.Abstract;
using CarsClientSevices.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Windows;

namespace CarsClientApp
{
    public partial class App : Application
    {
        private static ServiceProvider _provider;

        protected override void OnStartup(StartupEventArgs e)
        {
            InitDependencyInjection();

            var mainWindow = new MainWindow
            {
                DataContext = new MainWindowViewModel(
                    _provider.GetService<IApiService>(), 
                    _provider.GetService<IHandlingExceptionService>())
            };

            mainWindow.Show();
        }

        public static void InitDependencyInjection()
        {
            var service = new ServiceCollection();

            var appConfiguration = new AppConfiguration();
            var httpClient = new HttpClient() { BaseAddress = new Uri($"{appConfiguration.BaseAdress}{appConfiguration.CarController}") };            
            var pathLog = appConfiguration.PathLog;

            service.AddSingleton<ILogger>(new LoggerFactory().AddFile(pathLog).CreateLogger<App>());
            service.AddSingleton<IApiService, ApiService>();
            service.AddSingleton<IHandlingResponseService, HandlingResponseService>();
            service.AddSingleton<IHandlingExceptionService, HandlingExceptionService>();
            service.AddSingleton(httpClient);
            service.AddSingleton<IAppConfiguration>(appConfiguration);

            _provider = service.BuildServiceProvider();
        }        
    }
}
