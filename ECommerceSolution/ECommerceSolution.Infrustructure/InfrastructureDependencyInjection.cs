using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ECommerceSolution.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;
using ECommerceSolution.Infrastructure.FileService;

namespace ECommerceSolution.Infrastructure
{
    public static class InfrastructureDependencyInjection
    {
        public static IServiceCollection AddInfrastructureDependency(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

            });
            //services.Configure<FileStorageSettings>(configuration.GetSection(nameof(FileStorageSettings)));
            
            return services;
        }
    }
}
