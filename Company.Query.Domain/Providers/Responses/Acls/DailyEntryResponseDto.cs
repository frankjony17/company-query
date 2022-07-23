using Newtonsoft.Json;
using Company.Query.Domain.Enums;
using System;

namespace Company.Query.Domain.Providers.Acls
{
    public class DailyEntryResponseDto
    {

        [JsonProperty(PropertyName = "TransactionUuid")]
        public Guid TransactionUuid { get; set; }

        [JsonProperty(PropertyName = "Id")]
        public int TransactionId { get; set; }

        [JsonProperty(PropertyName = "IdCategoria")]
        public int CategoryId { get; set; }

        [JsonProperty(PropertyName = "Valor")]
        public decimal Value { get; set; }

        [JsonProperty(PropertyName = "DataTransacao")]
        public DateTime TransactionDate { get; set; }

        [JsonProperty(PropertyName = "Descricao")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "TipoOperacao")]
        public TipoOperacao TypeOperation { get; set; }
    }
}