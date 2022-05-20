using System.Collections.Generic;
using Newtonsoft.Json;

namespace TaxHelper.Models
{
    public class OrderLineItem
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "quantity")]
        public int? Quantity { get; set; }

        [JsonProperty(PropertyName = "product_tax_code")]
        public string ProductTaxCode { get; set; }

        [JsonProperty(PropertyName = "unit_price")]
        public float? UnitPrice { get; set; }

        [JsonProperty(PropertyName = "discount")]
        public float? Discount { get; set; }

        public IList<string> GetErrors()
        {
            IList<string> errors = new List<string>();
            if (string.IsNullOrEmpty(Id))
            {
                errors.Add("Line item: Id is required");
            }
            if (!Quantity.HasValue)
            {
                errors.Add("Line item: Quantity is required");
            }
            if (string.IsNullOrEmpty(ProductTaxCode))
            {
                errors.Add("Line item: Product Tax Code is required");
            }
            if (!UnitPrice.HasValue)
            {
                errors.Add("Line item: Unit Price is required");
            }
            if (!Discount.HasValue)
            {
                errors.Add("Line item: Discount is required");
            }
            return errors;
        }
    }
}
