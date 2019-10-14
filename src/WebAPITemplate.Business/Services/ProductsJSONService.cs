using WebAPITemplate.Business.Enums;
using WebAPITemplate.Business.Interfaces;
using WebAPITemplate.Business.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebAPITemplate.Business.Services
{
    public class ProductsJSONService : IProductService
    {
        private readonly HttpClient _client;

        public ProductsJSONService(IHttpClientFactory httpClientFactory)
        {
            _client = httpClientFactory.CreateClient("QuietStone");
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync("Product.json");
                string contentString = await response.Content.ReadAsStringAsync();
                
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    IEnumerable<Product> transactions = JsonConvert.DeserializeObject<IEnumerable<Product>>(contentString);
                    return transactions;
                }
                else
                {
                    throw new Exception($"Status Code: {response.StatusCode} - Content {contentString}");
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Product> GetProductById(string Id)
        {
            try
            {
                IEnumerable<Product> products = await GetProducts();
                return products.Where(t => t.Id == Id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

