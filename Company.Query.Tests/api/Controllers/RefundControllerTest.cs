using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Company.Query.Api.Controllers;
using Company.Query.Domain.Abstractions;
using Company.Query.Domain.Providers.Responses;
using Company.Query.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Company.Query.Tests.api.Controllers
{
    public class RefundControllerTest
    {
        private RefundController _refundController;
        private Fixture _fixture;
        private Mock<IRefundService> _refundService;
        private Mock<ILogger<PaymentController>> _logger;
        private RequestContextHolder _requestContextHolder;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _logger = new Mock<ILogger<PaymentController>>();
            _refundService = new Mock<IRefundService>();
            _requestContextHolder = new RequestContextHolder();
        }

        [Test]
        public void Constructor_ShouldBeSuccess()
        {
            // Arrange
            // Action
            Action act = () => _refundController = new RefundController(_refundService.Object, _requestContextHolder);

            // Assert
            act.Should().NotThrow();
        }

        [Test]
        public void Get_ReturnStatus200OK()
        {
            // Arrange
            IActionResult result = null;
            var paymentCompleteResponseFake = _fixture.Create<RefundCompleteResponse>();
            _refundService.Setup(x => x.GetRefundStatus(It.IsAny<string>())).ReturnsAsync(paymentCompleteResponseFake);
            _refundController = new RefundController(_refundService.Object, _requestContextHolder);

            // Action
            Func<Task> act = async () => result = await _refundController.Get(string.Empty);

            // Assert
            act.Should().NotThrow();
            Assert.IsInstanceOf<OkObjectResult>(result);
        }
    }
}
