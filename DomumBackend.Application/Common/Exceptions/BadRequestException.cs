namespace DomumBackend.Application.Common.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException() : base()
        {

        }

        public BadRequestException(string message) : base(message)
        {

        }

        public BadRequestException(string message, Exception exp) : base(message, exp)
        {

        }

        public int FailureCode { get; }

        public BadRequestException(string message, int failureCode = 400)
            : base(message)
        {
            FailureCode = failureCode;
        }
    }
}

