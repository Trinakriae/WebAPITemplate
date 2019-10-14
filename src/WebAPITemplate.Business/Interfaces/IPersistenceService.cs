using WebAPITemplate.Business.Models;
using System;
using System.Collections.Generic;

namespace WebAPITemplate.Business.Interfaces
{
    public interface IPersistenceService
    {
        void SaveProducts(IEnumerable<Product> productss);
    }
}
