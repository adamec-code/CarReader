using System.Windows;
using CarReader.Application.Mappers;
using CarReader.Application.Repositories;
using CarReader.Application.Services;
using CarReader.Infrastructure.Repositories;
using CarReader.Presentation.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MainApplication = System.Windows.Application;

namespace CarReader.Presentation
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : MainApplication
    {
        public static IHost AppHost { get; private set; } = null!;

        public App()
        {
            AppHost = Host.CreateDefaultBuilder()
                .ConfigureServices(
                    (context, services) =>
                    {
                        // mappers
                        services.AddSingleton<CarMapper>();

                        // services
                        services.AddSingleton<ICarService, CarService>();

                        // repositories
                        services.AddSingleton<ICarRepository, XmlCarRepository>();

                        // viewmodely
                        services.AddSingleton<CarReaderViewModel>();

                        // windows
                        services.AddSingleton<MainWindow>();
                    }
                )
                .Build();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            try
            {
                await AppHost.StartAsync();

                var mainWindow = AppHost.Services.GetRequiredService<MainWindow>();

                mainWindow.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await AppHost.StopAsync();

            AppHost.Dispose();

            base.OnExit(e);
        }
    }
}
