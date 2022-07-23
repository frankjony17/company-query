using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;

namespace Company.Query.Api.Configuration.Extensions
{
    public static class SwaggerExtensions
    {
        private const string API_NAME = "COMPANY Payment Query API";

        public static IServiceCollection AddCompanySwaggerGen(this IServiceCollection services, IConfiguration configuration)
        {
            var useSwagger = configuration.GetValue("UseSwagger", false);

            return useSwagger
                ? services.AddSwaggerGen(SwaggerOptions)
                : services;
        }

        public static IApplicationBuilder UseCompanySwagger(this IApplicationBuilder app, IConfiguration configuration)
        {
            var useSwagger = configuration.GetValue("UseSwagger", false);

            return useSwagger
                ? app.UseSwagger().UseSwaggerUI(SwaggerUiConfig())
                : app;
        }

        private static readonly Action<SwaggerGenOptions> SwaggerOptions = options =>
            options.SwaggerDoc("v1", SwaggerInfo);

        private static readonly OpenApiInfo SwaggerInfo = new OpenApiInfo
        {
            Title = API_NAME,
            Version = "v1",
            Description = API_NAME,
        };

        private static Action<SwaggerUIOptions> SwaggerUiConfig() =>
            c => c.SwaggerEndpoint("/swagger/v1/swagger.json", API_NAME);
    }
}
