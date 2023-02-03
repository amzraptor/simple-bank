namespace Banks.Library.Public.Exceptions
{
    public class UnknownAccountTypeException : Exception
    {
        public UnknownAccountTypeException()
        {
        }

        public UnknownAccountTypeException(string message)
            : base(message)
        {
        }

        public UnknownAccountTypeException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

}