using Banks.Library.Internal;
using Banks.Library.Public.Exceptions;
using Banks.Library.Public.Enums;
using Banks.Library.Public.RequestModels;
using static Banks.Library.Internal.Accounts.AccountFactory;
using Banks.Library.Internal.Extensions;

namespace Banks.Library.Public
{
    public class BankServiceManager
    {
        private Dictionary<string ,Bank> _banks = new Dictionary<string, Bank>();

        public void AddBank(string name)
        {
            try
            { 
                _banks.Add(name.ToLower(),new Bank(name));
            }
            catch(ArgumentException ar) 
            {
                throw new DuplicateBankException($"Cannot add {name} because one already with different or similar casing. Check inner exception for details", ar);         
            }
        }

        public List<string> ListBanks()
        {
            return _banks.Values.Select(b => b.Name).ToList();
        }

        public void AddAccount(NewAccountRequest newAccountRequest) 
        {
            try
            {
                var selectedBank = _banks[newAccountRequest.BankName];

                var newAccount = CreateAccount(newAccountRequest.AccountType, newAccountRequest.HolderName);

                selectedBank.AddAccount(newAccount);
            }
            catch(KeyNotFoundException kne) 
            {
                throw new UnknownBankException($"Cannot add account because the bank does not exist. Check inner exception for details", kne);
            }
        }

        public List<AccountInfoResult> ListAccounts() 
        {
            return _banks.SelectMany(b => b.Value.GetAccounts().Select(a => Tuple.Create(b.Value.Name, a)))
                            .Select(ba => ba.Item2.ToAccountResult(ba.Item1))
                            .ToList();
        }

        public void Deposit(DepositRequest depositRequest) 
        {
            foreach(var bank in _banks.Values) 
            {
                //only 1 will match anyway
                bank.Deposit(depositRequest);
            }
        }

        public void Withdraw(WithdrawRequest withdrawRequest) 
        {
            foreach(var bank in _banks.Values) 
            {
                //only 1 will match anyway
                bank.Withdraw(withdrawRequest);
            }
        }

        public void Transfer(TransferRequest transferRequest) 
        {
            //a transfer request is treated like a withdraw then deposit
            foreach(var bank in _banks.Values) 
            {
                //only 1 will match anyway
                bank.Withdraw(transferRequest.ToWithdrawRequest());
            }
            foreach(var bank in _banks.Values) 
            {
                //only 1 will match anyway
                bank.Deposit(transferRequest.ToDepositRequest());
            }
        }
    }
}
