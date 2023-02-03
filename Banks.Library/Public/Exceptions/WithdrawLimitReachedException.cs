namespace Banks.Library.Public.Exceptions
{
    public class WithdrawLimitReachedException : Exception
    {
        public WithdrawLimitReachedException()
        {
        }

        public WithdrawLimitReachedException(string message)
            : base(message)
        {
        }

        public WithdrawLimitReachedException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

}