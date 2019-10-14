using System;
using System.Collections.Generic;
using System.Linq;
using WebAPITemplate.Business.Enums;
using WebAPITemplate.Business.Interfaces;
using WebAPITemplate.Business.Models;

namespace WebAPITemplate.Business.Services
{
    public class CalculatorService : ICalculatorService
    {
        public decimal ComputeTotalProductsValueByCurrency(IEnumerable<Product> products, CurrencyCode currencyCode)
        {
            throw new NotImplementedException();
        }
    }
}
