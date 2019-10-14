using WebAPITemplate.Business.Enums;
using WebAPITemplate.Business.Models;
using System.Collections.Generic;

namespace WebAPITemplate.Business.Test
{
    public static class GetTestData
    {
        public static IEnumerable<ConversionRate> GetTestRates()
        {
            List<ConversionRate> rates = new List<ConversionRate>();

            rates.Add(new ConversionRate()
            {
                From = CurrencyCode.EUR.Value,
                To = CurrencyCode.USD.Value,
                Rate = 1.359M
            });

            rates.Add(new ConversionRate()
            {
                From = CurrencyCode.CAD.Value,
                To = CurrencyCode.EUR.Value,
                Rate = 0.732M
            });
            rates.Add(new ConversionRate()
            {
                From = CurrencyCode.USD.Value,
                To = CurrencyCode.EUR.Value,
                Rate = 0.736M
            });
            rates.Add(new ConversionRate()
            {
                From = CurrencyCode.EUR.Value,
                To = CurrencyCode.CAD.Value,
                Rate = 1.366M
            });

            return rates;
        }

        public static IEnumerable<BusinessTransaction> GetTestTransactions()
        {
            List<BusinessTransaction> transactions = new List<BusinessTransaction>();

            transactions.Add(new BusinessTransaction()
            {
                SKU = "T2006",
                Amount = 10.00M,
                Currency = CurrencyCode.USD.Value
            });

            transactions.Add(new BusinessTransaction()
            {
                SKU = "M2007",
                Amount = 34.57M,
                Currency = CurrencyCode.CAD.Value
            });
            transactions.Add(new BusinessTransaction()
            {
                SKU = "R2008",
                Amount = 17.95M,
                Currency = CurrencyCode.USD.Value
            });
            transactions.Add(new BusinessTransaction()
            {
                SKU = "T2006",
                Amount = 7.63M,
                Currency = CurrencyCode.EUR.Value
            });
            transactions.Add(new BusinessTransaction()
            {
                SKU = "B2009",
                Amount = 21.23M,
                Currency = CurrencyCode.USD.Value
            });

            return transactions;
        }
    }
}
