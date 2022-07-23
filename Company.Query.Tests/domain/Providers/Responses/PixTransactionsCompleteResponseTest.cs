using AutoFixture;
using FluentAssertions;
using NUnit.Framework;
using Company.Query.Domain.Configurations;
using Company.Query.Domain.Providers.Responses;
using System;

namespace Company.Query.Tests.domain.Providers.Responses
{
    class CompanyTransactionsCompleteResponseTest
    {
        private Fixture _fixture;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
        }

        [Test]
        public void Create_ShouldBeSuccess_WhenCompanyTransactionsIsNotCreated()
        {
            // Arrange
            CompanyTransactionsCompleteResponse companyTransactionsCompleteResponseFake = null;
            var endToEndId = _fixture.Create<string>();
            var actions = _fixture.Create<Actions>();

            // Action
            Action act = () => companyTransactionsCompleteResponseFake = CompanyTransactionsCompleteResponse.Create(
                    endToEndId,
                    actions
                );

            // Assert
            act.Should().NotThrow();
            companyTransactionsCompleteResponseFake.Id.Should().Be(endToEndId);
            companyTransactionsCompleteResponseFake.Actions.Should().Be(actions);
            companyTransactionsCompleteResponseFake.CompanyTransactions.Should().BeNull();
        }

        [Test]
        public void CreateTransctions_ShouldBeSuccess()
        {
            // Arrange
            var endToEndId = _fixture.Create<string>();
            var actions = _fixture.Create<Actions>();
            var companyTransactionsCompleteResponseFake = CompanyTransactionsCompleteResponse.Create(
                    endToEndId,
                    actions
                );

            // Action
            Action act = () => companyTransactionsCompleteResponseFake.CreateTransctions();

            // Assert
            act.Should().NotThrow();
            companyTransactionsCompleteResponseFake.Id.Should().Be(endToEndId);
            companyTransactionsCompleteResponseFake.Actions.Should().Be(actions);
            companyTransactionsCompleteResponseFake.CompanyTransactions.Should().NotBeNull();
        }
    }
}
