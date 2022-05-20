using System;
namespace TaxHelper.Services
{
    public class MalformedRestResponseException : Exception
    {
        public MalformedRestResponseException(string message)
            : base(message)
        {
        }

        public MalformedRestResponseException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
