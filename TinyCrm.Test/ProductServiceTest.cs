using NewConsoleApp;
using TinyCrm.Core.Data;
using TinyCrm.Core.Model;
using TinyCrm.Core.Model.Options;
using TinyCrm.Core.Services;
using Xunit;

namespace TinyCrm.Test
{
    public class ProductServiceTests
    {
        private TinyCrmDbContext context_;

        public ProductServiceTests()
        {
            context_ = new TinyCrmDbContext();
        }

        [Fact]
        public void CreateProductSuccess()
        {
            IProductService productService =
                new ProductService(context_);
            var options = new CreateProductOptions()
            {
                Id = "87",
                Name = "mobile",
                Code  = "1",
                Price = 5m,
                Category = ProductCategory.Laptops
            };

            var product = productService.CreateProduct(options);
            var optionsSearch = new SearchProductOptions()
            {
                Name = options.Name,
                Code = options.Code,
                Price = options.Price ,
                
                
            };
            var products = productService.SearchProduct(optionsSearch);   
            Assert.NotNull(product); 
              
        }
        [Fact]
        public void CreateProductFail()
        {
            IProductService productService =
                new ProductService(context_);
            var options = new CreateProductOptions()
            { 
                Price = null,
                Name = "mobile",
                Code = "1",
                Category = ProductCategory.Laptops
            }; var product = productService.CreateProduct(options);
            Assert.Null(product);

            options = new CreateProductOptions()
            { 
                Price = 5,
                Name = null,
                Code = "1",
                Category = ProductCategory.Laptops
            };

            product = productService.CreateProduct(options);
            Assert.Null(product);

            options = new CreateProductOptions()
            { 
                Price = 3,
                Name = "mobile",
                Code = null,
                Category = ProductCategory.Laptops
            };

            product = productService.CreateProduct(options);
            Assert.Null(product);
            options = new CreateProductOptions()
            { 
                Price = 3,
                Name = "mobile",
                Code = "5",
                Category = ProductCategory.Invalid 
            };

            product = productService.CreateProduct(options);
            Assert.Null(product);
        }

        [Fact]
        public void SearchProductSuccess()
        {
            IProductService productService =
               new ProductService(context_);
      
            var options = new CreateProductOptions()
            {
                Price = 5,
                Name = "mobile",
                Code = "1",
               
            }; 
            var product = productService.CreateProduct(options);
            var optionsCreate = new SearchProductOptions()
            {
                Price = 5,
                Name = "mobile",
                Code = "1",
            };
            var products = productService.SearchProduct(optionsCreate);
            Assert.NotNull(products);
              
        }
        [Fact]
        public void SearchProductFail()
        {
            IProductService productService =
                   new ProductService(context_);
            var options = new SearchProductOptions()
            {
                Price = 3,
                Name = "mobile",
                Code = null,
            };
            var product = productService.SearchProduct(options);
            
            Assert.Null(product);  
        }
    }
}
