using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyCrm.Core.Data;
using TinyCrm.Core.Model;
using TinyCrm.Core.Model.Options;

namespace TinyCrm.Core.Services
{
    class ProductService
    {
        public List<Product> SearchProduct(SearchProductOptions options)
        {
            if (options == null)
            {
                return null;
            }
            using (var context = new TinyCrmDbContext())
            {
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
            using (var context = new TinyCrmDbContext())
            {
                product.Id = options.Id;
                product.Name = options.Name;
                product.Price = options.Price;
                product.Category = options.Category;
                context.Set<Product>().Add(product);
                context.SaveChanges();
                return product;
            }
            
        }
    }
}
