using Banks.Library.Public.Enums;

namespace Banks.Library.Public.RequestModels
{
    public abstract class TransactionRequest 
    {
        public Guid AccountId { get; set; }
        public decimal Amount { get; set; }

        public DateTime DateTime { get; set; }

        public TransactionRequest()
        {
            //for simplicity its just when it runs, this can be added as a parameter for transactions at specific date and times
            DateTime = DateTime.Now;            
        }
    }
}