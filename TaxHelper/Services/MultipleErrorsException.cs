using System;
using System.Collections.Generic;
using System.Linq;

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
