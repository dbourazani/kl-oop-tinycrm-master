﻿using TinyCrm.Core.Model.Options;
using TinyCrm.Core.Services;
using TinyCrm.Core.Data;

using Xunit;
using TinyCrm.Core.Model;
using NewConsoleApp;

namespace TinyCrm.Tests
{
    public class ProductServiceTests :
        IClassFixture<TinyCrmFixture>
    {
        private TinyCrmDbContext context_;
        private IProductService products_;

        public ProductServiceTests(
            TinyCrmFixture fixture)
        {
            context_ = fixture.Context;
            products_ = fixture.Products;
        }

        [Fact]
        public Product AddProduct_Success()
        {
            var options = new AddProductOptions()
            {
                Name = "mobile",
                Price = 10m,
                ProductCategory = ProductCategory.Computers
            };

            var result = products_.AddProduct(options);
            Assert.NotNull(result);
            Assert.NotNull(result.Data);
            Assert.Equal(Core.StatusCode.Success, result.ErrorCode);
            return result.Data;
        }
    }
}
