using Newtonsoft.Json;
namespace TaxHelper.Dto
{
    public class TaxBreakdownLineItemsDto
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "taxable_amount")]
        public string TaxableAmount { get; set; }

        [JsonProperty(PropertyName = "tax_collectable")]
        public string TaxCollectable { get; set; }

        [JsonProperty(PropertyName = "combined_tax_rate")]
        public string CombinedTaxRate { get; set; }

        [JsonProperty(PropertyName = "state_taxable_amount")]
        public string StateTaxableAmount { get; set; }

        [JsonProperty(PropertyName = "state_sales_tax_rate")]
        public string StateSalesTaxRate { get; set; }

        [JsonProperty(PropertyName = "state_amount")]
        public string StateAmount { get; set; }

        [JsonProperty(PropertyName = "county_taxable_amount")]
        public string CountyTaxableAmount { get; set; }

        [JsonProperty(PropertyName = "county_tax_rate")]
        public string CountyTaxRate { get; set; }

        [JsonProperty(PropertyName = "county_amount")]
        public string CountyAmount { get; set; }

        [JsonProperty(PropertyName = "city_taxable_amount")]
        public string CityTaxableAmount { get; set; }

        [JsonProperty(PropertyName = "city_tax_rate")]
        public string CityTaxRate { get; set; }

        [JsonProperty(PropertyName = "city_amount")]
        public string CityAmount { get; set; }

        [JsonProperty(PropertyName = "special_district_taxable_amount")]
        public string SpecialDistrictTaxableAmount { get; set; }

        [JsonProperty(PropertyName = "special_tax_rate")]
        public string SpecialTaxRate { get; set; }

        [JsonProperty(PropertyName = "special_district_amount")]
        public string SpecialDistrictAmount { get; set; }
    }
}
