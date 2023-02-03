using Banks.Library.Public.Enums;

namespace Banks.Library.Public.RequestModels
{
    public class TransferRequest : TransactionRequest
    {
        public Guid TargetAccountId { get; set; }
        public TransferRequest() : base()
        {
        }
    }
}