using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetCore.AutoRegisterDi;
using Stocknize.Domain.Interfaces.Repositories;
using Stocknize.Infrastructure.Context;
using Stocknize.Infrastructure.Repositories;

namespace Stocknize.Infrastructure.IoC
{
    public static class InfraExtension
    {
        private const string SqlServerConnectionString = "SqlServerConnection";

        public static void InfraRegister(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<EFContext>(config =>
            {
                config.UseSqlServer(configuration.GetConnectionString(SqlServerConnectionString));
            });
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.RegisterAssemblyPublicNonGenericClasses()
                    .Where(e => e.Name.EndsWith("Repository"))
                    .AsPublicImplementedInterfaces();
        }
    }
}
