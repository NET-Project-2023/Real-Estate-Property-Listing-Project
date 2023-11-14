using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Real_estate.Application.Contracts;
using Real_estate.Application.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class InfrastructureRegistrationDI
    {
        public static IServiceCollection AddInfrastructureToDI(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<RealEstateContext>(
                options =>
                options.UseNpgsql(
                    configuration.GetConnectionString
                    ("RealEstateConnection"),
                    builder =>
                    builder.MigrationsAssembly(
                        typeof(RealEstateContext)
                        .Assembly.FullName)));
            services.AddScoped
                (typeof(IAsyncRepository<>),
                typeof(BaseRepository<>));
            services.AddScoped<
                IUserRepository, UserRepository>();
            return services;
        }
    }
}
