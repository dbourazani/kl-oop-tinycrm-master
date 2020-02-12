using TinyCrm.Core.Data;
using TinyCrm.Core.Model.Options;
using TinyCrm.Core.Services;
using Xunit;

namespace TinyCrm.Test
{
    public class ProductServiceTests
    {
        private TinyCrmDbContext context_;

        private ProductServiceTests()
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
                Name = "mobile",
                Price  = 10m,
            };

            var product = productService.CreateProduct(options);
            Assert.NotNull(product);
           
        }
    }
}
