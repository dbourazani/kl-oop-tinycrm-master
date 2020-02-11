using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyCrm.Core.Data;
using TinyCrm.Core.Model;
using TinyCrm.Core.Model.Options;

namespace TinyCrm.Core.Services
{
    public class CustomerService : ICustomerService
    {
        //public TinyCrmDbContext TinyCrmDbContext(string connString)
        //{
        //   var connectionString_ = connString;

        //}
        public List<Customer> Search(
            SearchCustomerOptions options)
        {
            if (options == null)
            {
                return null;
            }

            using (var context = new TinyCrmDbContext())
            {
                var query = context
                    .Set<Customer>()
                    .AsQueryable();

                if (options.Id != null)
                {
                    query = query.Where(
                        c => c.Id == options.Id);
                }

                if (options.VatNumber != null)
                {
                    query = query.Where(
                        c => c.VatNumber == options.VatNumber);
                }

                if (options.Email != null)
                {
                    query = query.Where(
                        c => c.Email == options.Email);
                }
                if(options.FirstName != null)
                {
                    query = query.Where(c => 
                        c.FirstName.Contains(options.FirstName).ToString() 
                                            == options.FirstName);
                }
                if(options.LastName != null)
                {
                    query = query.Where(c => 
                        c.FirstName.Contains(options.LastName).ToString() 
                                            == options.LastName);
                }
                return query.ToList();
            }
        }
        public Customer Create(CreateCustomerOptions options)
        {
            if (options == null)
            {
                return null;
            }

            if (string.IsNullOrWhiteSpace(options.Email)
                || string.IsNullOrWhiteSpace(options.VatNumber))
            {
                return null;
            }
            if (!options.Email.Contains("@")
                || options.VatNumber.Length != 9
                || !options.VatNumber.Contains("0123456789"))
            {
                return null;
            }
            var customer = new Customer();

            using (var context = new TinyCrmDbContext())
            {
                customer.VatNumber = options.VatNumber;
                customer.Email = options.Email;
                customer.FirstName = options.FirstName;

                context.Set<Customer>().Add(customer);
                context.SaveChanges();
                return customer;
            }

        }
    }
}

