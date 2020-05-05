using System.Collections.Generic;

namespace InvoiceManager.Services.Contracts.ExchangeRate
{
    public class ExchangeRateResult
    {
        public string Result { get; set; }
        public string Documentation { get; set; }
        public string TermsOfUse { get; set; }
        public string TimeZone { get; set; }
        public string TimeLastUpdate { get; set; }
        public string TimeNextUpdate { get; set; }
        public Dictionary<string, double> ConversionRates { get; set; }
    }
}
