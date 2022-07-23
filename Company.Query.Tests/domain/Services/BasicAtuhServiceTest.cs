using AutoFixture;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Company.Query.Domain.Configurations;
using Company.Query.Domain.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Company.Query.Tests.domain.Services
{
    [TestFixture]
    public class BasicAtuhServiceTest
    {
        private Fixture _fixture;
        private BasicAuthService _basicAtuhService;
        private CompanyConfiguration _configuration;
        private static string _userName = "teste";
        private static string _password = "testepassword";

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _configuration = new CompanyConfiguration()
            {
                BasicAuthUser = _userName,
                BasicAuthSecret = _password,
                CompanyIspb = string.Empty,
                CompanyAccountType = string.Empty
            };
        }

        [Test]
        public void Constructor_ShouldBeSuccess()
        {
            //Action
            Action act = () => _basicAtuhService = new BasicAuthService(_configuration);

            //Assert
            act.Should().NotThrow();
        }

        [Test]
        public void CheckCredentials_ReturnTrue_WhenCredentialsAreValid()
        {
            // Arrange
            bool result = false;
            _basicAtuhService = new BasicAuthService(_configuration);

            // Action
            Action act = () => result = _basicAtuhService.CheckCredentials(_userName, _password);

            // Assert
            act.Should().NotThrow();
            result.Should().Be(true);
        }

        static IEnumerable InvalidCredentials
        {
            get
            {
                yield return new TestCaseData(_userName, string.Empty);
                yield return new TestCaseData(string.Empty, _password);
                yield return new TestCaseData(string.Empty, string.Empty);
            }
        }
        [Test]
        [TestCaseSource(nameof(InvalidCredentials))]
        public void CheckCredentials_ReturnTrue_WhenCredentialsAreInvalid(string username, string password)
        {
            // Arrange
            bool result = true;
            _basicAtuhService = new BasicAuthService(_configuration);

            // Action
            Action act = () => result = _basicAtuhService.CheckCredentials(username, password);

            // Assert
            act.Should().NotThrow();
            result.Should().Be(false);
        }
    }
}
