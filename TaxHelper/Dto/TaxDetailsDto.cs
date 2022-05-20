using Newtonsoft.Json;

namespace TaxHelper.Dto
{
    public class TaxDetailsDto
    {
        [JsonProperty(PropertyName = "order_total_amount")]
        public float OrderTotalAmount { get; set; }

        [JsonProperty(PropertyName = "shipping")]
        public float Shipping { get; set; }

        [JsonProperty(PropertyName = "taxable_amount")]
        public float TaxableAmount { get; set; }

        [JsonProperty(PropertyName = "amount_to_collect")]
        public float AmountToCollect { get; set; }

        [JsonProperty(PropertyName = "rate")]
        public float Rate { get; set; }

        [JsonProperty(PropertyName = "has_nexus")]
        public bool HasNexus { get; set; }

        [JsonProperty(PropertyName = "freight_taxable")]
        public bool FreightTaxable { get; set; }

        [JsonProperty(PropertyName = "tax_source")]
        public string TaxSource { get; set; }

        [JsonProperty(PropertyName = "jurisdictions")]
        public TaxJurisdictionsDto Jurisdictions { get; set; }

        [JsonProperty(PropertyName = "breakdown")]
        public TaxBreakdownDto Breakdown { get; set; }
    }
}
