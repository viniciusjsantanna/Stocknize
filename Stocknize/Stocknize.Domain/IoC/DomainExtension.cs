using Microsoft.Extensions.DependencyInjection;
using NetCore.AutoRegisterDi;
using Stocknize.Domain.Exceptions.NotFound;
using Stocknize.Domain.Interfaces.Domain;
using Stocknize.Domain.Interfaces.Filters;
using Stocknize.Domain.Services;

namespace Stocknize.Domain.IoC
{
    public static class DomainExtension
    {
        public static void DomainRegister(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(DomainExtension).Assembly);
            services.RegisterAssemblyPublicNonGenericClasses()
                .Where(e => e.Name.EndsWith("Service"))
                .AsPublicImplementedInterfaces(ServiceLifetime.Scoped);
            services.AddScoped<IJwtGenerator, JwtGenerator>();

            //filters
            services.AddScoped<IExceptionHandler, NotFoundExceptionHandler>();
        }
    }
}
