using WebAPITemplate.Business.Enums;
using WebAPITemplate.Business.Models;
using System.Collections.Generic;
using System;

namespace WebAPITemplate.Business.Test
{
    public static class GetTestData
    {
        public static IEnumerable<Product> GetTestProducts()
        {
            List<Product> products = new List<Product>();

            products.Add(new Product()
            {
                Id = "T2006",
                Name = "P1",
                Description = "Product1",
                StartDate = new DateTime(2019, 10, 1),
                EndDate = new DateTime(2020, 10, 1)
            }); ;

            products.Add(new Product()
            {
                Id = "T2007",
                Name = "P2",
                Description = "Product2",
                StartDate = new DateTime(2019, 10, 1),
                EndDate = new DateTime(2020, 10, 1)
            });
            products.Add(new Product()
            {
                Id = "B2005",
                Name = "P3",
                Description = "Product3",
                StartDate = new DateTime(2019, 10, 1),
                EndDate = new DateTime(2020, 10, 1)
            });
            products.Add(new Product()
            {
                Id = "H2006",
                Name = "P4",
                Description = "Product4",
                StartDate = new DateTime(2019, 10, 1),
                EndDate = new DateTime(2020, 10, 1)
            });
            products.Add(new Product()
            {
                Id = "F2006",
                Name = "P5",
                Description = "Product5",
                StartDate = new DateTime(2019, 10, 1),
                EndDate = new DateTime(2020, 10, 1)
            });

            return products;
        }
    }
}
