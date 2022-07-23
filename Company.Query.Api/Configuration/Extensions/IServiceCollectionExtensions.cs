using Microsoft.Extensions.DependencyInjection;
using Company.Query.Api.Filters;
using System.Text.Json;

namespace Company.Query.Api.Configuration.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddControllerWithApiExceptionsFiltersAndJsonOptions(this IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(JsonApiSpecExceptionFilter));
                options.Filters.Add(typeof(LogExceptionFilter));
            }).AddJsonSerializerOptions();

            return services;
        }

        private static void AddJsonSerializerOptions(this IMvcBuilder mvcBuilder)
        {
            mvcBuilder.AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                options.JsonSerializerOptions.IgnoreNullValues = true;
            }).AddNewtonsoftJson();
        }
    }
}
