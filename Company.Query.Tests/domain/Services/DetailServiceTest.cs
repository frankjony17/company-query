using AutoFixture;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Company.Query.Domain.Abstractions;
using Company.Query.Domain.Providers.Responses;
using Company.Query.Domain.Repositories;
using Company.Query.Domain.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Company.Query.Tests.domain.Services
{
    public class DetailServiceTest
    {
        private IDetailService _detailService;
        private Fixture _fixture;
        private Mock<IDetailRepository> _detailRepository;
        private Mock<ILogger<DetailService>> _logger;
        private RequestContextHolder _requestContextHolder;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _detailRepository = new Mock<IDetailRepository>();
            _logger = new Mock<ILogger<DetailService>>();
            _requestContextHolder = new RequestContextHolder();
        }

        [Test]
        public void GetDetail_ReturnNull_WhenResponseIsNull()
        {
            // Arrange
            CompanyDetailResponse result = null;
            _detailRepository.Setup(x => x.Get(It.IsAny<string>())).ReturnsAsync(result);
            _detailService = new DetailService(_detailRepository.Object, _logger.Object, _requestContextHolder);

            // Action
            Func<Task> act = async () => result = await _detailService.Get(string.Empty);

            // Assert
            act.Should().NotThrow();
            result.Should().BeNull();
        }

        [Test]
        public void GetDetail_ReturnNotNull_WhenDetailsIsNull()
        {
            // Arrange
            var result = _fixture.Create<CompanyDetailResponse>();
            result.CompanyDetailPayment.PaymentEvents = null;
            result.CompanyDetailPayment = null;
            result.CompanyDetailRefund = null;
            result.CompanyDetailReceipt = null;
            _detailRepository.Setup(x => x.Get(It.IsAny<string>())).ReturnsAsync(result);
            _detailService = new DetailService(_detailRepository.Object, _logger.Object, _requestContextHolder);

            // Action
            Func<Task> act = async () => result = await _detailService.Get(string.Empty);

            // Assert
            act.Should().NotThrow();
            result.Should().NotBeNull();
            result.CompanyDetailPayment.Should().BeNull();
            result.CompanyDetailRefund.Should().BeNull();
            result.CompanyDetailReceipt.Should().BeNull();
        }

        [Test]
        public void GetPaymentDoneReceipt_ShouldBeSuccess()
        {
            // Arrange
            var detailFake = _fixture.Create<CompanyDetailResponse>();
            detailFake.CompanyDetailPayment.PaymentEvents = null;
            detailFake.CompanyDetailPayment = null;
            detailFake.CompanyDetailRefund = null;
            detailFake.CompanyDetailReceipt = null;
            var detailPaymentResponse = _fixture.Create<CompanyDetailPaymentResponse>();
            var detailPaymentEventResponse = _fixture.Create<IEnumerable<CompanyDetailPaymentEventResponse>>();
            var detailRefundEventResponse = _fixture.Create<IEnumerable<CompanyDetailRefundEventResponse>>();
            var detailReceiptResponse = _fixture.Create<IEnumerable<CompanyDetailReceiptResponse>>();
            _detailRepository.Setup(x => x.Get(It.IsAny<string>())).ReturnsAsync(detailFake);
            _detailRepository.Setup(x => x.GetPayment(It.IsAny<string>())).ReturnsAsync(detailPaymentResponse);
            _detailRepository.Setup(x => x.GetPaymentEvents(It.IsAny<string>())).ReturnsAsync(detailPaymentEventResponse);
            _detailRepository.Setup(x => x.GetRefundEvents(It.IsAny<string>())).ReturnsAsync(detailRefundEventResponse);
            _detailRepository.Setup(x => x.GetReceipts(It.IsAny<string>())).ReturnsAsync(detailReceiptResponse);
            _detailService = new DetailService(_detailRepository.Object, _logger.Object, _requestContextHolder);

            // Action
            Func<Task> act = async () => detailFake = await _detailService.Get(string.Empty);

            // Assert
            act.Should().NotThrow();
            Assert.IsInstanceOf<CompanyDetailResponse>(detailFake);
            detailFake.EndToEndId.Should().NotBeNull();
            detailFake.CompanyDetailPayment.Should().NotBeNull();
            detailFake.CompanyDetailPayment.PaymentEvents.Should().NotBeNull();
            detailFake.CompanyDetailRefund.Should().NotBeNull();
            detailFake.CompanyDetailReceipt.Should().NotBeNull();
        }
    }
}
