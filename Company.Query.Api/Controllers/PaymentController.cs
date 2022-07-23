using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Company.JsonApiSpec.Builders;
using Company.Query.Domain.Providers.Responses;
using Company.Query.Api.Responses;
using Company.Query.Domain.Services;
using Microsoft.Extensions.Logging;
using Company.Query.Domain.Abstractions;
using CompanySource.Infra.Abstractions.Logging;

namespace Company.Query.Api.Controllers
{
    [ApiController]
    [Route("v1/payments")]
    public class PaymentController : BaseController
    {
        private readonly IPaymentService _paymentService;
        private readonly IReceiptService _receiptService;
        private readonly IMapper _mapper;
        private readonly ILogger<PaymentController> _logger;
        private readonly RequestContextHolder _requestContextHolder;

        public PaymentController(
            IPaymentService paymentService,
            IReceiptService receiptService,
            IMapper mapper,
            ILogger<PaymentController> logger,
            RequestContextHolder requestContextHolder
            )
        {
            _paymentService = paymentService;
            _receiptService = receiptService;
            _mapper = mapper;
            _logger = logger;
            _requestContextHolder = requestContextHolder;
        }

        /// <summary>
        /// Retorna o status de um pagamento
        /// </summary>
        /// <param name="endToEndId"></param>
        /// <returns></returns>
        [HttpGet("{endToEndId}")]
        public async Task<IActionResult> Get(string endToEndId)
        {
            _logger.BeginCorrelationIdScope(_requestContextHolder.CorrelationId);
            _requestContextHolder.ContextName = "GetPaymentStatus";

            var response = await _paymentService.GetPaymentStatus(endToEndId);

            var jsonApiResponse = new JsonApiSpecBuilder<PaymentCompleteResponse>(response)
            .AddSelf(SelfLink)
                .Build();

            return Ok(jsonApiResponse);
        }

        /// <summary>
        /// Retorna os dados do comprovante de um pagamento recebido
        /// </summary>
        /// <param name="checkingaccount"></param>
        /// <param name="transactionId"></param>
        /// <returns></returns>
        [HttpGet("{checkingaccount:Guid}/in/{transactionId:Guid}")]
        public async Task<IActionResult> GetPaymentReceivedReceipt(Guid checkingaccount, Guid transactionId)
        {
            _logger.BeginCorrelationIdScope(_requestContextHolder.CorrelationId);
            _requestContextHolder.ContextName = "GetPaymentReceivedReceipt";

            var paymentReceived = await _receiptService.GetPaymentReceivedReceipt(checkingaccount, transactionId);
            if (paymentReceived == null)
            {
                _logger.LogWarning($"Payment received receipt NOT found for checking account {checkingaccount} and transaction id {transactionId}");

                return NotFound();
            }

            var response = _mapper.Map<PaymentReceivedResponse>(paymentReceived);

            _logger.LogInformation($"Payment received receipt found for checking account {checkingaccount} and transaction id {transactionId} with end to end id {response.EndToEndId}");

            var jsonApiResponse = new JsonApiSpecBuilder<PaymentReceivedResponse>(response)
                .AddSelf(SelfLink)
                .Build();

            return Ok(jsonApiResponse);
        }

        /// <summary>
        /// Retona os dados do comprovante de um pagamento realizado
        /// </summary>
        /// <param name="checkingaccount"></param>
        /// <param name="transactionId"></param>
        /// <returns></returns>
        [HttpGet("{checkingaccount:Guid}/out/{transactionId:Guid}")]
        public async Task<IActionResult> GetPaymentDoneReceipt(Guid checkingaccount, Guid transactionId)
        {
            _logger.BeginCorrelationIdScope(_requestContextHolder.CorrelationId);
            _requestContextHolder.ContextName = "GetPaymentDoneReceipt";

            var paymentDone = await _receiptService.GetPaymentDoneReceipt(checkingaccount, transactionId);
            if (paymentDone == null)
            {
                _logger.LogWarning($"Payment done receipt NOT found for checking account {checkingaccount} and transaction id {transactionId}");

                return NotFound();
            }

            var response = _mapper.Map<PaymentDoneResponse>(paymentDone);

            _logger.LogInformation($"Payment done receipt found for checking account {checkingaccount} and transaction id {transactionId} with end to end id {response.EndToEndId}");

            var jsonApiResponse = new JsonApiSpecBuilder<PaymentDoneResponse>(response)
                .AddSelf(SelfLink)
                .Build();

            return Ok(jsonApiResponse);
        }

        /// <summary>
        /// Retorna as transações dos lançamentos diários um pagamento/recebimento
        /// </summary>
        /// <param name="endToEndId"></param>
        /// <returns></returns>
        [HttpGet("{endToEndId}/transactions")]
        public async Task<IActionResult> GetCompanyTransactions(string endToEndId)
        {
            _logger.BeginCorrelationIdScope(_requestContextHolder.CorrelationId);
            _requestContextHolder.ContextName = "GetCompanyTransactions";

            var response = await _paymentService.GetCompanyTransactions(endToEndId);

            var jsonApiResponse = new JsonApiSpecBuilder<CompanyTransactionsCompleteResponse>(response)
            .AddSelf(SelfLink)
                .Build();

            return Ok(jsonApiResponse);
        }
    }
}
