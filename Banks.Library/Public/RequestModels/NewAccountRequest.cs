using Banks.Library.Public.Enums;

namespace Banks.Library.Public.RequestModels
{
    public class NewAccountRequest 
    {
        private string _bankName;
        public string BankName { get => _bankName; set => _bankName = value.ToLower();  }
        public string HolderName {get; set;}
        public AccountType AccountType {get; set;}

    }
}