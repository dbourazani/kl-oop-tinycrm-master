using NewConsoleApp;

namespace TinyCrm.Core.Model
{
    public class Product
    {
        public string Code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }

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

    }
}