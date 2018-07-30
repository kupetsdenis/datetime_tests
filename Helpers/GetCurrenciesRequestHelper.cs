using datetime_tests.Models;
using datetime_tests.Settings.Properties;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;

namespace datetime_tests.Helpers
{
    public class CurrencyRequestHelper:RequestHelper
    {
        private readonly string CURRENCY_URL =  Resources.CurrenciesAPIUrl;

        public List<CurrencyModel> GetCurrecies()
        {
            var response = GetCurrencyResponse("");
            var jSonString = GetHtmlFromResponce(response);
                return JsonConvert.DeserializeObject<List<CurrencyModel>>(jSonString);
        }

        public CurrencyModel GetCurrecy(string currencyId)
        {
            var response = GetCurrencyResponse(currencyId);
            var jSonString = GetHtmlFromResponce(response);
            return JsonConvert.DeserializeObject<CurrencyModel>(jSonString);
        }

        public WebResponse GetCurrencyResponse(string currencyId)
        {
            var requestUriString = $"{CURRENCY_URL}/{currencyId}";
            var webGetRequest = (HttpWebRequest)WebRequest.Create(requestUriString);
            webGetRequest.Method = "GET";
            webGetRequest.Timeout = System.Threading.Timeout.Infinite;
            webGetRequest.KeepAlive = false;
            webGetRequest.ProtocolVersion = HttpVersion.Version10;
            return webGetRequest.GetResponse();
        }

    }
}
