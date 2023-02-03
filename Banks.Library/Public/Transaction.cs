using Banks.Library.Public.Enums;

namespace Banks.Library.Public 
{
    public class Transaction
    {
        public TransactionType TransactionType { get; set; }
        public decimal Amount { get; set; }
        public DateTime DateTime { get; set;}
    }    
}