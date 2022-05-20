using System;
using System.Collections.Generic;

namespace TaxHelper.Models
{
    public class TaxLocation
    {
        public string Country { get; set; }
        public string Zip { get; set; }
        public string State { get; set; }
        public string County { get; set; }
        public string City { get; set; }
        public string Street { get; set; }

        public IList<string> GetErrors()
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
