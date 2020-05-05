using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System.Linq;

namespace InvoiceManager.Api.Config
{
    public static class SwaggerConfig
    {
        private static string _env;

        public static void ConfigureSwaggerWebApi(this IServiceCollection services) => services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new Info { Title = "Invoice API", Version = "v1", Description = _env + " Environment" });

            options.ResolveConflictingActions(list => list.First());
        });

        public static void UseSwaggerWebApi(this IApplicationBuilder app, IHostingEnvironment env)
        {
            _env = env.EnvironmentName;
            app.UseSwagger();

            app.UseSwaggerUI(s =>
            {
                s.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger UI");
            });

        }
    }
}