using Astoneti.Service.NewsWorker.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Threading.Tasks;

namespace Astoneti.Service.NewsWorker
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var services = ConfigureServices();

            var serviceProvider = services.BuildServiceProvider();

            await serviceProvider.GetService<AppSetup>().RunAsync();
        }

        private static IServiceCollection ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();

            var config = Configuration();

            services
                .AddSingleton(config);
            services
                .AddScoped<INewsServiceConsole, NewsServiceConsole>();
            services
                .AddScoped<INewsProviderConsole, NewsProviderConsole>();

            services
                .AddScoped<AppSetup>();

            return services;
        }

        public static IConfiguration Configuration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory());

            return builder
                .Build();
        }
    }
}
