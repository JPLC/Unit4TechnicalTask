using InvoiceManager.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InvoiceManager.Api.Configuration
{
    public static class DbContextConfig
    {
        public static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<InvoiceDbContext>((options => options.UseSqlServer(configuration.GetConnectionString("Default"))));
        }
    }
}
