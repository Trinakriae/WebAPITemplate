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
        public void CalculatorService_ComputeTotalTransactionValueByCurrency_Returns_Correct_Total_With_Existing_Rates()
        {
            // Arrange
            IEnumerable<BusinessTransaction> transactions = GetTestData.GetTestTransactions();
            IEnumerable<ConversionRate> rates = GetTestData.GetTestRates();
            CalculatorService calculator = new CalculatorService();
            IEnumerable<Amount> amounts = transactions.Select(t => new Amount { Value = t.Amount, Currency = t.Currency });

            // Act
            decimal total = calculator.ComputeTotalTransactionValueByCurrency(amounts, rates, CurrencyCode.EUR);

            //Assert
            Assert.AreEqual(69.13172M, total);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void CalculatorService_ComputeTotalTransactionValueByCurrency_Throws_Exception_When_Missing_Rate()
        {
            // Arrange
            IEnumerable<BusinessTransaction> transactions = GetTestData.GetTestTransactions();
            IEnumerable<ConversionRate> rates = GetTestData.GetTestRates();
            CalculatorService calculator = new CalculatorService();
            IEnumerable<Amount> amounts = transactions.Select(t => new Amount { Value = t.Amount, Currency = t.Currency });

            // Act
            decimal total = calculator.ComputeTotalTransactionValueByCurrency(amounts, rates, CurrencyCode.TRY);

            // Assert - Expects exception
        }
    }
}
