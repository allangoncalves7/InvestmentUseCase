using System.ComponentModel;

namespace InvestmentUseCase.Domain.Enums
{
    public enum EInvestmentAction
    {
        [Description("Depósito")]
        Deposit,
        [Description("Resgate")]
        Withdraw
    }
}
