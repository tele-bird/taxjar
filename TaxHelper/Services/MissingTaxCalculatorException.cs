using System;

namespace TaxHelper.Services
{
    public class MissingTaxCalculatorException : Exception
    {
        public MissingTaxCalculatorException(string message)
            : base(message)
        {
        }
    }
}
