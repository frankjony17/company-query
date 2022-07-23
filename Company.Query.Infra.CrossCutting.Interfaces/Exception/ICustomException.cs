
namespace Company.Query.Infra.CrossCutting.Interfaces.Exception
{
    public interface ICustomException
    {
        string Title { get; }
        string Message { get; }
        int StatusCode { get; }
    }
}
