using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyCrm.Core.Data;
using TinyCrm.Core.Model;
using TinyCrm.Core.Model.Options;

namespace TinyCrm.Core.Services
{
    public class ReportService : IReportService
    {
        private readonly TinyCrmDbContext context_;

        private readonly ICustomerService customer_;

        private readonly IProductService product_;
        private readonly IOrderService order_;

        public ReportService(
            TinyCrmDbContext context,
            ICustomerService customers,
            IProductService products,
            IOrderService orders)
        {
            context_ = context;
            customer_ = customers;
            product_ = products;
            order_ = orders;
        }

        public List<Product> TenMostSoldProducts()
        {
            var query = context_.Set<OrderProduct>()
                .GroupBy(o => o.ProductId)
                .OrderByDescending(o => o.Key).Take(10);

            var products = new List<Product>();
            foreach (OrderProduct product in query)
            {
                products.Add(product.Product);
            }
            return products;
        }

        public List<Customer> TopTenCustomersByGross()
        {
            var query = context_.Set<Customer>()
                .OrderByDescending(o => o.TotalGross).Take(10);
            var customers = new List<Customer>();
            foreach (var customer in query)
            {
                customers.Add(customer);
            }
            return customers;
        }

        public decimal? TotalSales(SearchOrderOptions startDay,
            SearchOrderOptions lastDay)
        { 
            if(startDay == null || 
                lastDay ==null || 
                lastDay.PeriodOfTime == null ||
                startDay.PeriodOfTime == null )
            {
                return null;
            }
            var query = context_.Set<Order>();
            var orders = new Order();
            var sumOfOrders = 0.0m;
            foreach (var order in query)
            {
                if (startDay.PeriodOfTime <= order.CreatedDateTime ||
                    lastDay.PeriodOfTime >= order.CreatedDateTime)
                {
                    sumOfOrders = sumOfOrders + orders.Price;
                }
            }
            return sumOfOrders;
        }

        public List<int> StateOfOrder()
        {
            var query = context_.Set<Order>().AsEnumerable() ;
            var sumOfActive = 0;
            var sumOfPending = 0;
            var sumOfCancelled = 0;
            foreach (var order in query)
            {
                if (order.Status.ToString() == "Active" )
                {
                    sumOfActive = sumOfActive +1;
                }
                if (order.Status.ToString() == "Pending")
                {
                    sumOfPending = sumOfPending + 1;
                }
                if (order.Status.ToString() == "Cancelled" )
                {
                    sumOfCancelled = sumOfCancelled +1;
                }
            }
            var results = new List<int> {
                sumOfActive, sumOfPending, sumOfCancelled };
            return results;
        }
    }
}
