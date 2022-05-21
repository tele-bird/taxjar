using System;
using System.Collections.Generic;

namespace TaxHelper.Services
{
    public class MultipleErrorsException : Exception
    {
        public MultipleErrorsException(IEnumerable<string> errors)
            : base(string.Join(Environment.NewLine, errors))
        {
        }
    }
}
