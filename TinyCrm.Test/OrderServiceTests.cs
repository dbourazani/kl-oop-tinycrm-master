using NewConsoleApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyCrm.Core.Data;
using TinyCrm.Core.Model;
using TinyCrm.Core.Model.Options;
using TinyCrm.Core.Services;
using Xunit;

namespace TinyCrm.Test
{
    public class OrderServiceTests :
        IClassFixture<TinyCrmFixture>
    {
        private TinyCrmDbContext context_;
        private ICustomerService customers_;
        private IProductService product_;

        public OrderServiceTests(
            TinyCrmFixture fixture)
        {
            
            customers_ = fixture.Customers;
            context_ = fixture.Context;
            product_ = fixture.Products;  
        }

        [Fact]
        public void CreateOrderSuccess()
        {
            //
            var option = new CreateCustomerOptions()
            {
                FirstName = "Dimitra",
                Email = "gfjhhfj@j",
                VatNumber ="123456789"
            };
            var customer = customers_.Create(option);
            Assert.NotNull(customer);

            var optionProduct = new CreateProductOptions()
            {
                Category = ProductCategory.HardDisks ,
                Price = 100m,
                Name ="product2"
                
            };

            var presult = product_.CreateProduct(optionProduct);
            Assert.Equal(StatusCode.Success, presult.ErrorCode);
            var optionProduct2 = new CreateProductOptions()
            {
                Category = ProductCategory.Laptops,
                Price = 100m,
                Name ="product1"
                
            };

            var presult2 = product_.CreateProduct(optionProduct2);
            Assert.Equal(StatusCode.Success, presult2.ErrorCode);     
            var order = new Order()
            {
                Status = Status.Delivered,
                Deliveryaddress = "davarh 5",
                CreatedDateTime = DateTimeOffset.Now,
              
            };

            order.OrderProducts.Add(
               new OrderProduct()
               {
                   Product = presult.Data
               });
            order.OrderProducts.Add(
                new OrderProduct()
                {
                    Product = presult2.Data
                });

            customer.Orders.Add(order);
            context_.SaveChanges();
            var dbOrder = context_.Set<Order>()
                .SingleOrDefault(o => o.Id == order.Id);
            Assert.NotNull(dbOrder);
            Assert.Equal(order.Deliveryaddress, dbOrder.Deliveryaddress);  
        }

        
    }
}
