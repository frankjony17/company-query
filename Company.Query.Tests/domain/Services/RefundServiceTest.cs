using AutoFixture;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Company.Query.Domain.Abstractions;
using Company.Query.Domain.Acls.DailyEntry;
using Company.Query.Domain.Configurations;
using Company.Query.Domain.Providers.Acls;
using Company.Query.Domain.Providers.Responses;
using Company.Query.Domain.Repositories;
using Company.Query.Domain.Services;
using System;
using System.Threading.Tasks;

namespace Company.Query.Tests.domain.Services
{
    public class RefundServiceTest
    {
        private RefundService _refundService;
        private Fixture _fixture;
        private Mock<IRefundRepository> _refundRepository;
        private Mock<IDailyEntryAcl> _dailyEntryAcl;
        private Mock<Actions> _actions;
        private Mock<ILogger<RefundService>> _logger;
        private RequestContextHolder _requestContextHolder;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _refundRepository = new Mock<IRefundRepository>();
            _dailyEntryAcl = new Mock<IDailyEntryAcl>();
            _actions = new Mock<Actions>();
            _logger = new Mock<ILogger<RefundService>>();
            _requestContextHolder = new RequestContextHolder();
        }

        [Test]
        public void GetRefundStatus_ReturnRefundNull_WhenRefundStatusIsNull()
        {
            // Arrange
            RefundCompleteResponse result = null;
            RefundStatusResponse refundStatusResponseFake = null;
            _refundRepository.Setup(x => x.GetRefundStatus(It.IsAny<string>())).ReturnsAsync(refundStatusResponseFake);
            _refundService = new RefundService(_refundRepository.Object, _actions.Object, _dailyEntryAcl.Object, _logger.Object, _requestContextHolder);

            // Action
            Func<Task> act = async () => result = await _refundService.GetRefundStatus(string.Empty);

            // Assert
            act.Should().NotThrow();
            Assert.IsInstanceOf<RefundCompleteResponse>(result);
            result.Refund.Should().BeNull();
        }

        [Test]
        public void GetRefundStatus_ReturnRefundNotNull_WhenRefundStatusIsNotNull()
        {
            // Arrange
            RefundCompleteResponse result = null;
            var refundStatusResponseFake = _fixture.Create<RefundStatusResponse>();
            var dailyEntryResponseDtoFake = _fixture.Create<DailyEntryResponseDto>();
            _refundRepository.Setup(x => x.GetRefundStatus(It.IsAny<string>())).ReturnsAsync(refundStatusResponseFake);
            _dailyEntryAcl.Setup(x => x.GetLegacyIdByTransactionUuid(It.IsAny<Guid>())).ReturnsAsync(dailyEntryResponseDtoFake);
            _refundService = new RefundService(_refundRepository.Object, _actions.Object, _dailyEntryAcl.Object, _logger.Object, _requestContextHolder);

            // Action
            Func<Task> act = async () => result = await _refundService.GetRefundStatus(string.Empty);

            // Assert
            act.Should().NotThrow();
            Assert.IsInstanceOf<RefundCompleteResponse>(result);
            result.Refund.Should().NotBeNull();
            dailyEntryResponseDtoFake.TransactionId.Should().Be(refundStatusResponseFake.LegacyTransactionId);
        }

    }
}
