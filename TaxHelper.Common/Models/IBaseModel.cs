using System;
using System.Collections.Generic;
using TaxHelper.Common.Exceptions;

namespace TaxHelper.Common.Models
{
    public abstract class ValidatableModel
    {
        public void ThrowIfInvalid()
        {
            var validationErrors = GetValidationErrors();
            if(validationErrors.Count > 0)
            {
                throw new ModelValidationException(validationErrors);
            }
        }

        internal abstract IList<string> GetValidationErrors();
    }
}
