using System;

namespace TaxHelper.Services
{
    public class MissingApiKeyException : Exception
    {
        public override string Message { get; } = "API key is missing - try the hamburger menu";
    }
}
