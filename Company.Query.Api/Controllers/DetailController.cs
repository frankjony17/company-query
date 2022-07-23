using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Company.JsonApiSpec.Builders;
using CompanySource.Infra.Abstractions.Logging;
using Company.Query.Domain.Abstractions;
using Company.Query.Domain.Providers.Responses;
using Company.Query.Domain.Services;
using System.Threading.Tasks;

namespace Company.Query.Api.Controllers
{
    [ApiController]
    [Route("v1/details")]
    public class DetailController : BaseController
    {
        private readonly IDetailService _detailService;
        private readonly ILogger<DetailController> _logger;
        private readonly RequestContextHolder _requestContextHolder;

        public DetailController(
            IDetailService detailService,
            ILogger<DetailController> logger,
            RequestContextHolder requestContextHolder
            )
        {
            _detailService = detailService;
            _logger = logger;
            _requestContextHolder = requestContextHolder;
        }

        /// <summary>
        /// Retorna o detalhes de um COMPANY
        /// </summary>
        /// <param name="endToEndId"></param>
        /// <returns></returns>
        [HttpGet("{endToEndId}")]
        public async Task<IActionResult> Get(string endToEndId)
        {
            _logger.BeginCorrelationIdScope(_requestContextHolder.CorrelationId);
            _requestContextHolder.ContextName = "GetDetails";

            var response = await _detailService.Get(endToEndId);

            var jsonApiResponse = new JsonApiSpecBuilder<CompanyDetailResponse>(response)
            .AddSelf(SelfLink)
                .Build();

            return Ok(jsonApiResponse);
        }
    }
}
