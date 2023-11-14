using System.Runtime.CompilerServices;
using MediatR;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Real_estate.Application
{
    public static class ApplicationServiceRegistrationDI
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR
                (
                    cfg => cfg.RegisterServicesFromAssembly(

                        Assembly.GetExecutingAssembly())
                );
        }
    }
}
