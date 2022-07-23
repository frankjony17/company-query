namespace Company.Query.Domain.Services
{
    public interface IBasicAuthService
    {
        public bool CheckCredentials(string username, string password);
    }
}
