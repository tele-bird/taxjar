using System.Collections.Generic;
using Newtonsoft.Json;

namespace TaxHelper.Models
{
    public class Order : IBaseModel
    {
        [JsonProperty(PropertyName ="from_country")]
        public string FromCountry { get; set; }

        [JsonProperty(PropertyName = "from_zip")]
        public string FromZip { get; set; }

        [JsonProperty(PropertyName = "from_state")]
        public string FromState { get; set; }

        [JsonProperty(PropertyName = "from_city")]
        public string FromCity { get; set; }

        [JsonProperty(PropertyName = "from_street")]
        public string FromStreet { get; set; }

        [JsonProperty(PropertyName = "to_country")]
        public string ToCountry { get; set; }

        [JsonProperty(PropertyName = "to_zip")]
        public string ToZip { get; set; }

        [JsonProperty(PropertyName = "to_state")]
        public string ToState { get; set; }

        [JsonProperty(PropertyName = "to_city")]
        public string ToCity { get; set; }

        [JsonProperty(PropertyName = "to_street")]
        public string ToStreet { get; set; }

        [JsonProperty(PropertyName = "amount")]
        public float? Amount { get; set; }

        [JsonProperty(PropertyName = "shipping")]
        public float? Shipping { get; set; }

        [JsonProperty(PropertyName = "customer_id")]
        public string CustomerId { get; set; }

        [JsonProperty(PropertyName = "exemption_type")]
        public string ExemptionType { get; set; }

        [JsonProperty(PropertyName ="nexus_addresses")]
        public NexusAddress[] Addresses { get; set; }

        [JsonProperty(PropertyName ="line_items")]
        public OrderLineItem[] LineItems { get; set; }

        [JsonIgnore]
        public float TotalCost
        {
            get
            {
                float? total = 0;
                foreach (var lineItem in LineItems)
                {
                    total += lineItem.Quantity * lineItem.UnitPrice - lineItem.Discount;
                }
                return total.Value;
            }
        }

        public Order()
        {
            Addresses = new NexusAddress[0];
            LineItems = new OrderLineItem[0];
        }

        public IList<string> GetErrors()
        {
            IList<string> errors = new List<string>();
            if (string.IsNullOrEmpty(ToCountry))
            {
                errors.Add("Order: To Country is required");
            }
            if (!Shipping.HasValue)
            {
                errors.Add("Order: Shipping is required");
            }
            if(!Amount.HasValue && ((null == LineItems) || LineItems.Length < 1))
            {
                errors.Add("Order: Either Amount or Line Items are required");
            }
            if(!string.IsNullOrEmpty(ToCountry) && ToCountry.ToLower().Equals("us") && string.IsNullOrEmpty(ToZip))
            {
                errors.Add("Order: To Zip is required when To Country is US");
            }
            if(!string.IsNullOrEmpty(ToCountry) && (ToCountry.ToLower().Equals("us") || ToCountry.ToLower().Equals("ca")) && string.IsNullOrEmpty(ToState))
            {
                errors.Add("Order: To State is required when To Country is US or CA");
            }
            if(null != LineItems)
            {
                foreach(var lineItem in LineItems)
                {
                    var lineItemErrors = lineItem.GetErrors();
                    foreach(var lineItemError in lineItemErrors)
                    {
                        errors.Add(lineItemError);
                    }
                }
            }
            return errors;
        }
    }
}
