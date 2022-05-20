using Newtonsoft.Json;

namespace TaxHelper.Dto
{
    public class TaxRateDto
    {
        #region location info
        [JsonProperty(PropertyName = "country")]
        public string Country { get; set; }

        [JsonProperty(PropertyName = "zip")]
        public string Zip { get; set; }

        [JsonProperty(PropertyName = "state")]
        public string State { get; set; }

        [JsonProperty(PropertyName = "county")]
        public string County { get; set; }

        [JsonProperty(PropertyName = "city")]
        public string City { get; set; }
        #endregion

        #region rate info
        [JsonProperty(PropertyName = "country_rate")]
        public float CountryRate { get; set; }

        [JsonProperty(PropertyName = "state_rate")]
        public float StateRate { get; set; }

        [JsonProperty(PropertyName = "county_rate")]
        public float CountyRate { get; set; }

        [JsonProperty(PropertyName = "city_rate")]
        public float CityRate { get; set; }

        [JsonProperty(PropertyName = "combined_district_rate")]
        public float CombinedDistrictRate { get; set; }

        [JsonProperty(PropertyName = "combined_rate")]
        public float CombinedRate { get; set; }

        [JsonProperty(PropertyName = "freight_taxable")]
        public bool IsFreightTaxable { get; set; }
        #endregion
    }
}