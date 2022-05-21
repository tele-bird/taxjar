using System.Collections.Generic;
using Newtonsoft.Json;

namespace TaxHelper.Models
{
    public class NexusAddress
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "country")]
        public string Country { get; set; }

        [JsonProperty(PropertyName = "zip")]
        public string Zip { get; set; }

        [JsonProperty(PropertyName = "state")]
        public string State { get; set; }

        [JsonProperty(PropertyName = "city")]
        public string City { get; set; }

        [JsonProperty(PropertyName = "street")]
        public string Street { get; set; }

        public IList<string> GetErrors()
        {
            IList<string> errors = new List<string>();
            if (string.IsNullOrEmpty(Id))
            {
                errors.Add("Address: Id is required");
            }
            if (string.IsNullOrEmpty(Country))
            {
                errors.Add("Address: Country is required");
            }
            if (string.IsNullOrEmpty(Zip))
            {
                errors.Add("Address: Zip is required");
            }
            if (string.IsNullOrEmpty(State))
            {
                errors.Add("Address: State is required");
            }
            if (string.IsNullOrEmpty(City))
            {
                errors.Add("Address: City is required");
            }
            if (string.IsNullOrEmpty(Street))
            {
                errors.Add("Address: Street is required");
            }
            return errors;
        }
    }
}
