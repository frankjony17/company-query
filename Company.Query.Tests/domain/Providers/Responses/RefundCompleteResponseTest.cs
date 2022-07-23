using AutoFixture;
using FluentAssertions;
using NUnit.Framework;
using Company.Query.Domain.Configurations;
using Company.Query.Domain.Providers.Responses;
using System;

namespace Company.Query.Tests.domain.Providers.Responses
{
    public class RefundCompleteResponseTest
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
            RefundCompleteResponse refundCompleteResponseFake = null;
            var endToEndIdFake = _fixture.Create<string>();
            var refundStatusResponseFake = _fixture.Create<RefundStatusResponse>();
            var actionsFake = _fixture.Create<Actions>();

            // Action
            Action act = () => refundCompleteResponseFake = new RefundCompleteResponse(endToEndIdFake, refundStatusResponseFake, actionsFake);

            // Assert
            act.Should().NotThrow();
            refundCompleteResponseFake.Id.Should().Be(endToEndIdFake);
            refundCompleteResponseFake.Refund.Should().Be(refundStatusResponseFake);
            refundCompleteResponseFake.Actions.Should().Be(actionsFake);
        }
    }
}
