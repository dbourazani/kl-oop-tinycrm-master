
using System;
using System.Linq;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.FileExtensions;
using Microsoft.Extensions.Configuration.Json;

using TinyCrm.Core.Data;
using TinyCrm.Core.Model;
using TinyCrm.Core.Model.Options;
using TinyCrm.Core.Services;

namespace TinyCrmConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            ICustomerService customerService = new CustomerService();
            //var results = customerService.Search(null);

            //Console.WriteLine($"Found {results.Count()} customers");
            var options = new CreateCustomerOptions()
            {
                FirstName = "Eleana",
                VatNumber = "ere342",
                Email = "eleana@gmail.com"
            };

            var result = customerService.Create(options);

            IProductService productService = new ProductService(); 
            var optionsForProduct = new CreateProductOptions()
            {
                Name = " dbgsgks",
                Price = 12m
            };
            var resultProduct= customerService.CreateProduct(optionsForProduct);
        }
    }
}
