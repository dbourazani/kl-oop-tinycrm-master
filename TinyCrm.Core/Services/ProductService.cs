using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyCrm.Core.Data;
using TinyCrm.Core.Model;
using TinyCrm.Core.Model.Options;

namespace TinyCrm.Core.Services
{
    public class ProductService : IProductService 
    {
        public TinyCrmDbContext context;
        public ProductService(TinyCrmDbContext dbContext)
        {
            context = dbContext;
        }
        public ProductService()
        {
            var context = new TinyCrmDbContext();
        }
        public List<Product> SearchProduct(SearchProductOptions options)
        {
            if (options == null)
            {
                return null;
            }
            
                var query = context
                    .Set<Product>()
                    .AsQueryable();

                if (options.Id != null)
                {
                    query = query.Where(c => c.Id == options.Id);
                }
                if (options.Code != null)
                {
                    query = query.Where(c => c.Code == options.Code);
                }
            else
            {
                return null;
            }
                if (options.Description  != null)
                {
                    query = query.Where(c => c.Description.
                    Contains(options.Description).ToString() 
                    == options.Description);
                }
                if(options.PriceMax != null &&
                    options.PriceMax > 0)
                {
                    query = query.Where(c => c.Price <= options.PriceMax.Value); 
                }
                if(options.PriceMin != null &&
                    options.PriceMin > 0)
                {
                    query = query.Where(c => c.Price >= options.PriceMin.Value); 
                }
                
                return query.ToList() ;  
        }

        public Product CreateProduct(CreateProductOptions options)
        {
            var product = new Product();
            if(options == null)
            {
                return null;
            }

            if(options.Id ==null ||
                options.Name == null ||
                options.Price == null ||
                options.Category == 0)
            {
                return null;
            }
            
                product.Id = options.Id;
                product.Name = options.Name;
                product.Price = options.Price;
                product.Category = options.Category;
                context.Set<Product>().Add(product);
                context.SaveChanges();
                return product;
        }
        public int? SumOfStocks()
        {
           
            var query = context
                    .Set<Product>()
                    .AsQueryable();


            var totalStock = query.Sum(c => c.InStock);
            return totalStock;
            
        }
    }
}
