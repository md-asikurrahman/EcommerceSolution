using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerceSolution.Service
{
    public static class ServiceDependencyInjection
    {
        public static IServiceCollection AddServiceDependency(this IServiceCollection services,IConfiguration configuration)
        {
            var assembly = typeof(ServiceDependencyInjection).Assembly;

            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(assembly);
            });
            //services.AddValidatorsFromAssembly(assembly);
            return services;
        }
    }
}
