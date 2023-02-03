namespace Banks.Library.Public.Exceptions
{
    public class DuplicateBankException : Exception
    {
        public DuplicateBankException()
        {
        }

        public DuplicateBankException(string message)
            : base(message)
        {
        }

        public DuplicateBankException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

}