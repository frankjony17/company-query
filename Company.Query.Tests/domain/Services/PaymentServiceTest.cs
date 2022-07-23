using AutoFixture;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Company.Query.Domain.Abstractions;
using Company.Query.Domain.Acls.DailyEntry;
using Company.Query.Domain.Configurations;
using Company.Query.Domain.Enums;
using Company.Query.Domain.Exceptions;
using Company.Query.Domain.Providers.Acls;
using Company.Query.Domain.Providers.Responses;
using Company.Query.Domain.Repositories;
using Company.Query.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Query.Tests.domain.Services
{
    public class PaymentServiceTest
    {
        private PaymentService _paymentService;
        private Fixture _fixture;
        private Mock<Actions> _actions;
        private Mock<IPaymentRepository> _paymentRepository;
        private Mock<IDailyEntryAcl> _dailyEntryAcl;
        private Mock<ILogger<PaymentService>> _logger;
        private RequestContextHolder _requestContextHolder;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _actions = new Mock<Actions>();
            _paymentRepository = new Mock<IPaymentRepository>();
            _dailyEntryAcl = new Mock<IDailyEntryAcl>();
            _logger = new Mock<ILogger<PaymentService>>();
            _requestContextHolder = new RequestContextHolder();
        }

        [Test]
        public void GetPaymentStatus_ReturnPaymentNull_WhenPaymentStatusResponseIsNull()
        {
            // Arrange
            PaymentCompleteResponse result = null;
            PaymentStatusResponse paymentStatusResponseFake = null;
            _paymentRepository.Setup(x => x.GetPaymentStatus(It.IsAny<string>())).ReturnsAsync(paymentStatusResponseFake);
            _paymentService = new PaymentService(_paymentRepository.Object, _dailyEntryAcl.Object, _actions.Object, _logger.Object, _requestContextHolder);

            // Action
            Func<Task> act = async () => result = await _paymentService.GetPaymentStatus(string.Empty);

            // Assert
            act.Should().NotThrow();
            Assert.IsInstanceOf<PaymentCompleteResponse>(result);
            result.Payment.Should().BeNull();
        }

        [Test]
        public void GetPaymentStatus_ReturnPaymentNotNull_WhenPaymentStatusResponseIsNotNull()
        {
            // Arrange
            var endToEndId = string.Empty;
            PaymentCompleteResponse result = null;
            var paymentStatusResponseFake = _fixture.Create<PaymentStatusResponse>();
            var dailyEntryResponseDtoFake = _fixture.Create<DailyEntryResponseDto>();
            _paymentRepository.Setup(x => x.GetPaymentStatus(It.IsAny<string>())).ReturnsAsync(paymentStatusResponseFake);
            _dailyEntryAcl.Setup(x => x.GetLegacyIdByTransactionUuid(It.IsAny<Guid>())).ReturnsAsync(dailyEntryResponseDtoFake);
            _paymentService = new PaymentService(_paymentRepository.Object, _dailyEntryAcl.Object, _actions.Object, _logger.Object, _requestContextHolder);

            // Action
            Func<Task> act = async () => result = await _paymentService.GetPaymentStatus(endToEndId);

            // Assert
            act.Should().NotThrow();
            Assert.IsInstanceOf<PaymentCompleteResponse>(result);
            result.Payment.Should().NotBeNull();
            dailyEntryResponseDtoFake.TransactionId.Should().Be(paymentStatusResponseFake.LegacyTransactionId);
        }

        [Test]
        public void GetCompanyTransactions_ReturnCompanyTransactionsIsNull_WhenListCompanyTransactionsDbResponseIsEmpty()
        {
            // Arrange
            var endToEndId = string.Empty;
            CompanyTransactionsCompleteResponse result = null;
            var listCompanyTransactionsDbResponseFake = new List<CompanyTransactionsDbResponse>();
            _paymentRepository.Setup(x => x.GetCompanyTransactions(It.IsAny<string>())).ReturnsAsync(listCompanyTransactionsDbResponseFake);
            _paymentService = new PaymentService(_paymentRepository.Object, _dailyEntryAcl.Object, _actions.Object, _logger.Object, _requestContextHolder);

            // Action
            Func<Task> act = async () => result = await _paymentService.GetCompanyTransactions(endToEndId);

            // Assert
            act.Should().Throw<NotFoundCustomException>();
        }

        [Test]
        public void GetCompanyTransactions_ReturnCompanyTransactionsIsNull_WhenDailyEntryTransactionsIsEmpty()
        {
            // Arrange
            var endToEndId = string.Empty;
            CompanyTransactionsCompleteResponse result = null;
            var listCompanyTransactionsDbResponseFake = _fixture.Create<List<CompanyTransactionsDbResponse>>();
            var listDailyEntryResponseDtoFake = new List<DailyEntryResponseDto>();
            _paymentRepository.Setup(x => x.GetCompanyTransactions(It.IsAny<string>())).ReturnsAsync(listCompanyTransactionsDbResponseFake);
            _dailyEntryAcl.Setup(x => x.GetDailyEntryByTransactions(It.IsAny<IEnumerable<Guid>>())).ReturnsAsync(listDailyEntryResponseDtoFake);
            _paymentService = new PaymentService(_paymentRepository.Object, _dailyEntryAcl.Object, _actions.Object, _logger.Object, _requestContextHolder);

            // Action
            Func<Task> act = async () => result = await _paymentService.GetCompanyTransactions(endToEndId);

            // Assert
            act.Should().NotThrow();
            Assert.IsInstanceOf<CompanyTransactionsCompleteResponse>(result);
            result.Should().NotBeNull();
            result.CompanyTransactions.Should().BeNull();
            result.Id.Should().Be(endToEndId);
            result.Actions.Should().Be(_actions.Object);
        }

        [Test]
        public void GetCompanyTransactions_ReturnAllowRefundIsFalse_WhenTypeOperationIsDebit()
        {
            // Arrange
            var endToEndId = string.Empty;
            CompanyTransactionsCompleteResponse result = null;
            var listCompanyTransactionsDbResponseFake = _fixture.Create<List<CompanyTransactionsDbResponse>>();
            listCompanyTransactionsDbResponseFake.ForEach(x => x.AllowRefund = false);
            listCompanyTransactionsDbResponseFake.ForEach(x => x.IsSource = true);
            var listDailyEntryResponseDtoFake = _fixture.Create<List<DailyEntryResponseDto>>();
            _paymentRepository.Setup(x => x.GetCompanyTransactions(It.IsAny<string>())).ReturnsAsync(listCompanyTransactionsDbResponseFake);
            _dailyEntryAcl.Setup(x => x.GetDailyEntryByTransactions(It.IsAny<IEnumerable<Guid>>())).ReturnsAsync(listDailyEntryResponseDtoFake);
            _paymentService = new PaymentService(_paymentRepository.Object, _dailyEntryAcl.Object, _actions.Object, _logger.Object, _requestContextHolder);

            // Action
            Func<Task> act = async () => result = await _paymentService.GetCompanyTransactions(endToEndId);

            // Assert
            act.Should().NotThrow();
            Assert.IsInstanceOf<CompanyTransactionsCompleteResponse>(result);
            result.Should().NotBeNull();
            result.Id.Should().Be(endToEndId);
            result.Actions.Should().Be(_actions.Object);
            result.CompanyTransactions.Should().NotBeNull();
            result.CompanyTransactions.AllowRefund.Should().Be(false);
        }

        [Test]
        public void GetCompanyTransactions_ReturnAllAllowRefoundsEqualsIsSource_WhenTypeOperationIsCredit()
        {
            // Arrange
            var endToEndId = string.Empty;
            CompanyTransactionsCompleteResponse result = null;
            var listCompanyTransactionsDbResponseFake = _fixture.Create<List<CompanyTransactionsDbResponse>>();
            listCompanyTransactionsDbResponseFake.ForEach(x => x.AllowRefund = true);
            listCompanyTransactionsDbResponseFake.ForEach(x => x.IsSource = true);
            var listDailyEntryResponseDtoFake = _fixture.Create<List<DailyEntryResponseDto>>();
            listDailyEntryResponseDtoFake.ForEach(x => x.TypeOperation = TipoOperacao.Credito);
            listDailyEntryResponseDtoFake.ForEach(x => x.Value = 100);
            _paymentRepository.Setup(x => x.GetCompanyTransactions(It.IsAny<string>())).ReturnsAsync(listCompanyTransactionsDbResponseFake);
            _dailyEntryAcl.Setup(x => x.GetDailyEntryByTransactions(It.IsAny<IEnumerable<Guid>>())).ReturnsAsync(listDailyEntryResponseDtoFake);
            _paymentService = new PaymentService(_paymentRepository.Object, _dailyEntryAcl.Object, _actions.Object, _logger.Object, _requestContextHolder);

            // Action
            Func<Task> act = async () => result = await _paymentService.GetCompanyTransactions(endToEndId);

            // Assert
            act.Should().NotThrow();
            Assert.IsInstanceOf<CompanyTransactionsCompleteResponse>(result);
            result.Should().NotBeNull();
            result.Id.Should().Be(endToEndId);
            result.Actions.Should().Be(_actions.Object);
            result.CompanyTransactions.Should().NotBeNull();
            result.CompanyTransactions.AllowRefund.Should().Be(true);
        }

        [Test]
        public void GetCompanyTransactions_ReturnOneAllowRefoundEqualsIsSource_TypeOperationCredito()
        {
            // Arrange
            var endToEndId = string.Empty;
            CompanyTransactionsCompleteResponse result = null;
            var listCompanyTransactionsDbResponseFake = _fixture.Create<List<CompanyTransactionsDbResponse>>();
            listCompanyTransactionsDbResponseFake.ForEach(x => x.AllowRefund = false);
            listCompanyTransactionsDbResponseFake.ForEach(x => x.IsSource = true);
            listCompanyTransactionsDbResponseFake[0].AllowRefund = true;
            var listDailyEntryResponseDtoFake = _fixture.Create<List<DailyEntryResponseDto>>();
            listDailyEntryResponseDtoFake.ForEach(x => x.TypeOperation = TipoOperacao.Credito);
            _paymentRepository.Setup(x => x.GetCompanyTransactions(It.IsAny<string>())).ReturnsAsync(listCompanyTransactionsDbResponseFake);
            _dailyEntryAcl.Setup(x => x.GetDailyEntryByTransactions(It.IsAny<IEnumerable<Guid>>())).ReturnsAsync(listDailyEntryResponseDtoFake);
            _paymentService = new PaymentService(_paymentRepository.Object, _dailyEntryAcl.Object, _actions.Object, _logger.Object, _requestContextHolder);

            // Action
            Func<Task> act = async () => result = await _paymentService.GetCompanyTransactions(endToEndId);

            // Assert
            act.Should().NotThrow();
            Assert.IsInstanceOf<CompanyTransactionsCompleteResponse>(result);
            result.Should().NotBeNull();
            result.Id.Should().Be(endToEndId);
            result.Actions.Should().Be(_actions.Object);
            result.CompanyTransactions.Should().NotBeNull();
            result.CompanyTransactions.AllowRefund.Should().Be(true);
        }
    }
}
