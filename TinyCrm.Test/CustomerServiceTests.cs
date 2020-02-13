using System;
using TinyCrm.Core.Data;
using TinyCrm.Core.Model;
using TinyCrm.Core.Model.Options;
using TinyCrm.Core.Services;
using Xunit;


namespace TinyCrm.Test
{
    public class CustomerServiceTests :
        IClassFixture<TinyCrmFixture>
    {
        private TinyCrmDbContext context_;
        private ICustomerService customers_; 
   
        public CustomerServiceTests(
            TinyCrmFixture fixture)
        {
            context_ = fixture.Context ;
            customers_ = fixture.Customers; 
        }

        [Fact]
        public void CreateCustomerSuccess()
        {

            var options = new CreateCustomerOptions()
            {
                FirstName = "dimitra",
                Email = "dkgdgkg@hl",
                VatNumber = "123456779"
            };
            var customer = customers_.Create(options);

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
            var customers = customers_.Search(option1);

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
           ;
            var options = new CreateCustomerOptions()
            {
                FirstName = "dimitra",
                Email = "dkgdgkg@hl",
                VatNumber = null
            };
            var customer = customers_.Create(options);
            Assert.Null(customer);  
        }
        [Fact]
        public void CreateCustomer_Fail_Email()
        {
            
            var options = new CreateCustomerOptions()
            {
                FirstName = "dimitra",
                Email = null,
                VatNumber = "123456789"
            };
            var customer = customers_.Create(options);
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

        [Fact]
        public void SearchCustomerSuccess()
        {
            
            var optionsCreateCustomer = new CreateCustomerOptions()
            {
                FirstName = "dimitra",
                Email = "dimitra@hotmail.gr",
                VatNumber = "012345678"
            };
            var customer = customers_.Create(optionsCreateCustomer);
            var optionsSearchCustomer = new SearchCustomerOptions()
            {
                FirstName = optionsCreateCustomer.FirstName,
                Email = optionsCreateCustomer.Email,
                VatNumber = optionsCreateCustomer.VatNumber
            };
            var customer2 = customers_.Search(optionsSearchCustomer);
            Assert.NotNull(customer2);
            //Assert.NotEmpty(customer2);  
       
        } 
        [Fact]
        public void SearchCustomerFail()
        {
          
            var optionsCreateCustomer = new CreateCustomerOptions()
            {
                FirstName = "dimitra",
                Email = "dimitra@hotmail.gr",
                VatNumber = "412375678"
            };
            var customer = customers_.Create(optionsCreateCustomer);
            var optionsSearchCustomer = new SearchCustomerOptions()
            {
                FirstName = null,
                Email = null,
                VatNumber = null
            };
            var customer2 = customers_.Search(optionsSearchCustomer);
            Assert.Null(customer2);   
        }

    }
}
