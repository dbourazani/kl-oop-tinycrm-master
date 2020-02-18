using System;
using System.Collections.Generic;
using System.Text;
using TinyCrm.Core.Model;
using TinyCrm.Core.Model.Options;

namespace TinyCrm.Core.Services
{
    public interface IReportService
    {
        public List<Product> TenMostSoldProducts();
        public List<Customer> TopTenCustomersByGross();
        public decimal? TotalSales(SearchOrderOptions startDay,
            SearchOrderOptions lastDay);
        public List<int> StateOfOrder();
    }
}
