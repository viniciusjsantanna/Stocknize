﻿using Microsoft.Extensions.DependencyInjection;
using Stocknize.Domain.Interfaces.Domain;
using Stocknize.Domain.Services;

namespace Stocknize.Domain.IoC
{
    public static class DomainExtension
    {
        public static void DomainRegister(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(DomainExtension).Assembly);
            services.AddScoped<IProductService, ProductService>();
        }
    }
}
