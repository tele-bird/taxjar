using System;
using System.Collections.Generic;
using System.Linq;

namespace TaxHelper.Common.Exceptions
{
    public class ModelValidationException : Exception
    {
        public IEnumerable<string> ValidationErrors { get; set; }

        public override string Message
        {
            get
            {
                return string.Join(Environment.NewLine, ValidationErrors);
            }
        }

        public ModelValidationException(IEnumerable<string> validationErrors)
        {
            ValidationErrors = validationErrors;
        }

        public override bool Equals(object obj)
        {
            if (!obj.GetType().Equals(typeof(ModelValidationException))) return false;
            ModelValidationException other = (ModelValidationException)obj;
            return ValidationErrors.SequenceEqual(other.ValidationErrors);
        }

        public override int GetHashCode()
        {
            return ValidationErrors.Sum(error => error.GetHashCode());
        }
    }
}
