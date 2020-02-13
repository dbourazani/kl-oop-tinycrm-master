using NewConsoleApp;
using System;

namespace TinyCrm.Core.Model.Options
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateProductOptions
    {
        /// <summary>
        /// 
        /// </summary>
        //public Guid Id { get; set; }den mas xreiazetai giati to guid to ftiaxnei apo mono toy

        ///// <summary>
        ///// 
        ///// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? Price { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ProductCategory Category { get; set; }
        public string Code { get; set; }

    }
}