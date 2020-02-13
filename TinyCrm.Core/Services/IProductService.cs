using System;
using System.Collections.Generic;
using System.Text;
using TinyCrm.Core.Data;
using TinyCrm.Core.Model;
using TinyCrm.Core.Model.Options;

namespace TinyCrm.Core.Services
{
    public interface IProductService
    {
        public List<Product> SearchProduct(SearchProductOptions options);
        public APIResult<Product> CreateProduct(CreateProductOptions options);
        public int? SumOfStocks();
    }
}
