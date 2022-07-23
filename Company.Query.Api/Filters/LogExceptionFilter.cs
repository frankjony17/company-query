using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using CompanySource.Infra.Abstractions.Logging;
using Company.Query.Domain.Abstractions;

namespace Company.Query.Api.Filters
{
    public class LogExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<LogExceptionFilter> _logger;
        private readonly RequestContextHolder _requestContextHolder;

        public LogExceptionFilter(ILogger<LogExceptionFilter> logger, RequestContextHolder requestContextHolder)
        {
            _logger = logger;
            _requestContextHolder = requestContextHolder;
        }

        public void OnException(ExceptionContext context)
        {
            _logger.BeginCorrelationIdScope(_requestContextHolder.CorrelationId);
            _logger.LogError($"Error during {_requestContextHolder.ContextName}. Exception message: {context.Exception.InnerException?.Message ?? context.Exception.Message }");
        }
    }
}
