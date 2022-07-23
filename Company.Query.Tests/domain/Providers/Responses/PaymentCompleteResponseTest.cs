using AutoFixture;
using FluentAssertions;
using NUnit.Framework;
using Company.Query.Domain.Configurations;
using Company.Query.Domain.Providers.Responses;
using System;

namespace Company.Query.Tests.domain.Providers.Responses
{
    public class PaymentCompleteResponseTest
    {
        private Fixture _fixture;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
        }

        [Test]
        public void Constructor_ShouldBeSuccess()
        {
            // Arrange
            PaymentCompleteResponse paymentCompleteResponseFake = null;
            var endToEndIdFake = _fixture.Create<string>();
            var paymentStatusResponseFake = _fixture.Create<PaymentStatusResponse>();
            var actionsFake = _fixture.Create<Actions>();

            // Action
            Action act = () => paymentCompleteResponseFake = new PaymentCompleteResponse(endToEndIdFake, paymentStatusResponseFake, actionsFake);

            // Assert
            act.Should().NotThrow();
            paymentCompleteResponseFake.Id.Should().Be(endToEndIdFake);
            paymentCompleteResponseFake.Payment.Should().Be(paymentStatusResponseFake);
            paymentCompleteResponseFake.Actions.Should().Be(actionsFake);
        }

    }
}
