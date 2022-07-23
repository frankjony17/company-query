using AutoFixture;
using FluentAssertions;
using NUnit.Framework;
using Company.Query.Domain.Enums;
using Company.Query.Domain.Providers.Responses;
using System;

namespace Company.Query.Tests.domain.Providers.Responses
{
    public class CompanyTransactionsAssociatedListResponseTest
    {
        private Fixture _fixture;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
        }

        [Test]
        public void Constructor_ShouldBeSuccess_WhenOperationTypeIsDebit()
        {
            // Arrange
            CompanyTransactionsAssociatedListResponse companyTransactionsAssociatedListResponseFake = null;
            var transactionId = _fixture.Create<int>();
            var transactionUuid = Guid.NewGuid();
            var value = 100;
            var description = _fixture.Create<string>();
            var transactionDate = DateTime.Now;
            var categoryId = _fixture.Create<int>();
            var typeOperation = TipoOperacao.Debito;

            // Action
            Action act = () => companyTransactionsAssociatedListResponseFake = CompanyTransactionsAssociatedListResponse.Create(
                    transactionId,
                    transactionUuid,
                    value,
                    description,
                    transactionDate,
                    categoryId,
                    typeOperation
                ); 

            // Assert
            act.Should().NotThrow();
            companyTransactionsAssociatedListResponseFake.Value.Should().BeLessThan(0);
        }

        [Test]
        public void Constructor_ShouldBeSuccess_WhenOperationTypeIsCredit()
        {
            // Arrange
            CompanyTransactionsAssociatedListResponse companyTransactionsAssociatedListResponseFake = null;
            var transactionId = _fixture.Create<int>();
            var transactionUuid = Guid.NewGuid();
            var value = 100;
            var description = _fixture.Create<string>();
            var transactionDate = DateTime.Now;
            var categoryId = _fixture.Create<int>();
            var typeOperation = TipoOperacao.Credito;

            // Action
            Action act = () => companyTransactionsAssociatedListResponseFake = CompanyTransactionsAssociatedListResponse.Create(
                    transactionId,
                    transactionUuid,
                    value,
                    description,
                    transactionDate,
                    categoryId,
                    typeOperation
                );

            // Assert
            act.Should().NotThrow();
            companyTransactionsAssociatedListResponseFake.Value.Should().BeGreaterThan(0);
        }

        [Test]
        public void SetIsSource_ReturnIsSourceTrue()
        {
            // Arrange
            var transactionId = _fixture.Create<int>();
            var transactionUuid = Guid.NewGuid();
            var value = 100;
            var description = _fixture.Create<string>();
            var transactionDate = DateTime.Now;
            var categoryId = _fixture.Create<int>();
            var typeOperation = TipoOperacao.Debito;
            var companyTransactionsAssociatedListResponseFake = CompanyTransactionsAssociatedListResponse.Create(
                    transactionId,
                    transactionUuid,
                    value,
                    description,
                    transactionDate,
                    categoryId,
                    typeOperation
                );

            // Action
            Action act = () => companyTransactionsAssociatedListResponseFake.SetIsSource(); 

            // Assert
            act.Should().NotThrow();
            companyTransactionsAssociatedListResponseFake.IsSource.Should().BeTrue();
        }
    }
}
