using InvoiceManager.Services.Abstractions;
using InvoiceManager.Services.Contracts.ExchangeRate;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using Toolkit.Services;

namespace InvoiceManager.Services
{
    public class ExchangeRateService : IExchangeRateService
    {
        private string _urlString;
        public ExchangeRateService(string url, string apiKey)
        {
            _urlString = $"{url}/{apiKey}/latest/";
        }

        public OperationResult<ExchangeRateResult> ImportRates(string currency)
        {
            var result = new OperationResult<ExchangeRateResult>();
            try
            {
                _urlString += currency;
                using (var webClient = new WebClient())
                {
                    var json = webClient.DownloadString(_urlString);
                    var test = JsonConvert.DeserializeObject<ExchangeRate>(json);
                    if (test.result.ToLowerInvariant() == "error")
                    {
                        result.Errors.Add("WrongCurrency", $"The currency: '{currency}' doesn't exist");
                        return result;
                    }

                    result.Entity = ConvertToExchangeRateResult(test);
                }
            }
            catch (Exception)
            {
                result.Errors.Add("WrongCurrency", $"The currency: '{currency}' doesn't exist");
            }
            return result;
        }

        private ExchangeRateResult ConvertToExchangeRateResult(ExchangeRate excRate)
        {
            var result = new ExchangeRateResult
            {
                Documentation = excRate.documentation,
                Result = excRate.result,
                TermsOfUse = excRate.terms_of_use,
                TimeLastUpdate = excRate.time_last_update,
                TimeNextUpdate = excRate.time_next_update,
                TimeZone = excRate.result,
                ConversionRates = new Dictionary<string, double>()
            };

            Type myClassType = excRate.conversion_rates.GetType();
            PropertyInfo[] properties = myClassType.GetProperties();

            foreach (PropertyInfo property in properties)
            {
                result.ConversionRates.Add(property.Name, (double)property.GetValue(excRate.conversion_rates, null));
            }
            return result;
        }
    }
}