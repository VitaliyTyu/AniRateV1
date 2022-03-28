using AniRateV1.Data;
using AniRateV1.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace AniRateV1
{
    public partial class App : Application
    {
        private static IHost __Host;
        public static IHost Host => __Host ??= Program.CreateHostBuilder(Environment.GetCommandLineArgs()).Build();

        public static IServiceProvider Services => Host.Services;

        internal static void ConfigureServices(HostBuilderContext host, IServiceCollection services) => services
            .AddViewModels()
            .AddDatabase(host.Configuration.GetSection("Database"))
            ;

        protected override async void OnStartup(StartupEventArgs e)
        {
            var host = Host;

            //если сделаем с await, бд инициализируется после открытия окна приложения
            using (var scope = Services.CreateScope())
                scope.ServiceProvider.GetRequiredService<DbInitializer>().InitializeAsync().Wait();
            //используя Wait() можно получить deadock, чтебы его избежать нужно у асинхронных методо вызывать .ConfigureAwait(false)

            base.OnStartup(e);
            await host.StartAsync();
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            using var host = Host;
            base.OnExit(e);
            await host.StopAsync();
        }
    }
}
