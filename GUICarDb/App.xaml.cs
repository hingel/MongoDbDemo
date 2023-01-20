using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using DataAccess;
using DataAccess.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GUICarDb
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IHost? AppHost { get; private set; } //Behöver ej ha frågetecken, men för att slippa squiglyline.

        public App()
        {
            AppHost = Host.CreateDefaultBuilder().ConfigureServices((hostContext, services) =>
            {
                services.AddScoped<IRepository<Car>, CarManager>();
                services.AddScoped<IRepository<MyColor>, ColorManager>();
                services.AddScoped<IRepository<Brand>, BrandManager>();
                
                //Lägg till fler IRepositories till services om det behövs.
                //services.AddScoped<IRepository<Brand>, MakeManager>();

                services.AddScoped<MainWindow>();
            }).Build();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await AppHost!.StartAsync(); //Utropstecknget säger att den inte kan vara null
            var mainWindow = AppHost!.Services.GetRequiredService<MainWindow>(); //GetRequiredService letar upp rätt service i ServiceCollection.
            mainWindow.Show();
            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await AppHost!.StopAsync(); //Avslutar Hosten, annars kör den bara vidare även om programmet stängs ned.
            base.OnExit(e);
        }
    }
}
