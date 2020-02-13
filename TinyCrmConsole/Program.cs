
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
            using (var context = new TinyCrmDbContext())
            {
                ICustomerService service = new CustomerService(context);
                //var results = customerService.Search(null);

                //Console.WriteLine($"Found {results.Count()} customers");
                var options = new CreateCustomerOptions()
                {
                    FirstName = "Eleana",
                    VatNumber = "ere342",
                    Email = "eleana@gmail.com"
                };

                var result = service.Create(options);

                IProductService productService = new ProductService(context);
                var optionsForProduct = new CreateProductOptions()
                {
                    Name = " dbgsgks",
                    Price = 12m
                };
                var resultProduct = productService.CreateProduct(optionsForProduct);
                var customer = new Customer()
                {
                    VatNumber = "4576873",
                    Email = "hhfyeggf@vhh"
                };
                var optionsCustomer = new SearchCustomerOptions
                {
                    Email = "hhdfkdgk@vh"
                };
                var results = service.Search(optionsCustomer);

                context.Add(new Product
                {
                    Name = "mobile",
                    InStock = 5,
                    Id = "55"
                });
                context.Add(new Product
                {
                    Name = "laptop",
                    InStock = 5,
                    Id = "56"
                });
               
              context.SaveChanges();
                var sum = productService.SumOfStocks();
            }
        }
    }
}
