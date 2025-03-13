using InvestmentUseCase.Domain.Interfaces.Auth;
using InvestmentUseCase.Domain.Interfaces.Notification;
using InvestmentUseCase.Domain.Interfaces.Repositories;
using InvestmentUseCase.Domain.Interfaces.Services;
using InvestmentUseCase.Domain.Services;
using InvestmentUseCase.Infra.Auth.Services;
using InvestmentUseCase.Infra.Data.Context;
using InvestmentUseCase.Infra.Data.Repositories;
using InvestmentUseCase.Infra.Notification.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InvestmentUseCase.Infra.IoC.DependencyInjection
{
    public static class DependecyInjection
    {
        public static IServiceCollection Inject(this IServiceCollection services, IConfiguration configuration)
        {

            string connectionString = GetConnectionString(configuration);

            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(connectionString,
                b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));

            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IInvestmentRepository, InvestmentRepository>();
            services.AddScoped<IInvestmentProductRepository, InvestmentProductRepository>();

            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IInvestmentService, InvestmentService>();
            services.AddScoped<IInvestmentProductService, InvestmentProductService>();
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IJwtService, JwtService>();

            services.AddScoped<INotificationService, NotificationService>();


            return services;
        }

        private static string GetConnectionString(IConfiguration configuration)
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var connectionString = env == "Development" ? configuration.GetConnectionString("LocalConnection") : configuration.GetConnectionString("DefaultConnection");

            return connectionString;
        }
    }
}
