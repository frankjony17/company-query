using Company.Query.Domain.Providers.Acls;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Company.Query.Domain.Acls.DailyEntry
{
    public interface IDailyEntryAcl
    {
        Task<IEnumerable<DailyEntryResponseDto>> GetDailyEntryByTransactions(IEnumerable<Guid> transactionsUuid);
        Task<DailyEntryResponseDto> GetLegacyIdByTransactionUuid(Guid transactionUuid);
    }
}