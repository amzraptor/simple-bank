using Banks.Library.Public.Enums;
using Banks.Library.Public.Exceptions;

namespace Banks.Library.Internal.Accounts 
{
    internal static class AccountFactory
    {
        public static Account CreateAccount(AccountType accountType, string holderName) 
        {
            switch(accountType) 
            {
                case AccountType.Checking:
                    return new CheckingAccount(holderName);
                case AccountType.InvestmentCorporate:
                    return new InvestmentCorporateAccount(holderName);
                case AccountType.InvestmentIndividual:
                    return new InvestmentIndividualAccount(holderName);
                default:
                    throw new UnknownAccountTypeException($"{accountType.ToString()} is not supported.");
            }
        }
    }
}