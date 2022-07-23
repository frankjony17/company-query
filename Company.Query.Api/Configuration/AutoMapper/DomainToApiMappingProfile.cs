using AutoMapper;
using Company.Query.Api.Responses;
using Company.Query.Domain.Abstractions.Entities;

namespace Company.Query.Api.Configuration.AutoMapper
{
    internal class DomainToApiMappingProfile : Profile
    {
        public DomainToApiMappingProfile()
        {
            CreateMap<PaymentReceived, PaymentReceivedResponse>();
            CreateMap<PaymentDone, PaymentDoneResponse>();
        }
    }
}
