using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using TaxHelper.Dto;
using TaxHelper.Models;

namespace TaxHelper.Services
{
    public class TaxJarTaxCalculator : ITaxCalculator
    {
        private static string GET_TAX_RATES_FOR_LOCATION_URL = "https://api.taxjar.com/v2/rates/";
        private static string GET_TAXES_FOR_ORDER = "https://api.taxjar.com/v2/taxes";

        private readonly string mTaxJarApiKey;

        public TaxJarTaxCalculator(string taxJarApiKey)
        {
            if (taxJarApiKey == null)
            {
                throw new Exception("API key is required");
            }
            mTaxJarApiKey = taxJarApiKey;
        }

        private AuthenticationHeaderValue ConstructAuthorizationHeader()
        {
            return new AuthenticationHeaderValue("Bearer", mTaxJarApiKey);
        }

        private async Task<Exception> ConstructTaxJarException(HttpResponseMessage response)
        {
            Exception exc = null;
            try
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var taxJarError = JsonConvert.DeserializeObject<TaxJarErrorResponseDto>(responseBody);
                exc = new Exception($"Endpoint returned error code {taxJarError.Status}{Environment.NewLine}{taxJarError.Error}{Environment.NewLine}{taxJarError.Detail}");
            }
            catch (Exception)
            {
                exc = new Exception($"Endpoint returned error code {response.StatusCode}");
            }
            return exc;
        }

        #region get tax rates for location
        public async Task<string> GetTaxRatesForLocation(TaxLocation taxLocation)
        {
            Uri restUrl = this.ConstructGetTaxRatesForLocationUrl(taxLocation);
            TaxRatesResultDto resultsDto = null;
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = this.ConstructAuthorizationHeader();
                HttpResponseMessage response = await httpClient.GetAsync(restUrl);
                if (!response.IsSuccessStatusCode)
                {
                    throw await this.ConstructTaxJarException(response);
                }
                string responseBody = await response.Content.ReadAsStringAsync();
                try
                {
                    resultsDto = JsonConvert.DeserializeObject<TaxRatesResultDto>(responseBody);
                }
                catch (JsonSerializationException exc)
                {
                    throw new MalformedRestResponseException($"Received a malformed REST response from {restUrl}", exc);
                }
            }
            return JsonConvert.SerializeObject(resultsDto, Formatting.Indented);
        }

        private Uri ConstructGetTaxRatesForLocationUrl(TaxLocation taxLocation)
        {
            StringBuilder sbUrl = new StringBuilder($"{GET_TAX_RATES_FOR_LOCATION_URL}{taxLocation.Zip}");
            bool parametersExist = false;
            if (taxLocation.Country != null)
            {
                sbUrl.Append(parametersExist ? "&" : "?");
                parametersExist = true;
                sbUrl.Append($"country={HttpUtility.UrlEncode(taxLocation.Country)}");
            }
            if (taxLocation.State != null)
            {
                sbUrl.Append(parametersExist ? "&" : "?");
                parametersExist = true;
                sbUrl.Append($"state={HttpUtility.UrlEncode(taxLocation.State)}");
            }
            if (taxLocation.City != null)
            {
                sbUrl.Append(parametersExist ? "&" : "?");
                parametersExist = true;
                sbUrl.Append($"city={HttpUtility.UrlEncode(taxLocation.City)}");
            }
            if (taxLocation.Street != null)
            {
                sbUrl.Append(parametersExist ? "&" : "?");
                parametersExist = true;
                sbUrl.Append($"street={HttpUtility.UrlEncode(taxLocation.Street)}");
            }
            return new Uri(sbUrl.ToString());
        }
        #endregion

        #region get taxes for order
        public async Task<string> GetTaxesForOrder(Order order)
        {
            Uri restUrl = new Uri(GET_TAXES_FOR_ORDER);
            TaxResultDto taxResultDto = null;
            using(var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = this.ConstructAuthorizationHeader();
                var jsonBody = JsonConvert.SerializeObject(order, Formatting.None);
                var httpContent = new StringContent(jsonBody, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await httpClient.PostAsync(restUrl, httpContent);
                if (!response.IsSuccessStatusCode)
                {
                    throw await this.ConstructTaxJarException(response);
                }
                string responseBody = await response.Content.ReadAsStringAsync();
                try
                {
                    taxResultDto = JsonConvert.DeserializeObject<TaxResultDto>(responseBody);
                }
                catch (JsonSerializationException exc)
                {
                    throw new MalformedRestResponseException($"Received a malformed REST response from {restUrl}", exc);
                }
                return JsonConvert.SerializeObject(taxResultDto, Formatting.Indented);
            }
        }
        #endregion
    }
}
