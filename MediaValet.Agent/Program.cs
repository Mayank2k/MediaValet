using System;
using System.IO;
using System.Threading.Tasks;
using MediaValet.OrderSupervisor.Repository;
using MediaValet.OrderSupervisor.Repository.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MediaValet.Agent
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            var services = new ServiceCollection();
            ConfigureServices(services);

            var serviceProvider = services.BuildServiceProvider();
            await serviceProvider.GetService<App>().Run();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).AddEnvironmentVariables()
                .Build();

            services.AddSingleton<IConfiguration>(configuration);            
            services.AddLogging(configure => configure.AddConsole())
                .AddTransient<App>();
            services.AddSingleton<IOrderRepository, OrderRepository>();
            services.AddSingleton(typeof(IQueueStorageRepository<>), typeof(QueueStorageRepository<>));
            services.AddSingleton(typeof(ITableStorageRepository<>), typeof(TableStorageRepository<>));
            services.AddAutoMapper(typeof(MapperProfile));
            
        }

    }
}
