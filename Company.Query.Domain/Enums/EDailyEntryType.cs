using System.ComponentModel;

namespace Company.Query.Domain.Enums
{
    public enum EDailyEntryType
    {
        [Description("COMPANY In diferente titularidade")]
        COMPANYInDifferentOwnership = 65,
        [Description("COMPANY In mesma titularidade")]
        COMPANYInSameOwnership = 67,
    }
}
