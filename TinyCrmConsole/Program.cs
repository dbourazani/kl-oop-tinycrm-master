
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
               


                    ICustomerService service = new CustomerService(context) ;
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
                
            }
        }
    }
}
