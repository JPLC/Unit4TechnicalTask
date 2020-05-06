using AutoMapper;
using InvoiceManager.Api.Config;
using InvoiceManager.Api.Configuration;
using InvoiceManager.Services.Mappings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Toolkit.Api.Middlewares;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace InvoiceManager.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureDependencyInjection(Configuration);

            services.ConfigureMapper();

            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(ValidateModelStateAttribute));
                options.Filters.Add(typeof(APIActionFilter));
            }).AddDataAnnotationsLocalization().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddHealthChecks();

            services.ConfigureSwaggerWebApi();

            services.ConfigureDbContext(Configuration);

            services.ConfigureLog4Net();

            services.AddAutoMapper(c => c.AddProfile<AutoMapping>(), typeof(Startup));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseHealthChecks("/health");

            app.UseSwaggerWebApi(env);

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseMvc();
        }
    }
}
