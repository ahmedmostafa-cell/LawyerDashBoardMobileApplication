using BL;
using BL.Interfaces;
using BL.Repositories;
using Google;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AlMohamyProject.Extensions
{
    public static class ContextServicesExtensions
    {
        public static IServiceCollection AddContextServices(this IServiceCollection services, IConfiguration config)
        {

            //- context && json services
           
            //services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
            //services.AddControllers().AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            // IBaseRepository && IUnitOfWork Service
            //services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>)); // only Repository
            //services.AddTransient<IUnitOfWork, UnitOfWork>(); // Repository and UnitOfWork

            return services;
        }
    }
}
