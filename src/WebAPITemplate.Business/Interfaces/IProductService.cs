using WebAPITemplate.Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPITemplate.Business.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetProducts();
        Task<Product> GetProductById(string Id);
    }
}
