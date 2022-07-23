using AutoFixture;
using FluentAssertions;
using NUnit.Framework;
using Company.Query.Domain.Enums;
using Company.Query.Domain.Providers.Acls;
using Company.Query.Domain.Providers.Responses;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Company.Query.Tests.domain.Providers.Responses
{
    public class CompanyTransactionsResponseTest
    {
        private Fixture _fixture;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
        }

        [Test]
        public void Create_ShouldBeSuccess()
        {
            // Arrange
            CompanyTransactionsResponse companyTransactionsResponseFake = null;

            // Action
            Action act = () => companyTransactionsResponseFake = CompanyTransactionsResponse.Create();

            // Assert
            act.Should().NotThrow();
            companyTransactionsResponseFake.AssociatedList.Should().NotBeNull();
        }

        static IEnumerable AllowRefundCases
        {
            get
            {
                yield return new TestCaseData(true);
                yield return new TestCaseData(false);
            }
        }
        [Test]
        [TestCaseSource(nameof(AllowRefundCases))]
        public void SetAllowRefund_ShouldBeSuccess_WhenParameterAllowRefund(bool allowRefund)
        {
            // Arrange
            var companyTransactionsResponseFake = CompanyTransactionsResponse.Create();

            // Action
            Action act = () => companyTransactionsResponseFake.SetAllowRefund(allowRefund);

            // Assert
            act.Should().NotThrow();
            companyTransactionsResponseFake.AllowRefund.Should().Be(allowRefund);
        }

        [Test]
        public void AddTransactionsList_ShouldBeSuccess()
        {
            // Arrange
            var companyTransactionsResponseFake = CompanyTransactionsResponse.Create();
            var dailyEntryResponseDtosFake = _fixture.Create<List<DailyEntryResponseDto>>();
            var result = dailyEntryResponseDtosFake
                            .Select(s => CompanyTransactionsAssociatedListResponse.Create(s.TransactionId, s.TransactionUuid, s.Value, s.Description, s.TransactionDate, s.CategoryId, s.TypeOperation))
                            .OrderByDescending(t => t.TransactionDate).ToList();

            // Action
            Action act = () => companyTransactionsResponseFake.AddTransactionsList(dailyEntryResponseDtosFake);

            // Assert
            act.Should().NotThrow();
            companyTransactionsResponseFake.AssociatedList.Should().NotBeNull();
            companyTransactionsResponseFake.AssociatedList.Should().BeEquivalentTo(result);
            companyTransactionsResponseFake.AssociatedList[0].TransactionId.Should().Be(result[0].TransactionId);
        }

        [Test]
        public void SetSourceTransaction_ShouldBeSuccess_WhenOperationTypeIsDebit()
        {
            // Arrange
            var companyTransactionsResponseFake = CompanyTransactionsResponse.Create();
            var associatedListFake = _fixture.Create<List<CompanyTransactionsAssociatedListResponse>>();
            var transactionUuidFake = associatedListFake.FirstOrDefault().TransactionUuid;
            associatedListFake.ForEach(a => a.TypeOperation = TipoOperacao.Debito);
            companyTransactionsResponseFake.AssociatedList = associatedListFake;
            companyTransactionsResponseFake.SetAllowRefund(true);

            // Action
            Action act = () => companyTransactionsResponseFake.SetSourceTransaction(transactionUuidFake);

            // Assert
            act.Should().NotThrow();
            companyTransactionsResponseFake.AssociatedList.Should().NotBeNull();
            companyTransactionsResponseFake.AllowRefund.Should().BeFalse();
        }

        [Test]
        public void SetSourceTransaction_ShouldBeSuccess_WhenOperationTypeIsCredit()
        {
            // Arrange
            var companyTransactionsResponseFake = CompanyTransactionsResponse.Create();
            var associatedListFake = _fixture.Create<List<CompanyTransactionsAssociatedListResponse>>();
            var transactionUuidFake = associatedListFake.FirstOrDefault().TransactionUuid;
            associatedListFake.ForEach(a => a.TypeOperation = TipoOperacao.Credito);
            companyTransactionsResponseFake.SetAllowRefund(true);

            // Action
            Action act = () => companyTransactionsResponseFake.SetSourceTransaction(transactionUuidFake);

            // Assert
            act.Should().NotThrow();
            companyTransactionsResponseFake.AssociatedList.Should().NotBeNull();
            companyTransactionsResponseFake.AllowRefund.Should().BeTrue();
        }

        [Test]
        public void SetReceiptDate_ShouldBeSuccess()
        {
            // Arrange
            var receiptDateFake = DateTime.Now;
            var companyTransactionsResponseFake = CompanyTransactionsResponse.Create();

            // Action
            Action act = () => companyTransactionsResponseFake.SetReceiptDate(receiptDateFake);

            // Assert
            act.Should().NotThrow();
            companyTransactionsResponseFake.ReceiptDate.Should().Be(receiptDateFake);
        }

        [Test]
        public void CalculateRemainingDays_ShouldBeGreatherThan0_WhenReceiptDateIsToday()
        {
            // Arrange
            DateTime receiptDateFake = DateTime.Now;
            var companyTransactionsResponseFake = CompanyTransactionsResponse.Create();
            companyTransactionsResponseFake.SetReceiptDate(receiptDateFake);
            var date = receiptDateFake.AddDays(90);
            var remainingDaysResult = (int)date.Subtract(DateTime.Now).TotalDays;

            // Action
            Action act = () => companyTransactionsResponseFake.CalculateRemainingDays();

            // Assert
            act.Should().NotThrow();
            companyTransactionsResponseFake.RemainingDays.Should().BeGreaterOrEqualTo(0);
            companyTransactionsResponseFake.RemainingDays.Should().Be(remainingDaysResult);
        }

        [Test]
        public void CalculateRemainingDays_ShouldBeEquals0_WhenReceiptDateIs90Days()
        {
            // Arrange
            DateTime receiptDateFake = DateTime.Now.AddDays(-90);
            var companyTransactionsResponseFake = CompanyTransactionsResponse.Create();
            companyTransactionsResponseFake.SetReceiptDate(receiptDateFake);
            var date = receiptDateFake.AddDays(90);
            var remainingDaysResult = (int)date.Subtract(DateTime.Now).TotalDays;

            // Action
            Action act = () => companyTransactionsResponseFake.CalculateRemainingDays();

            // Assert
            act.Should().NotThrow();
            companyTransactionsResponseFake.RemainingDays.Should().Be(0);
            companyTransactionsResponseFake.RemainingDays.Should().Be(remainingDaysResult);
        }

        [Test]
        public void CalculateRemainingDays_ShouldBeEquals0_WhenReceiptDateIsBefore90Days()
        {
            // Arrange
            DateTime receiptDateFake = DateTime.Now.AddDays(-91);
            var companyTransactionsResponseFake = CompanyTransactionsResponse.Create();
            companyTransactionsResponseFake.SetReceiptDate(receiptDateFake);

            // Action
            Action act = () => companyTransactionsResponseFake.CalculateRemainingDays();

            // Assert
            act.Should().NotThrow();
            companyTransactionsResponseFake.RemainingDays.Should().Be(0);
        }

        static IEnumerable CalculateRemainingValueCases
        {
            get
            {
                yield return new TestCaseData(
                    new List<CompanyTransactionsAssociatedListResponse>()
                        {
                            CompanyTransactionsAssociatedListResponse.Create(1 ,Guid.NewGuid(), 100, "raw 1", DateTime.Now, 0, TipoOperacao.Credito),
                            CompanyTransactionsAssociatedListResponse.Create(2 ,Guid.NewGuid(), 90, "raw 2", DateTime.Now.AddDays(1), 0, TipoOperacao.Credito)
                        },
                    true
                    )
                    .SetDescription("RemainingValueGreatherThan0");

                yield return new TestCaseData(
                    new List<CompanyTransactionsAssociatedListResponse>()
                        {
                            CompanyTransactionsAssociatedListResponse.Create(1 ,Guid.NewGuid(), 100, "raw 1", DateTime.Now, 0, TipoOperacao.Debito),
                            CompanyTransactionsAssociatedListResponse.Create(2 ,Guid.NewGuid(), 90, "raw 2", DateTime.Now.AddDays(1), 0, TipoOperacao.Debito)
                        },
                    false
                    )
                    .SetDescription("RemainingValueLessThan0");

                yield return new TestCaseData(
                    new List<CompanyTransactionsAssociatedListResponse>()
                        {
                            CompanyTransactionsAssociatedListResponse.Create(1 ,Guid.NewGuid(), 0, "raw 1", DateTime.Now, 0, TipoOperacao.Debito),
                            CompanyTransactionsAssociatedListResponse.Create(2 ,Guid.NewGuid(), 0, "raw 2", DateTime.Now.AddDays(1), 0, TipoOperacao.Debito)
                        },
                    false
                    )
                    .SetDescription("RemainingValueEquals0");
            }
        }
        [Test]
        [TestCaseSource(nameof(CalculateRemainingValueCases))]
        public void CalculateRemainingValue_ShouldBeSuccess(List<CompanyTransactionsAssociatedListResponse> companyTransactionsAssociatedListResponseFake, bool allorRefundRes)
        {
            // Arrange
            var companyTransactionsResponseFake = CompanyTransactionsResponse.Create();
            companyTransactionsResponseFake.AssociatedList = companyTransactionsAssociatedListResponseFake;
            companyTransactionsResponseFake.SetAllowRefund(true);

            // Action
            Action act = () => companyTransactionsResponseFake.CalculateRemainingValue();

            // Assert
            act.Should().NotThrow();
            companyTransactionsResponseFake.RemainingValue.Should().Be(companyTransactionsAssociatedListResponseFake.Sum(s => s.Value));
            companyTransactionsResponseFake.AllowRefund.Should().Be(allorRefundRes);
        }
    }
}
