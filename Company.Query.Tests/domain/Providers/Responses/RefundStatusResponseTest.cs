using AutoFixture;
using FluentAssertions;
using NUnit.Framework;
using Company.Query.Domain.Providers.Responses;
using System;

namespace Company.Query.Tests.domain.Providers.Responses
{
    class RefundStatusResponseTest
    {
        private Fixture _fixture;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
        }

        [Test]
        public void SetLegacyTransactionId_ShouldBeSuccess()
        {
            // Arrange
            var refundStatusResponseFake = _fixture.Create<RefundStatusResponse>();
            var legacyTransactionIdFake = _fixture.Create<int>();

            // Action
            Action act = () => refundStatusResponseFake.SetLegacyTransactionId(legacyTransactionIdFake);

            // Assert
            act.Should().NotThrow();
            refundStatusResponseFake.LegacyTransactionId.Should().Be(legacyTransactionIdFake);
        }
    }
}
