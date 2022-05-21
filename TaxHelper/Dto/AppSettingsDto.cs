using Newtonsoft.Json;
namespace TaxHelper.Dto
{
    public class AppSettingsDto
    {
        [JsonProperty(PropertyName ="tax_jar_api_key")]
        public string TaxJarApiKey { get; set; }
    }
}
