using Banks.Library.Internal.Accounts;
using Banks.Library.Public.RequestModels;

namespace Banks.Library.Internal 
{

    internal class Bank 
    {
        internal string Name {get;}
        private Dictionary<Guid , Account> _accounts = new Dictionary<Guid, Account>();
        internal Bank(string name)
        {
            Name = name;
        }

        internal void AddAccount(Account account) 
        {
            var key = account.Id;
            _accounts[key] = account; 
                  
        }

        internal List<Account> GetAccounts()
        {
            return _accounts.Values.ToList();
        }

        internal void Deposit(DepositRequest depositRequest)
        {
            if (_accounts.ContainsKey(depositRequest.AccountId))
            {
                _accounts[depositRequest.AccountId].Deposit(depositRequest.Amount, depositRequest.DateTime);
            }
        }

        internal void Withdraw(WithdrawRequest withdrawRequest)
        {
            if (_accounts.ContainsKey(withdrawRequest.AccountId))
            {
                _accounts[withdrawRequest.AccountId].Withdraw(withdrawRequest.Amount, withdrawRequest.DateTime);
            }
        }
    }
}