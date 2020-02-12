using System;
using TinyCrm.Core.Data;
using TinyCrm.Core.Model;
using TinyCrm.Core.Model.Options;
using TinyCrm.Core.Services;
using Xunit;


namespace TinyCrm.Test
{
    public class CustomerServiceTests
    {
        private TinyCrmDbContext context_;
   
        private CustomerServiceTests()
        {
            context_ = new TinyCrmDbContext();
        }

        [Fact]
        public void CreateCustomerSuccess()
        {
            ICustomerService customerService =
                new CustomerService(context_);

            var options = new CreateCustomerOptions()
            {
                FirstName = "dimitra",
                Email = "dkgdgkg@hl",
                VatNumber = "123456789"
            };
            var customer = customerService.Create(options);

            Assert.NotNull(customer);
            Assert.Equal(options.Email, customer.Email);
            Assert.Equal(options.VatNumber, customer.VatNumber);
            Assert.Equal(options.FirstName, customer.FirstName);
            
            var option1 = new SearchCustomerOptions()
            {
                FirstName = options.FirstName,
                Email = options.Email,
                VatNumber = options.VatNumber
            };
            var customers = customerService.Search(option1);

            Assert.NotNull(customer);
            var length = customers.Count;
            Assert.True(length == 1);//elegxoume ama exei ginei h egrafh. opote prpei ka8e customer na exei mia egrafh 
            //alliws Assert.Single(customers);
            var dbCustomer = customers[0];
            Assert.Equal(customer.Id, dbCustomer.Id); 
            
         }
        
        [Fact]
        public void CreateCustomer_Fail_Null_VatNumber()
        {
            ICustomerService customerService =
                new CustomerService(context_);
            var options = new CreateCustomerOptions()
            {
                FirstName = "dimitra",
                Email = "dkgdgkg@hl",
                VatNumber = null
            };
            var customer = customerService.Create(options);
            Assert.Null(customer);  
        }
        [Fact]
        public void CreateCustomer_Fail_Email()
        {
            ICustomerService customerService =
                new CustomerService(context_);
            var options = new CreateCustomerOptions()
            {
                FirstName = "dimitra",
                Email = null,
                VatNumber = "123456789"
            };
            var customer = customerService.Create(options);
            Assert.Null(customer);
            options = new CreateCustomerOptions()
            {
                FirstName = "dimitra",
                Email = "gjhjh",
                VatNumber = "123456789"
            };
            
            Assert.Null(customer);
            options = new CreateCustomerOptions()
            {
                FirstName = "dimitra",
                Email = "  ",
                VatNumber = "123456789"
            };
            
            Assert.Null(customer);
        }

    }
}
