using WebAPITemplate.Business.Enums;
using WebAPITemplate.Business.Models;
using System.Collections.Generic;

namespace WebAPITemplate.Business.Interfaces
{
    public interface ICalculatorService
    {
        decimal ComputeTotalProductsValueByCurrency(IEnumerable<Product> products, CurrencyCode currencyCode);
    }
}
