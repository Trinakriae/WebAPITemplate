using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPITemplate.Business.Enums;
using WebAPITemplate.Business.Interfaces;
using WebAPITemplate.Business.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebAPITemplate.Business.Controllers
{
    //FINISHED
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;
        private readonly ICalculatorService _calculatorService;
        private readonly IPersistenceService _persistenceService;

        public ProductController(ILogger<ProductController> logger, IProductService productService, ICalculatorService calculatorService, IPersistenceService persistenceService)
        {
            _logger = logger;
            _productService = productService;
            _calculatorService = calculatorService;
            _persistenceService = persistenceService;
        }

        // GET: api/products/transactions
        [HttpGet("transactions")]
        public async Task<IActionResult> GetTransactions()
        {
            try
            {
                IEnumerable<Product> products = await _productService.GetProducts();
                _persistenceService.SaveProducts(products);
                return Ok(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // GET: api/products/total/T2006
        [HttpGet("total/{SKU}")]
        public async Task<IActionResult> GetProductsValue()
        {
            try
            {
                IEnumerable<Product> products = await _productService.GetProducts();
                if(products == null || products.Count() == 0)
                {
                    return NotFound();
                }


                return Ok(_calculatorService.ComputeTotalTransactionValueByCurrency(products, CurrencyCode.EUR));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Peticion erronea");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}