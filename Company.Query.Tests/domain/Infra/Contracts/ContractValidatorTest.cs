using FluentAssertions;
using Flunt.Validations;
using Moq;
using NUnit.Framework;
using Company.Query.Domain.Infra.Contracts;
using System;

namespace Company.Query.Tests.domain.Infra.Contracts
{
    public class ContractValidatorTest
    {
        private Contract contract;

        [SetUp]
        public void Setup()
        {
            contract = new Contract();
        }

        [Test]
        public void Check_ShouldBeThrowInvalidContractException_WhenContractIsInvalid()
        {
            // Arrange
            contract.AreEquals(0, 1, string.Empty, string.Empty);

            // Action
            Action act = () => ContractValidator.Check(contract);

            // Assert
            act.Should().Throw<InvalidContractException>();
        }

        [Test]
        public void Check_ShouldBeSuccess_WhenContractIsValid()
        {
            // Arrange
            contract.AreEquals(0, 0, string.Empty, string.Empty);

            // Action
            Action act = () => ContractValidator.Check(contract);

            // Assert
            act.Should().NotThrow();
        }
    }
}
