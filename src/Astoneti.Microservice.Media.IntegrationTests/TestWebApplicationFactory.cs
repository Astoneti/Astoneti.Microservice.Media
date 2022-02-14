using Astoneti.Microservice.Media.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;
using Xunit.Abstractions;

namespace Astoneti.Microservice.Media.IntegrationTests
{
    public class TestWebApplicationFactory : WebApplicationFactory<Startup>
    {
        private readonly DbContextOptions _dbContextOptions;

        public TestWebApplicationFactory()
        {
            _dbContextOptions = new DbContextOptionsBuilder()
                .UseInMemoryDatabase("InMemoryDbForTesting")
                .Options;

            DbContext = new MediaDbContext(_dbContextOptions);
        }

        public ITestOutputHelper Output { get; set; }

        public MediaDbContext DbContext { get; }

        protected override IHostBuilder CreateHostBuilder()
        {
            var builder = base.CreateHostBuilder();

            builder.ConfigureServices(
                services =>
                {
                    var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<MediaDbContext>));

                    if (descriptor != null)
                    {
                        services.Remove(descriptor);
                    }

                    services.AddDbContext<MediaDbContext>(
                        options => options.UseInMemoryDatabase("InMemoryDbForTesting")
                    );
                }
            );

            return builder;
        }
    }
}
