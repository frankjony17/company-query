using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CompanySource.Infra.Extensions;
using Company.Query.Domain.Abstractions;
using Company.Query.Domain.Acls.DailyEntry;
using Company.Query.Domain.Configurations;
using Company.Query.Domain.Repositories;
using Company.Query.Domain.Services;
using Company.Query.Infra.CrossCutting.Tools.Logging;
using Company.Query.Infra.Data.Contexts;
using Company.Query.Infra.Data.Repositories;
using Company.Query.Infra.Data.Transaction;

namespace Company.Query.Infra.CrossCutting.IoC
{
    public static class IoC
    {
        private const string CONNECTION_STRING = "ConnectionString";
        private const string MONGO = "MongoDbConfig";
        private const string ACTIONS = "Actions";

        public static IServiceCollection ConfigureContainer(this IServiceCollection services, IConfiguration configuration)
        {
            RegisterRepositories(services, configuration);
            RegisterTools(services, configuration);
            RegisterServices(services);
            RegisterConfigurations(services, configuration);
            RegisterAcls(services, configuration);

            services.AddCompanySourceAclServices();

            return services;
        }

        private static void RegisterAcls(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(configuration.GetSection("Parameters:DailyEntry").Get<DailyEntryConfiguration>());

            services.AddScoped<IDailyEntryAcl, DailyEntryAcl>();
        }

        private static void RegisterTools(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IProducerLogger, ProducerLogger>();
        }

        private static void RegisterRepositories(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetValue<string>(CONNECTION_STRING);
            var mongoConfig = configuration.GetSection(MONGO).Get<MongoDbSettings>();

            services.AddScoped<TransactionFilter>();
            services.AddScoped<IMsSqlContext>(_ => new MsSqlContext(connectionString));
            services.AddScoped<IUnitOfWork>(_ => new UnitOfWork(connectionString));
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IRefundRepository, RefundRepository>();
            services.AddScoped<IReceiptRepository, ReceiptRepository>();
            services.AddScoped<IDetailRepository, DetailRepository>();

            services.AddSingleton<IMongoDbSettings>(t => mongoConfig);
            services.AddScoped(typeof(IMongoDBRepository<>), typeof(MongoDBRepository<>));
        }

        private static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IRefundService, RefundService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IReceiptService, ReceiptService>();
            services.AddScoped<IDetailService, DetailService>();
        }

        private static void RegisterConfigurations(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(configuration.GetSection(ACTIONS).Get<Actions>());

            services.AddScoped<RequestContextHolder>();
        }
    }
}