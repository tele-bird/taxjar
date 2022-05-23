using System;
using System.Collections.Generic;

namespace TaxHelper.Common.Models
{
    public class TaxLocation : ValidatableModel
    {
        public string Country { get; set; }
        public string Zip { get; set; }
        public string State { get; set; }
        public string County { get; set; }
        public string City { get; set; }
        public string Street { get; set; }

        internal override IList<string> GetValidationErrors()
        {
            IList<string> errors = new List<string>();
            if (string.IsNullOrEmpty(this.Zip))
            {
                errors.Add("Zip is required");
            }
            return errors;
        }
    }
}
