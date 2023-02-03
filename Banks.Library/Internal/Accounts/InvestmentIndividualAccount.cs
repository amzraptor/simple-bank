using Banks.Library.Public.Exceptions;

namespace Banks.Library.Internal.Accounts 
{
    internal class InvestmentIndividualAccount : Account
    {
        public InvestmentIndividualAccount(string holderName) : base(holderName)
        {
        }

        internal override void Withdraw(decimal amount, DateTime dateTime)
        {
            var dailyWithdrawLimit = 500;
            var previousWithdrawnAmounts = _transactions.Where(t => t.TransactionType == Public.Enums.TransactionType.Subtract && t.DateTime.Date == dateTime.Date).Sum(t => t.Amount);
            
            if (previousWithdrawnAmounts + amount > Convert.ToDecimal(dailyWithdrawLimit)) 
            {
                var allowedLeft = dailyWithdrawLimit - previousWithdrawnAmounts;
                throw new WithdrawLimitReachedException($"Cannot withdraw {amount} from account {Id} as the daily withdraw limit of {dailyWithdrawLimit} would be reached. The account currently at {previousWithdrawnAmounts}");
            }
            
            base.Withdraw(amount, dateTime);        
        }
    }
}