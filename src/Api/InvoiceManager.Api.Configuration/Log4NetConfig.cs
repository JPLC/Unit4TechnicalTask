using log4net;
using log4net.Config;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Reflection;

namespace InvoiceManager.Api.Configuration
{
    public static class Log4NetConfig
    {
        public static void ConfigureLog4Net(this IServiceCollection services)
        {
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());

            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
        }
    }
}
