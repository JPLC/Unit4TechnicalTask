using Microsoft.Extensions.DependencyInjection;

namespace InvoiceManager.Api.Configuration
{
    public static class MapperConfig
    {
        public static void ConfigureMapper(this IServiceCollection services)
        {
            //services.AddSingleton<IFundMapper, FundMapper>();
            //services.AddSingleton<IProviderMapper, ProviderMapper>();
        }
    }
}
