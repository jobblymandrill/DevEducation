using ElectronicsStore.Core;
using ElectronicsStore.Core.ConfigurationOptions;
using ElectronicsStore.Core.Countries;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace ElecronicsStore.Core
{
    public class CurrencyConverter : ICurrencyConverter
    {
        private string _urlOptions;
        public CurrencyConverter(IOptions<UrlOptions> urlOptions)
        {
            _urlOptions = urlOptions.Value.CurrenciesApiUrl;
        }
        public async ValueTask<decimal> GetCurrentExchangeRate(CurrencyTypeEnum type)
        {
            WebRequest request = WebRequest.CreateHttp(_urlOptions);
            WebResponse response = await request.GetResponseAsync();
            Stream dataStream;
            string result;
            decimal rate = 1;
            using (dataStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(dataStream);
                result = reader.ReadToEnd();
                JObject o = JObject.Parse(result);
                if (type == CurrencyTypeEnum.UkrainianCurrency) { rate = (decimal)o.SelectToken(Ukraine.Currency); }
                if (type == CurrencyTypeEnum.BelorussianCurrency) { rate = (decimal)o.SelectToken(Belorussia.Currency); }
                return rate;
            }
        }
    }
}
