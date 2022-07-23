using AutoFixture;
using AutoMapper;
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
using System.Threading.Tasks;

namespace Company.Query.Tests.api.Controllers
{
    public class DetailControllerTest
    {
        private DetailController _detailController;
        private Fixture _fixture;
        private Mock<IDetailService> _detailService;
        private Mock<IMapper> _mapper;
        private Mock<ILogger<DetailController>> _logger;
        private RequestContextHolder _requestContextHolder;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _logger = new Mock<ILogger<DetailController>>();
            _detailService = new Mock<IDetailService>();
            _mapper = new Mock<IMapper>();
            _requestContextHolder = new RequestContextHolder();
        }

        [Test]
        public void Constructor_ShouldBeSuccess()
        {
            // Arrange
            // Action
            Action act = () => _detailController = new DetailController(_detailService.Object, _logger.Object, _requestContextHolder);

            // Assert
            act.Should().NotThrow();
        }

        [Test]
        public void Get_ReturnStatus200OK()
        {
            // Arrange
            IActionResult result = null;
            var paymentCompleteResponseFake = _fixture.Create<CompanyDetailResponse>();
            _detailService.Setup(x => x.Get(It.IsAny<string>())).ReturnsAsync(paymentCompleteResponseFake);
            _detailController = new DetailController(_detailService.Object, _logger.Object, _requestContextHolder);

            // Action
            Func<Task> act = async () => result = await _detailController.Get(string.Empty);

            // Assert
            act.Should().NotThrow();
            Assert.IsInstanceOf<OkObjectResult>(result);
        }
    }
}
