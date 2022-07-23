using AutoFixture;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Company.Query.Domain.Abstractions;
using Company.Query.Domain.Abstractions.Entities;
using Company.Query.Domain.Repositories;
using Company.Query.Domain.Services;
using System;
using System.Threading.Tasks;

namespace Company.Query.Tests.domain.Services
{
    public class ReceiptServiceTest
    {
        private ReceiptService _receiptService;
        private Fixture _fixture;
        private Mock<IReceiptRepository> _receiptRepository;
        private Mock<ILogger<ReceiptService>> _logger;
        private RequestContextHolder _requestContextHolder;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _receiptRepository = new Mock<IReceiptRepository>();
            _logger = new Mock<ILogger<ReceiptService>>();
            _requestContextHolder = new RequestContextHolder();
        }

        [Test]
        public void GetPaymentReceivedReceipt_ShouldBeSuccess()
        {
            // Arrange
            var paymentReceivedFake = new PaymentReceived();
            _receiptRepository.Setup(x => x.GetPaymentReceivedReceipt(It.IsAny<Guid>(), It.IsAny<Guid>())).ReturnsAsync(paymentReceivedFake);
            _receiptService = new ReceiptService(_receiptRepository.Object, _logger.Object, _requestContextHolder);

            // Action
            Func<Task> act = async () => await _receiptService.GetPaymentReceivedReceipt(Guid.NewGuid(), Guid.NewGuid());

            // Assert
            act.Should().NotThrow();
        }

        [Test]
        public void GetPaymentDoneReceipt_ShouldBeSuccess()
        {
            // Arrange
            var paymentDoneFake = new PaymentDone();
            _receiptRepository.Setup(x => x.GetPaymentDoneReceipt(It.IsAny<Guid>(), It.IsAny<Guid>())).ReturnsAsync(paymentDoneFake);
            _receiptService = new ReceiptService(_receiptRepository.Object, _logger.Object, _requestContextHolder);

            // Action
            Func<Task> act = async () => await _receiptService.GetPaymentDoneReceipt(Guid.NewGuid(), Guid.NewGuid());

            // Assert
            act.Should().NotThrow();
        }
    }
}
