using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Company.JsonApiSpec.Builders;
using Company.Query.Domain.Abstractions;
using Company.Query.Domain.Providers.Responses;
using Company.Query.Domain.Services;

namespace Company.Query.Api.Controllers
{
    [ApiController]
    [Route("v1/refund")]
    public class RefundController : BaseController
    {
        private readonly IRefundService _refundService;
        private readonly RequestContextHolder _requestContextHolder;

        public RefundController(IRefundService refundService, RequestContextHolder requestContextHolder)
        {
            _refundService = refundService;
            _requestContextHolder = requestContextHolder;
        }

        /// <summary>
        /// Retorna o status de uma devolução
        /// </summary>
        /// <param name="returnId"></param>
        /// <returns></returns>
        [HttpGet("{returnId}")]
        public async Task<IActionResult> Get(string returnId)
        {
            _requestContextHolder.ContextName = "GetPaymentStatus";

            var response = await _refundService.GetRefundStatus(returnId);

            var jsonApiResponse = new JsonApiSpecBuilder<RefundCompleteResponse>(response)
            .AddSelf(SelfLink)
                .Build();

            return Ok(jsonApiResponse);
        }
    }
}
