using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TinyCrm.Core.Data;
using TinyCrm.Core.Model;
using TinyCrm.Core.Model.Options;
using TinyCrm.Core.Services;

using Xunit;

namespace TinyCrm.Tests
{
    public class OrderServiceTests :
        IClassFixture<TinyCrmFixture>
    {
        private ICustomerService customer_;
        private IProductService products_;
        private TinyCrmDbContext context_;
        private IOrderService order_;
        private ProductServiceTests productServiceTests_;
        private CustomerServiceTests customerServiceTests_;
        public OrderServiceTests(
            TinyCrmFixture fixture)
        {
            productServiceTests_ =
                  new ProductServiceTests(fixture);
            context_ = fixture.Context;
            customer_ = fixture.Customers;
            products_ = fixture.Products;
            order_ = fixture.Orders;
            customerServiceTests_ =
                new CustomerServiceTests(fixture);
        }

        [Fact]
        public void CreateOrder_Success()
        {
            var customer = customerServiceTests_
                .CreateCustomer_Success();
            var p1 = productServiceTests_.AddProduct_Success();
            var p2 = productServiceTests_.AddProduct_Success();

            var orderOptions = new CreateOrderOptions
            {
                CustomerId = customer.Id,
                ProductIds = new List<Guid>() { p1.Id, p2.Id }
            };
            var createorder = order_.CreateOrder(orderOptions);

            Assert.True(createorder.Success);


            var orderId = createorder.Data.Id;
            var order = context_.Set<Order>()
                //.Include(o=> o.OrderProducts)
                .Where(o => o.Id == orderId)
                .SingleOrDefault();
            Assert.NotNull(order);

            foreach (var id in orderOptions.ProductIds)
            {
                var op = order.OrderProducts
                    .Where(p => p.ProductId == id)
                    .SingleOrDefault();

                Assert.NotNull(op);
            }
        }

    }
}