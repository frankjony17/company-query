using CompanySource.Infra.Abstractions.Acls;
using Company.Query.Domain.Configurations;
using Company.Query.Domain.Exceptions;
using Company.Query.Domain.Providers.Acls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Company.Query.Domain.Acls.DailyEntry
{
    public class DailyEntryAcl : IDailyEntryAcl
    {
        private readonly DailyEntryConfiguration _dailyEntryConfig;
        private readonly IAclService _aclService;

        public DailyEntryAcl(IAclServiceFactory aclServieFactory, DailyEntryConfiguration dailyEntryConfig)
        {
            _aclService = aclServieFactory.Create(dailyEntryConfig.BaseUrl);
            _aclService.SetSerializerForJson();

            _dailyEntryConfig = dailyEntryConfig;
        }


        public async Task<IEnumerable<DailyEntryResponseDto>> GetDailyEntryByTransactions(IEnumerable<Guid> transactionsUuid)
        {
            return await _aclService.Get<IEnumerable<DailyEntryResponseDto>>(_dailyEntryConfig.GetLancamentoRelativeUrl, ToQueryParam(transactionsUuid));
        }

        public async Task<DailyEntryResponseDto> GetLegacyIdByTransactionUuid(Guid transactionUuid)
        {
            var response = await _aclService.Get<IEnumerable<DailyEntryResponseDto>>(_dailyEntryConfig.GetLancamentoRelativeUrl, ToQueryParam(transactionUuid));

            if(response == null)
            {
                throw new BadRequestCustomException("Legacy Id not found by Transaction Uuid");
            }

            return response.FirstOrDefault();
        }

        private Dictionary<string, string> ToQueryParam(IEnumerable<Guid> uuids)
        {
            return uuids
                    .Select((x, i) => new { Item = x, Index = i })
                        .ToDictionary(x => $"ls[{x.Index}]", x => x.Item.ToString());
        }

        private Dictionary<string, string> ToQueryParam(Guid transactionUuid)
        {
            return new Dictionary<string, string>
            {
                ["ls"] = transactionUuid.ToString()
            };
        }
    }
}