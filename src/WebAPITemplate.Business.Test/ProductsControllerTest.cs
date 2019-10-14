using WebAPITemplate.Business.Controllers;
using WebAPITemplate.Business.Enums;
using WebAPITemplate.Business.Interfaces;
using WebAPITemplate.Business.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPITemplate.Business.Test
{
    [TestClass]
    public class ProductControllerTest
    {
        private readonly Mock<IProductService> _mockProductsService;
        private readonly Mock<ICalculatorService> _mockCalculatorService;
        private readonly Mock<IPersistenceService> _mockPersistenceService;
        private readonly Mock<ILogger<ProductController>>  _mockLogger;

        public ProductControllerTest()
        {
            _mockProductsService = new Mock<IProductService>();
            _mockCalculatorService = new Mock<ICalculatorService>();
            _mockLogger = new Mock<ILogger<ProductController>>();
            _mockPersistenceService = new Mock<IPersistenceService>();
        }

        [TestMethod]
        public async Task ProductController_GetProducts_Returns_OKResult_With_Correct_Products_ObjectType()
        {
            // Arrange
            _mockProductsService.Setup(p => p.GetProducts()).Returns(Task.FromResult(GetTestData.GetTestProducts()));

            ProductController controller = new ProductController(_mockLogger.Object, _mockProductsService.Object, _mockCalculatorService.Object, _mockPersistenceService.Object);

            // Act
            IActionResult result = await controller.GetProducts();
            ObjectResult objectResult = result as ObjectResult;

            // Assert
            Assert.IsNotNull(objectResult);
            Assert.AreEqual(StatusCodes.Status200OK, objectResult.StatusCode);
            Assert.IsInstanceOfType(objectResult.Value, typeof(IEnumerable<Product>));
        }

        [TestMethod]
        public async Task ProductController_GetProducts_Returns_OKResult_With_Correct_Products_Number()
        {
            // Arrange
            _mockProductsService.Setup(p => p.GetProducts()).Returns(Task.FromResult(GetTestData.GetTestProducts()));

            ProductController controller = new ProductController(_mockLogger.Object, _mockProductsService.Object, _mockCalculatorService.Object, _mockPersistenceService.Object);

            // Act
            IActionResult result =  await controller.GetProducts();
            ObjectResult objectResult = result as ObjectResult;
            IEnumerable<Product> content = objectResult.Value as IEnumerable<Product>;

            // Assert
            Assert.IsNotNull(objectResult);
            Assert.AreEqual(StatusCodes.Status200OK, objectResult.StatusCode);
            Assert.AreEqual(5, content.Count());
        }

        [TestMethod]
        public async Task ProductController_GetProducts_Returns_InternalServerError_When_Exception_Occurs()
        {
            // Arrange
            _mockProductsService.Setup(p => p.GetProducts()).Throws(new Exception());

            ProductController controller = new ProductController(_mockLogger.Object, _mockProductsService.Object,  _mockCalculatorService.Object, _mockPersistenceService.Object);

            // Act
            IActionResult result = await controller.GetProducts();
            StatusCodeResult statusCodeResult = result as StatusCodeResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.GetType(), typeof(StatusCodeResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
        }


        [TestMethod]
        public async Task ProductController_GetProductsBySKU_Returns_NotFound_When_SKU_Not_Exists()
        {
            // Arrange
            _mockProductsService.Setup(p => p.GetProductById(It.IsAny<string>()))
                                .Returns(Task.FromResult(GetTestData.GetTestProducts().Where(t => t.Id == "00000").FirstOrDefault()));

            ProductController controller = new ProductController(_mockLogger.Object, _mockProductsService.Object, _mockCalculatorService.Object, _mockPersistenceService.Object);

            // Act
            IActionResult result = await controller.GetProductsValue();
            StatusCodeResult statusCodeResult = result as NotFoundResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.GetType(), typeof(NotFoundResult));
            Assert.AreEqual(StatusCodes.Status404NotFound, statusCodeResult.StatusCode);
        }

        [TestMethod]
        public async Task ProductController_GetProductsBySKU_Returns_InternalServerError_When_Exception_Occurs()
        {
            // Arrange
            _mockProductsService.Setup(p => p.GetProductById(It.IsAny<string>())).Throws(new Exception());

            ProductController controller = new ProductController(_mockLogger.Object, _mockProductsService.Object, _mockCalculatorService.Object, _mockPersistenceService.Object);

            // Act
            IActionResult result = await controller.GetProductsValue();
            StatusCodeResult statusCodeResult = result as StatusCodeResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.GetType(), typeof(StatusCodeResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
        }

        [TestMethod]
        public async Task ProductController_GetProductsBySKU_Returns_OKResult_With_Correct_Total_Amount()
        {
            // Arrange
            _mockProductsService.Setup(p => p.GetProductById(It.IsAny<string>()))
                                    .Returns(Task.FromResult(GetTestData.GetTestProducts().Where(t => t.Id == "T2006").FirstOrDefault()));
 
            _mockCalculatorService.Setup(p => p.ComputeTotalProductsValueByCurrency(It.IsAny<IEnumerable<Product>>(), It.IsAny<CurrencyCode>())).Returns(10.16M);

            ProductController controller = new ProductController(_mockLogger.Object, _mockProductsService.Object, _mockCalculatorService.Object, _mockPersistenceService.Object);

            // Act
            IActionResult result = await controller.GetProductsValue();
            ObjectResult objectResult = result as ObjectResult;
            decimal? content = objectResult.Value as decimal?;

            // Assert
            Assert.IsNotNull(objectResult);
            Assert.AreEqual(StatusCodes.Status200OK, objectResult.StatusCode);
            Assert.AreEqual(10.16M, content);
        }
    }
}
