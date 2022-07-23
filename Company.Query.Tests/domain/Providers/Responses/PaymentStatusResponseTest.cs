using System;
using System.Collections.Generic;
using System.Text;
using AutoFixture;
using FluentAssertions;
using NUnit.Framework;
using Company.Query.Domain.Providers.Responses;

namespace Company.Query.Tests.domain.Providers.Responses
{
    public class PaymentStatusResponseTest
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
            var paymentStatusResponseFake = _fixture.Create<PaymentStatusResponse>();
            var legacyTransactionIdFake = _fixture.Create<int>();

            // Action
            Action act = () => paymentStatusResponseFake.SetLegacyTransactionId(legacyTransactionIdFake);

            // Assert
            act.Should().NotThrow();
            paymentStatusResponseFake.LegacyTransactionId.Should().Be(legacyTransactionIdFake);
        }
    }
}
