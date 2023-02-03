using Banks.Library.Internal;
using Banks.Library.Public.Enums;

namespace Banks.Library.Public.RequestModels
{
    public class AccountInfoResult
    {
        public Guid AccountId {get; set;}
        public string HolderName { get; set; }
        public string BankName { get; set; }
        public AccountType AccountType { get; set; }
        public List<Transaction> Transactions { get; set; } 
        public decimal Balance { get; set; }
    }
}