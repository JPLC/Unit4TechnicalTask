using InvoiceManager.Domain;
using InvoiceManager.Services;
using InvoiceManager.Services.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Toolkit.UoW.Abstractions;
using Toolkit.UoW.Repository;

namespace InvoiceManager.Api.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void ConfigureDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            //// applications
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            ////services
            services.AddScoped<IInvoiceService, InvoiceService>();
            services.AddTransient(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));

            // building dependencies
            services.BuildServiceProvider();
        }
    }
}
