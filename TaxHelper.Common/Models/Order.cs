using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TaxHelper.Common.Models
{
    public class Order : ValidatableModel
    {
        [JsonProperty(PropertyName = "from_country")]
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

        [JsonProperty(PropertyName = "nexus_addresses")]
        public NexusAddress[] Addresses { get; set; }

        [JsonProperty(PropertyName = "line_items")]
        public OrderLineItem[] LineItems { get; set; }

        [JsonIgnore]
        public float AmountFloat => Amount.HasValue ? Amount.Value : 0f;

        [JsonIgnore]
        public float ShippingFloat => Shipping.HasValue ? Shipping.Value : 0f;

        [JsonIgnore]
        public float LineItemsTotalFloat
        {
            get
            {
                float total = 0;
                foreach (var lineItem in LineItems)
                {
                    total += lineItem.TotalFloat;
                }
                return total;
            }
        }

        [JsonIgnore]
        public float GrandTotalFloat => LineItemsTotalFloat + ShippingFloat;

        public Order()
        {
            Addresses = new NexusAddress[0];
            LineItems = new OrderLineItem[0];
        }

        internal override IList<string> GetValidationErrors()
        {
            IList<string> errors = new List<string>();
            if (string.IsNullOrEmpty(ToCountry))
            {
                errors.Add(ModelValidationErrors.ORDER_TOCOUNTRY_REQUIRED);
            }
            if (!Shipping.HasValue)
            {
                errors.Add(ModelValidationErrors.ORDER_SHIPPING_REQUIRED);
            }
            if (!Amount.HasValue && ((null == LineItems) || LineItems.Length < 1))
            {
                errors.Add(ModelValidationErrors.ORDER_AMOUNT_OR_LINE_ITEMS_REQUIRED);
            }
            if (!string.IsNullOrEmpty(ToCountry) && ToCountry.ToLower().Equals("us") && string.IsNullOrEmpty(ToZip))
            {
                errors.Add(ModelValidationErrors.ORDER_TOZIP_REQUIRED_FOR_US);
            }
            if (!string.IsNullOrEmpty(ToCountry) && (ToCountry.ToLower().Equals("us") || ToCountry.ToLower().Equals("ca")) && string.IsNullOrEmpty(ToState))
            {
                errors.Add(ModelValidationErrors.ORDER_TOSTATE_REQUIRED_FOR_US_AND_CA);
            }
            if (null != LineItems)
            {
                foreach (var lineItem in LineItems)
                {
                    var lineItemErrors = lineItem.GetValidationErrors();
                    foreach (var lineItemError in lineItemErrors)
                    {
                        errors.Add(lineItemError);
                    }
                }
            }
            if (Amount.HasValue && !Amount.Value.Equals(LineItemsTotalFloat))
            {
                errors.Add(ModelValidationErrors.ORDER_AMOUNT_MISMATCH);
            }
            return errors;
        }
    }
}
