using WebAPITemplate.Business.Enums;
using WebAPITemplate.Business.Models;
using WebAPITemplate.Business.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebAPITemplate.Business.Test
{
    [TestClass]
    public class CalculatorServiceTest
    {

        [TestMethod]
        public void CalculatorService_ComputeTotalProductsValueByCurrency_Returns_Correct_Total_With_Existing_Rates()
        {
            // Arrange
            IEnumerable<Product> products = GetTestData.GetTestProducts();
            CalculatorService calculator = new CalculatorService();

            // Act
            decimal total = calculator.ComputeTotalProductsValueByCurrency(products, CurrencyCode.EUR);

            //Assert
            Assert.AreEqual(69.13172M, total);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void CalculatorService_ComputeTotalTransactionValueByCurrency_Throws_Exception_When_Missing_Rate()
        {
            // Arrange
            IEnumerable<Product> products = GetTestData.GetTestProducts();
            CalculatorService calculator = new CalculatorService();


            // Act
            decimal total = calculator.ComputeTotalProductsValueByCurrency(products, CurrencyCode.TRY);

            // Assert - Expects exception
        }
    }
}
