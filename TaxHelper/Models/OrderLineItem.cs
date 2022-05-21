using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TaxHelper.Models
{
    public class OrderLineItem : IBaseModel
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

        [JsonIgnoreAttribute]
        public string Description => ToString();

        public override string ToString()
        {
            float? total = Quantity * UnitPrice - Discount;
            return $"{Id}:  {Quantity}@{UnitPrice}ea = {Quantity * UnitPrice}  Disc: {Discount}  Total: {total.Value.ToString("C")}";
        }

        public override bool Equals(object obj)
        {
            return typeof(OrderLineItem).Equals(obj.GetType()) && ((OrderLineItem)obj).Id.Equals(Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public void Update(OrderLineItem editedLineItem)
        {
            if(!Id.Equals(editedLineItem.Id))
            {
                throw new Exception("Id mismatch detected");
            }
            Quantity = editedLineItem.Quantity;
            ProductTaxCode = editedLineItem.ProductTaxCode;
            UnitPrice = editedLineItem.UnitPrice;
            Discount = editedLineItem.Discount;
        }

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
