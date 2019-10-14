using WebAPITemplate.Business.Interfaces;
using WebAPITemplate.Business.Models;
using WebAPITemplate.Business.Utils;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;

namespace WebAPITemplate.Business.Services
{
    public class FileService : IPersistenceService
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        public FileService(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public void SaveTransactions(IEnumerable<BusinessTransaction> transactions)
        {
            try
            {
                string path = $"{_hostingEnvironment.ContentRootPath }\\Data";
                string fileName = $"{DateTime.Now.ToString("yyyyMMddTHHmmss")}-transactions.json";

                FileUtils.WriteJSONFile(path, fileName, transactions);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void SaveRates(IEnumerable<ConversionRate> rates)
        {
            try
            {
                string path = $"{_hostingEnvironment.ContentRootPath }\\Data";
                string fileName = $"{DateTime.Now.ToString("yyyyMMddTHHmmss")}-rates.json";

                FileUtils.WriteJSONFile(path, fileName, rates);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        
    }
}
