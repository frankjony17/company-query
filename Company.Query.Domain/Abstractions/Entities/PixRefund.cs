using System;
using MongoDB.Bson.Serialization.Attributes;

namespace Company.Query.Domain.Abstractions.Entities
{
    public class CompanyRefund
    {
        public CompanyRefund() { }

        [BsonElement(elementName: "ORIGINAL_VL")]
        [BsonIgnoreIfNull]
        public double OriginalValue { get; private set; }
        [BsonElement(elementName: "ORIGINAL_DT")]
        [BsonIgnoreIfNull]
        public DateTime OriginalDate { get; private set; }
        [BsonElement(elementName: "ORIGINAL_CM")]
        [BsonIgnoreIfNull]
        public string OriginalComentary { get; private set; }

    }
}
