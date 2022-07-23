using Flunt.Validations;

namespace Company.Query.Domain.Infra.Contracts
{
    public static class ContractValidator
    {
        public static void Check(Contract contract)
        {
            if (contract.Invalid)
            {
                throw new InvalidContractException(contract);
            }
        }
    }
}
