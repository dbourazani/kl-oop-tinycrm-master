using NewConsoleApp;
using TinyCrm.Core.Data;
using TinyCrm.Core.Model;
using TinyCrm.Core.Model.Options;
using TinyCrm.Core.Services;
using Xunit;

namespace TinyCrm.Test
{
    public class ProductServiceTests :
        IClassFixture<TinyCrmFixture> //me ayto ton tropo den janagrafw ta ta IProduct..... synexeia
    {                                  //ayto to interface den exei kamia me8odo

        private TinyCrmDbContext context_;
        private IProductService products_;  
        public ProductServiceTests(
            TinyCrmFixture fixture)
        {
            context_ = fixture.Context ;
            products_ = fixture.Products; 
        }

        [Fact]
        public void CreateProductSuccess()
        {
           
            var options = new CreateProductOptions()
            {
               
                Name = "mobile",
                Code  = "1",
                Price = 5m,
                Category = ProductCategory.Laptops
            };

            var result = products_.CreateProduct(options);
            var optionsSearch = new SearchProductOptions()
            {
                Name = options.Name,
                Code = options.Code,
                Price = options.Price ,
                
                
            };
            var products = products_.SearchProduct(optionsSearch);   
            Assert.NotNull(result);
            Assert.NotNull(result.Data);
            Assert.Equal(StatusCode.Success, result.ErrorCode); 

              
        }
        [Fact]
        public void CreateProductFail()
        {
           
            var options = new CreateProductOptions()
            { 
                Price = null,
                Name = "mobile",
                Code = "1",
                Category = ProductCategory.Laptops
            }; var product = products_.CreateProduct(options);
            Assert.Null(product);
            


            options = new CreateProductOptions()
            { 
                Price = 5,
                Name = null,
                Code = "1",
                Category = ProductCategory.Laptops
            };

            product = products_.CreateProduct(options);
            Assert.Null(product);

            options = new CreateProductOptions()
            { 
                Price = 3,
                Name = "mobile",
                Code = null,
                Category = ProductCategory.Laptops
            };

            product = products_.CreateProduct(options);
            Assert.Null(product);
            options = new CreateProductOptions()
            { 
                Price = 3,
                Name = "mobile",
                Code = "5",
                Category = ProductCategory.Invalid 
            };

            product = products_.CreateProduct(options);
            Assert.Null(product);
        }

        [Fact]
        public void SearchProductSuccess()
        {
           
      
            var options = new CreateProductOptions()
            {
                Price = 5,
                Name = "mobile",
                Code = "1",
               
            }; 
            var product = products_.CreateProduct(options);
            var optionsCreate = new SearchProductOptions()
            {
                Price = 5,
                Name = "mobile",
                Code = "1",
            };
            var products = products_.SearchProduct(optionsCreate);
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
