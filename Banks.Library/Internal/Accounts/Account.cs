using Banks.Library.Public;
using Banks.Library.Public.Enums;

namespace Banks.Library.Internal.Accounts 
{

    internal abstract class Account
    {
        internal Guid Id { get; set; }
        internal string HolderName { get; set; }
        internal string BankName { get; set; }
        internal decimal Balance { get => CalculateFromTransactions();  }
        internal List<Transaction> _transactions = new List<Transaction>();

        internal Account(string holderName) 
        {
            Id = Guid.NewGuid();
            HolderName = holderName;
        }

        internal List<Transaction> GetTransactions() 
        {
            return _transactions.ToList();
        }

        internal virtual void Deposit(decimal amount, DateTime dateTime)
        {
            _transactions.Add(
                new Transaction
                {
                    Amount = amount,
                    DateTime = dateTime,
                    TransactionType = TransactionType.Add
                });
        }

        internal virtual void Withdraw(decimal amount, DateTime dateTime)
        {
            _transactions.Add(
                new Transaction
                {
                    Amount = amount,
                    DateTime = dateTime,
                    TransactionType = TransactionType.Subtract
                });
        }

        private decimal CalculateFromTransactions()
        {
            var balance = decimal.Zero;
            _transactions.ForEach(t => 
            {
                balance = t.TransactionType == TransactionType.Add ? balance + t.Amount : balance - t.Amount;
            });

            return balance;
        }

    }
}