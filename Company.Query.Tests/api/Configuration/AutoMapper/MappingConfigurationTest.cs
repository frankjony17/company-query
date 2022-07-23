using AutoMapper;
using FluentAssertions;
using NUnit.Framework;
using Company.Query.Api.Configuration.AutoMapper;
using System;

namespace Company.Query.Tests.api.Configuration.AutoMapper
{
    public class MappingConfigurationTest
    {
        [Test]
        public void MappingConfigurationRegister_ShouldBeSuccess()
        {
            // Arrange
            MapperConfiguration mapperConfiguration = null;

            // Action
            Action act = () => mapperConfiguration = MappingConfiguration.Register();

            // Assert
            act.Should().NotThrow();
            mapperConfiguration.Should().NotBeNull();
        }
    }
}
