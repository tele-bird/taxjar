using Newtonsoft.Json;

namespace TaxHelper.Dto
{
    public class TaxResultDto
    {
        [JsonProperty(PropertyName = "tax")]
        public TaxDetailsDto Tax { get; set; }
    }
}
