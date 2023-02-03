namespace Banks.Library.Public.Exceptions
{
    public class UnknownBankException : Exception
    {
        public UnknownBankException()
        {
        }

        public UnknownBankException(string message)
            : base(message)
        {
        }

        public UnknownBankException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

}