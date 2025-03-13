using InvestmentUseCase.Infra.Data.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace InvestmentUseCase.Tests.IntegrationTests.Data
{
    public class InvestmentApiFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.Remove(
                    services.SingleOrDefault(d => d.ServiceType == typeof(IDbContextOptionsConfiguration<AppDbContext>))
                );

                services.AddDbContext<AppDbContext>(options =>
                {
                    options.UseInMemoryDatabase("InvestmentTestDb");
                });

                var sp = services.BuildServiceProvider();
                using var scope = sp.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                db.Database.EnsureCreated();
            });
        }
    }
}
