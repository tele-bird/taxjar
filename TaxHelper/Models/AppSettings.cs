using Newtonsoft.Json;
namespace TaxHelper.Models
{
    public class AppSettings
    {
        [JsonProperty(PropertyName ="tax_jar_api_key")]
        public string TaxJarApiKey { get; set; }

        public override bool Equals(object obj)
        {
            return typeof(AppSettings).Equals(obj.GetType()) && TaxJarApiKey.Equals(((AppSettings)obj).TaxJarApiKey);
        }

        public override int GetHashCode()
        {
            return TaxJarApiKey.GetHashCode();
        }
    }
}
