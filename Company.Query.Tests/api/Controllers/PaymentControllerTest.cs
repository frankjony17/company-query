using AutoFixture;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Company.Query.Api.Controllers;
using Company.Query.Domain.Abstractions.Entities;
using Company.Query.Domain.Providers.Responses;
using Company.Query.Domain.Services;
using System;
using System.Threading.Tasks;
using Company.Query.Api.Responses;
using Company.Query.Domain.Abstractions;

namespace Company.Query.Tests.api.Controllers
{
    public class PaymentControllerTest
    {
        private PaymentController _paymentController;
        private Fixture _fixture;
        private Mock<IPaymentService> _paymentService;
        private Mock<IReceiptService> _receiptService;
        private Mock<IMapper> _mapper;
        private Mock<ILogger<PaymentController>> _logger;
        private RequestContextHolder _requestContextHolder;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _logger = new Mock<ILogger<PaymentController>>();
            _paymentService = new Mock<IPaymentService>();
            _receiptService = new Mock<IReceiptService>();
            _mapper = new Mock<IMapper>();
            _requestContextHolder = new RequestContextHolder();
        }

        [Test]
        public void Constructor_ShouldBeSuccess()
        {
            // Arrange
            // Action
            Action act = () => _paymentController = new PaymentController(_paymentService.Object, _receiptService.Object, _mapper.Object, _logger.Object, _requestContextHolder);

            // Assert
            act.Should().NotThrow();
        }

        [Test]
        public void Get_ReturnStatus200OK()
        {
            // Arrange
            IActionResult result = null;
            var paymentCompleteResponseFake = _fixture.Create<PaymentCompleteResponse>();
            _paymentService.Setup(x => x.GetPaymentStatus(It.IsAny<string>())).ReturnsAsync(paymentCompleteResponseFake);
            _paymentController = new PaymentController(_paymentService.Object, _receiptService.Object, _mapper.Object, _logger.Object, _requestContextHolder);

            // Action
            Func<Task> act = async () => result = await _paymentController.Get(string.Empty);

            // Assert
            act.Should().NotThrow();
            Assert.IsInstanceOf<OkObjectResult>(result);
        }


        [Test]
        public void GetPaymentReceivedReceipt_ReturnStatus404NotFound_WhenPaymentReceivedIsNull()
        {
            // Arrange
            IActionResult result = null;
            PaymentReceived paymentReceivedFake = null;
            _receiptService.Setup(x => x.GetPaymentReceivedReceipt(It.IsAny<Guid>(), It.IsAny<Guid>())).ReturnsAsync(paymentReceivedFake);
            _paymentController = new PaymentController(_paymentService.Object, _receiptService.Object, _mapper.Object, _logger.Object, _requestContextHolder);

            // Action
            Func<Task> act = async () => result = await _paymentController.GetPaymentReceivedReceipt(Guid.NewGuid(), Guid.NewGuid());

            // Assert
            act.Should().NotThrow();
            result.Should().BeOfType<NotFoundResult>();
        }


        [Test]
        public void GetPaymentReceivedReceipt_ReturnStatus200OK_WhenPaymentReceivedIsNotNull()
        {
            // Arrange
            IActionResult result = null;
            var paymentReceivedFake = new PaymentReceived();
            var paymentReceivedResponseFake = _fixture.Create<PaymentReceivedResponse>();
            _mapper.Setup(x => x.Map<PaymentReceivedResponse>(paymentReceivedFake)).Returns(paymentReceivedResponseFake);
            _receiptService.Setup(x => x.GetPaymentReceivedReceipt(It.IsAny<Guid>(), It.IsAny<Guid>())).ReturnsAsync(paymentReceivedFake);
            _paymentController = new PaymentController(_paymentService.Object, _receiptService.Object, _mapper.Object, _logger.Object, _requestContextHolder);

            // Action
            Func<Task> act = async () => result = await _paymentController.GetPaymentReceivedReceipt(Guid.NewGuid(), Guid.NewGuid());

            // Assert
            act.Should().NotThrow();
            result.Should().BeOfType<OkObjectResult>();
        }


        [Test]
        public void GetPaymentDoneReceipt_ReturnStatus404NotFound_WhenPaymentDoneIsNull()
        {
            // Arrange
            IActionResult result = null;
            PaymentDone paymentDoneFake = null;
            _receiptService.Setup(x => x.GetPaymentDoneReceipt(It.IsAny<Guid>(), It.IsAny<Guid>())).ReturnsAsync(paymentDoneFake);
            _paymentController = new PaymentController(_paymentService.Object, _receiptService.Object, _mapper.Object, _logger.Object, _requestContextHolder);

            // Action
            Func<Task> act = async () => result = await _paymentController.GetPaymentDoneReceipt(Guid.NewGuid(), Guid.NewGuid());

            // Assert
            act.Should().NotThrow();
            result.Should().BeOfType<NotFoundResult>();
        }


        [Test]
        public void GetPaymentDoneReceipt_ReturnStatus200OK_WhenPaymentDoneIsNotNull()
        {
            // Arrange
            IActionResult result = null;
            var paymentDoneFake = new PaymentDone();
            var paymentDoneResponseFake = _fixture.Create<PaymentDoneResponse>();
            _mapper.Setup(x => x.Map<PaymentDoneResponse>(paymentDoneFake)).Returns(paymentDoneResponseFake);
            _receiptService.Setup(x => x.GetPaymentDoneReceipt(It.IsAny<Guid>(), It.IsAny<Guid>())).ReturnsAsync(paymentDoneFake);
            _paymentController = new PaymentController(_paymentService.Object, _receiptService.Object, _mapper.Object, _logger.Object, _requestContextHolder);

            // Action
            Func<Task> act = async () => result = await _paymentController.GetPaymentDoneReceipt(Guid.NewGuid(), Guid.NewGuid());

            // Assert
            act.Should().NotThrow();
            result.Should().BeOfType<OkObjectResult>();
        }

        [Test]
        public void GetCompanyTransactions_ReturnStatus200OK_WhenCreateTransctions()
        {
            // Arrange
            IActionResult result = null;
            var companyTransactionsCompleteResponseFake = _fixture.Create<CompanyTransactionsCompleteResponse>();
            companyTransactionsCompleteResponseFake.CreateTransctions();
            _paymentService.Setup(x => x.GetCompanyTransactions(It.IsAny<string>())).ReturnsAsync(companyTransactionsCompleteResponseFake);
            _paymentController = new PaymentController(_paymentService.Object, _receiptService.Object, _mapper.Object, _logger.Object, _requestContextHolder);

            // Action
            Func<Task> act = async () => result = await _paymentController.GetCompanyTransactions(string.Empty);

            // Assert
            act.Should().NotThrow();
            result.Should().BeOfType<OkObjectResult>();
        }
    }
}
