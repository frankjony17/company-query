using System;

namespace Company.Query.Domain.Providers.Acls
{
    public class CheckingAccountResponseDto
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public CheckingAccountDto CheckingAccount { get; set; }
    }
}