using AutoMapper;

namespace Company.Query.Api.Configuration.AutoMapper
{
    public static class MappingConfiguration
    {
        public static MapperConfiguration Register()
        {
            var configs = new MapperConfiguration(config =>
            {
                config.AddProfile(new DomainToApiMappingProfile());
            });

            configs.AssertConfigurationIsValid();

            return configs;
        }
    }
}
