using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Project.Service.Data;
using Project.Service.Mapping;
using Project.Service.Model;
using Project.Service.Repository;

namespace Project.Service
{
    public static class DependancyInjection
    {
        public static IServiceCollection Persistance(this IServiceCollection services, IConfiguration config)
        {
            services
                .AddDbContext<ApplicationDBContext>
                (options => options.UseSqlServer(config.GetConnectionString("ApplicationContext"), 
                b => b.MigrationsAssembly(typeof(ApplicationDBContext).Assembly.FullName)), 
                ServiceLifetime.Scoped);

            services.AddScoped<IDBContext>(provider => provider.GetService<ApplicationDBContext>());

            services.AddScoped<IMapping, MapperConfig>();

            services.AddScoped
                <IVehicleService
                <VehicleMake,
                VehicleMakeDTORead,
                VehicleMakeDTOInsert,
                VehicleMakeDTOReadWithoutID>, VehicleMakeService>();

            return services;
        }
    }
}
