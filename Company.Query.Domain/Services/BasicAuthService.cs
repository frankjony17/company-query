using Company.Query.Domain.Configurations;

namespace Company.Query.Domain.Services
{
    public class BasicAuthService : IBasicAuthService
    {
        private readonly CompanyConfiguration _configuration;

        public BasicAuthService(CompanyConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool CheckCredentials(string username, string password)
        {
            return username == _configuration.BasicAuthUser && password == _configuration.BasicAuthSecret;
        }
    }
}
