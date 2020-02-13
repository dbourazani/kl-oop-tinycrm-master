using NewConsoleApp;
using System;

namespace TinyCrm.Core.Model
{
    public class Product
    {
        public string Code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Guid Id { get; set; }//to dhlvnv string giati ta pernav egv xerata

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? Price { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ProductCategory Category { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }
        public int? InStock { get; set; }
        public Product()
        {
            //Id = Guid.NewGuid(); 
        }
    }
}