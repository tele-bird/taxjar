using Newtonsoft.Json;

namespace TaxHelper.Dto
{
    public class TaxRatesResultDto
    {
        [JsonProperty(PropertyName ="rate")]
        public TaxRateDto Rate {get; set;}
    }
}
