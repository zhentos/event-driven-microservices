using Application.Common.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class ConfigureServices
    {
        public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<OrderDbContext>(options =>
                        options.UseNpgsql(configuration.GetConnectionString("OrdersConnection")));

            services.AddDbContext<UserDbContext>(options =>
                        options.UseNpgsql(configuration.GetConnectionString("UsersConnection")));

            //Commented code is a mechanism to deal with entity framework migrations.

            //var migrationsAssembly = Assembly.GetExecutingAssembly().GetName().Name;

            //services.AddDbContext<UserDbContext>(options =>
            //    options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
            //             x => x.MigrationsAssembly(migrationsAssembly)));

            services.AddScoped<IOrderDbContext>(provider => provider.GetRequiredService<OrderDbContext>());
            services.AddScoped<IUserDbContext>(provider => provider.GetRequiredService<UserDbContext>());
        }
    }
}
