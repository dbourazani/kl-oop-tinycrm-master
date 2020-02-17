using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TinyCrm.Core;
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

        [Fact]
        public void CreateOrderFailNoProduct()
        {

            var customer = customerServiceTests_
                .CreateCustomer_Success();

            var orderOptions = new CreateOrderOptions()
            {
                CustomerId = customer.Id

            };
            var createorder = order_.CreateOrder(orderOptions);

            Assert.Null(createorder.Data);
        }

        [Fact]
        public void CreateOrderFailNoCustomerId()
        {
            var p1 = productServiceTests_.AddProduct_Success();
            var p2 = productServiceTests_.AddProduct_Success();
            var orderOptions = new CreateOrderOptions
            {
                ProductIds = new List<Guid>() { p1.Id, p2.Id }
            };
            
            var order = order_.CreateOrder(orderOptions);
            Assert.Null(order.Data);
        }


        [Fact]
        public void SearchOrderSuccess()
        {
            var customer = customerServiceTests_
                .CreateCustomer_Success();
            var p1 = productServiceTests_.AddProduct_Success();
            var p2 = productServiceTests_.AddProduct_Success();

            var options = new CreateOrderOptions()
            {
               CustomerId = customer.Id,
               ProductIds = new List<Guid>() { p1.Id , p2.Id }
            };

            var resultCreate = order_.CreateOrder(options);
            Assert.NotNull(resultCreate);

            var searchOrder = new SearchOrderOptions()
            {
                CustomerId = resultCreate.Data.CustomerId,   
                OrderId =  resultCreate.Data.Id,   
                VatNumber = resultCreate.Data.Customer.VatNumber    
            };

            var result = order_.SearchOrder(searchOrder);
            Assert.NotNull(result.Data);
            Assert.Equal(1, result.Data.Count());
            Assert.Equal(StatusCode.Success, result.ErrorCode); 
             
        }

        [Fact]
        public void SearchOrderFailCustomerId()
        {
            var options = new SearchOrderOptions()
            {
                CustomerId = null,
                VatNumber = $"111{DateTimeOffset.Now:ffffff}"
            };

            var result = order_.SearchOrder(options);

            Assert.Empty(result.Data.ToList());
        }
        
        [Fact]
        public void SearchOrderFailOrderId()
        {
            var options = new SearchOrderOptions()
            {
                CustomerId = 1,
                OrderId = null,
                VatNumber = $"111{DateTimeOffset.Now:ffffff}"
            };

            var result = order_.SearchOrder(options);

            Assert.Empty(result.Data.ToList());
        }
        
        [Fact]
        public void SearchOrderFailVatNumber()
        {
            var options = new SearchOrderOptions()
            {
                CustomerId =1,
                VatNumber = null
            };

            var result = order_.SearchOrder(options);

            Assert.Empty(result.Data.ToList());
        }
    }
}