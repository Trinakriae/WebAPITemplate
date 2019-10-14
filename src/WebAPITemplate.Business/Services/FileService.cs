using WebAPITemplate.Business.Interfaces;
using WebAPITemplate.Business.Models;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using WebAPITemplate.Shared.Utils;

namespace WebAPITemplate.Business.Services
{
    public class FileService : IPersistenceService
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        public FileService(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public void SaveProducts(IEnumerable<Product> products)
        {
            try
            {
                string path = $"{_hostingEnvironment.ContentRootPath }\\Data";
                string fileName = $"{DateTime.UtcNow.ToString("yyyyMMddTHHmmss")}-transactions.json";

                FileUtils.WriteJSONFile(path, fileName, products);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
