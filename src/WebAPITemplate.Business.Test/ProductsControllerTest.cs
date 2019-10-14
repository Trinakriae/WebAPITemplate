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
    public class ProductsControllerTest
    {
        private readonly Mock<ITransactionsService> _mockTransactionsService;
        private readonly Mock<IRatesService> _mockRatesService;
        private readonly Mock<ICalculatorService> _mockCalculatorService;
        private readonly Mock<IPersistenceService> _mockPersistenceService;
        private readonly Mock<ILogger<ProductsController>>  _mockLogger;

        public ProductsControllerTest()
        {
            _mockTransactionsService = new Mock<ITransactionsService>();
            _mockRatesService = new Mock<IRatesService>();
            _mockCalculatorService = new Mock<ICalculatorService>();
            _mockLogger = new Mock<ILogger<ProductsController>>();
            _mockPersistenceService = new Mock<IPersistenceService>();
        }

        [TestMethod]
        public async Task ProductsController_GetTransactions_Returns_OKResult_With_Correct_Transactions_ObjectType()
        {
            // Arrange
            _mockTransactionsService.Setup(p => p.GetTransactions()).Returns(Task.FromResult(GetTestData.GetTestTransactions()));

            ProductsController controller = new ProductsController(_mockLogger.Object, _mockTransactionsService.Object, _mockRatesService.Object, _mockCalculatorService.Object, _mockPersistenceService.Object);

            // Act
            IActionResult result = await controller.GetTransactions();
            ObjectResult objectResult = result as ObjectResult;

            // Assert
            Assert.IsNotNull(objectResult);
            Assert.AreEqual(StatusCodes.Status200OK, objectResult.StatusCode);
            Assert.IsInstanceOfType(objectResult.Value, typeof(IEnumerable<BusinessTransaction>));
        }

        [TestMethod]
        public async Task ProductsController_GetTransactions_Returns_OKResult_With_Correct_Transactions_Number()
        {
            // Arrange
            _mockTransactionsService.Setup(p => p.GetTransactions()).Returns(Task.FromResult(GetTestData.GetTestTransactions()));

            ProductsController controller = new ProductsController(_mockLogger.Object, _mockTransactionsService.Object, _mockRatesService.Object, _mockCalculatorService.Object, _mockPersistenceService.Object);

            // Act
            IActionResult result =  await controller.GetTransactions();
            ObjectResult objectResult = result as ObjectResult;
            IEnumerable<BusinessTransaction> content = objectResult.Value as IEnumerable<BusinessTransaction>;

            // Assert
            Assert.IsNotNull(objectResult);
            Assert.AreEqual(StatusCodes.Status200OK, objectResult.StatusCode);
            Assert.AreEqual(5, content.Count());
        }

        [TestMethod]
        public async Task ProductsController_GetTransactions_Returns_InternalServerError_When_Exception_Occurs()
        {
            // Arrange
            _mockTransactionsService.Setup(p => p.GetTransactions()).Throws(new Exception());

            ProductsController controller = new ProductsController(_mockLogger.Object, _mockTransactionsService.Object, _mockRatesService.Object, _mockCalculatorService.Object, _mockPersistenceService.Object);

            // Act
            IActionResult result = await controller.GetTransactions();
            StatusCodeResult statusCodeResult = result as StatusCodeResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.GetType(), typeof(StatusCodeResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
        }

        [TestMethod]
        public async Task ProductsController_GetConversionRates_Returns_OKResult_With_Correct_Rates_ObjectType()
        {
            // Arrange
            _mockRatesService.Setup(p => p.GetConversionRates()).Returns(Task.FromResult(GetTestData.GetTestRates()));

            ProductsController controller = new ProductsController(_mockLogger.Object, _mockTransactionsService.Object, _mockRatesService.Object, _mockCalculatorService.Object, _mockPersistenceService.Object);

            // Act
            IActionResult result = await controller.GetRates();
            ObjectResult objectResult = result as ObjectResult;

            // Assert
            Assert.IsNotNull(objectResult);
            Assert.AreEqual(StatusCodes.Status200OK, objectResult.StatusCode);
            Assert.IsInstanceOfType(objectResult.Value, typeof(IEnumerable<ConversionRate>));
        }

        [TestMethod]
        public async Task ProductsController_GetConversionRates_Returns_OKResult_With_Correct_Rates_Number()
        {
            // Arrange
            _mockRatesService.Setup(p => p.GetConversionRates()).Returns(Task.FromResult(GetTestData.GetTestRates()));

            ProductsController controller = new ProductsController(_mockLogger.Object, _mockTransactionsService.Object, _mockRatesService.Object, _mockCalculatorService.Object, _mockPersistenceService.Object);

            // Act
            IActionResult result = await controller.GetRates();
            ObjectResult objectResult = result as ObjectResult;
            IEnumerable<ConversionRate> content = objectResult.Value as IEnumerable<ConversionRate>;

            // Assert
            Assert.IsNotNull(objectResult);
            Assert.AreEqual(StatusCodes.Status200OK, objectResult.StatusCode);
            Assert.AreEqual(4, content.Count());
        }

        [TestMethod]
        public async Task ProductsController_GetRates_Returns_InternalServerError_When_Exception_Occurs()
        {
            // Arrange
            _mockRatesService.Setup(p => p.GetConversionRates()).Throws(new Exception());

            ProductsController controller = new ProductsController(_mockLogger.Object, _mockTransactionsService.Object, _mockRatesService.Object, _mockCalculatorService.Object, _mockPersistenceService.Object);

            // Act
            IActionResult result = await controller.GetRates();
            StatusCodeResult statusCodeResult = result as StatusCodeResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.GetType(), typeof(StatusCodeResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
        }

        [TestMethod]
        public async Task ProductsController_GetTransactionsBySKU_Returns_NotFound_When_SKU_Not_Exists()
        {
            // Arrange
            _mockTransactionsService.Setup(p => p.GetTransactionsBySKU(It.IsAny<string>()))
                                    .Returns(Task.FromResult(GetTestData.GetTestTransactions().Where(t => t.SKU == "00000")));
            _mockRatesService.Setup(p => p.GetConversionRates()).Returns(Task.FromResult(GetTestData.GetTestRates()));

            ProductsController controller = new ProductsController(_mockLogger.Object, _mockTransactionsService.Object, _mockRatesService.Object, _mockCalculatorService.Object, _mockPersistenceService.Object);

            // Act
            IActionResult result = await controller.GetTotalTransactionValueBySKU("00000");
            StatusCodeResult statusCodeResult = result as NotFoundResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.GetType(), typeof(NotFoundResult));
            Assert.AreEqual(StatusCodes.Status404NotFound, statusCodeResult.StatusCode);
        }

        [TestMethod]
        public async Task ProductsController_GetTransactionsBySKU_Returns_InternalServerError_When_Exception_Occurs()
        {
            // Arrange
            _mockTransactionsService.Setup(p => p.GetTransactionsBySKU(It.IsAny<string>())).Throws(new Exception());

            ProductsController controller = new ProductsController(_mockLogger.Object, _mockTransactionsService.Object, _mockRatesService.Object, _mockCalculatorService.Object, _mockPersistenceService.Object);

            // Act
            IActionResult result = await controller.GetTotalTransactionValueBySKU("00000");
            StatusCodeResult statusCodeResult = result as StatusCodeResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.GetType(), typeof(StatusCodeResult));
            Assert.AreEqual(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
        }

        [TestMethod]
        public async Task ProductsController_GetTransactionsBySKU_Returns_OKResult_With_Correct_Total_Amount()
        {
            // Arrange
            _mockTransactionsService.Setup(p => p.GetTransactionsBySKU(It.IsAny<string>()))
                                    .Returns(Task.FromResult(GetTestData.GetTestTransactions().Where(t => t.SKU == "T2006")));
            _mockRatesService.Setup(p => p.GetConversionRates()).Returns(Task.FromResult(GetTestData.GetTestRates()));
            _mockCalculatorService.Setup(p => p.ComputeTotalTransactionValueByCurrency(It.IsAny<IEnumerable<Amount>>(), It.IsAny<IEnumerable<ConversionRate>>(), It.IsAny<CurrencyCode>())).Returns(10.16M);

            ProductsController controller = new ProductsController(_mockLogger.Object, _mockTransactionsService.Object, _mockRatesService.Object, _mockCalculatorService.Object, _mockPersistenceService.Object);

            // Act
            IActionResult result = await controller.GetTotalTransactionValueBySKU("T2006");
            ObjectResult objectResult = result as ObjectResult;
            decimal? content = objectResult.Value as decimal?;

            // Assert
            Assert.IsNotNull(objectResult);
            Assert.AreEqual(StatusCodes.Status200OK, objectResult.StatusCode);
            Assert.AreEqual(10.16M, content);
        }
    }
}
