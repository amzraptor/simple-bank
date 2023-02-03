using Banks.Library.Internal.Accounts;
using Banks.Library.Public.Enums;
using Banks.Library.Public.Exceptions;
using Banks.Library.Public.RequestModels;

namespace Banks.Library.Internal.Extensions
{
    internal static class Mappers
    {
        internal static AccountInfoResult ToAccountResult(this Account account, string bankName) 
        {
            return new AccountInfoResult 
            {
                BankName = bankName,
                AccountId = account.Id,
                Balance = account.Balance,
                HolderName = account.HolderName,
                Transactions = account.GetTransactions(),
                AccountType = account.DetermineAccountType()
            };
        }
        
        private static AccountType DetermineAccountType(this Account account) 
        {
            if (account is CheckingAccount)
                return AccountType.Checking;
            if (account is InvestmentCorporateAccount)
                return AccountType.InvestmentCorporate;
            if (account is InvestmentIndividualAccount)
                return AccountType.InvestmentIndividual;
            
            throw new UnknownAccountTypeException();
        }

        internal static WithdrawRequest ToWithdrawRequest(this TransferRequest transferRequest)
        {
            return new WithdrawRequest
            {
                AccountId = transferRequest.AccountId,
                Amount = transferRequest.Amount,
                DateTime = transferRequest.DateTime
            };
        }

         internal static DepositRequest ToDepositRequest(this TransferRequest transferRequest)
        {
            return new DepositRequest
            {
                AccountId = transferRequest.TargetAccountId,
                Amount = transferRequest.Amount,
                DateTime = transferRequest.DateTime
            };
        }
    }
}