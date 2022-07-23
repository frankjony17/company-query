using Company.Query.Domain.Providers.Responses;
using System.Threading.Tasks;

namespace Company.Query.Domain.Services
{
    public interface IDetailService
    {
        Task<CompanyDetailResponse> Get(string endToEndId);
    }
}
