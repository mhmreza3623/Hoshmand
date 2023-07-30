using Hoshmand.Core.Interfaces.Repositories;
using Hoshmand.Infrastructure.DataBase;
using Hoshmand.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Hoshmand.Presentation.Configurations
{
    public static class EFServiceCollections
    {
        public static IServiceCollection AddEfConfiguraion(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped(typeof(IGeneralRepository<>), typeof(GeneralRepository<>));

            return services;
        }
    }
}
