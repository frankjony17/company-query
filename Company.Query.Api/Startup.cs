using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Company.Query.Api.Configuration.AutoMapper;
using Company.Query.Api.Configuration.Extensions;
using Company.Query.Infra.CrossCutting.IoC;
using System.Diagnostics.CodeAnalysis;

namespace Company.Query.Api
{
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        public IConfiguration _configuration { get; }
        private const string Liveness = "Liveness";
        private const string Readiness = "Readiness";

        public Startup(IHostEnvironment env)
        {
            var environmentName = env.EnvironmentName;
            var builder = new ConfigurationBuilder()
                 .SetBasePath(env.ContentRootPath)
                 .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                 .AddJsonFile($"appsettings.{(string.IsNullOrEmpty(environmentName) ? "Development" : environmentName)}.json", optional: true, reloadOnChange: true)
                 .AddEnvironmentVariables();

            _configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureContainer(_configuration)
                    .AddControllerWithApiExceptionsFiltersAndJsonOptions()
                    .AddCompanySwaggerGen(_configuration)
                    .AddHealthChecks();

            services.AddAutoMapper(typeof(Startup));
            MappingConfiguration.Register();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCompanySwagger(_configuration);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/healthcheck", new HealthCheckOptions
                {
                    Predicate = check => check.Tags.Contains(Liveness)
                });

                endpoints.MapHealthChecks("/ready", new HealthCheckOptions
                {
                    Predicate = check => check.Tags.Contains(Readiness)
                });
            });
        }
    }
}
