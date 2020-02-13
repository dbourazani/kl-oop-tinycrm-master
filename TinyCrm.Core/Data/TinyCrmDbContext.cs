﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace TinyCrm.Core.Data
{
    public class TinyCrmDbContext : DbContext
    {
        private readonly string connectionString_;
        public TinyCrmDbContext()
        {
            connectionString_ =
                "Server =localhost; Database =tinycrm_acc; " +
                "Integrated Security=SSPI;Persist Security Info=False;";
        }

        public TinyCrmDbContext(string connString)
        {
            connectionString_ = connString;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)//override:prosparnaei thn 
                                                                          //leitoyrghkothat poy hdh exei kai kanei auto pou ths lev
                                                                          //gia na ths dvsv to diko mou string h na ths pv pvw
                                                                          //uelw na ftiaxtoun oi pinakes
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .Entity<Model.Customer>()
                .ToTable("Customer", "core");//namespace
            modelBuilder
                 .Entity<Model.Customer>()
                 .HasIndex(c => c.VatNumber)//kathgoriopoiw tous customers bash to vatnumber. to kanei kai gia to id bydefault   
                 .IsUnique();             //to kanei monadiko, san dikleida asfaleias
            modelBuilder
                .Entity<Model.Customer>()
                .Property(c => c.VatNumber)
                .HasMaxLength(9)
                .IsFixedLength();

            modelBuilder
                .Entity<Model.Order>()
                .ToTable("Order", "core");
            
            modelBuilder
                .Entity<Model.OrderProduct>()
                .ToTable("OrderProduct", "core");

            modelBuilder
                .Entity<Model.OrderProduct>()
                .HasKey(op => new { op.OrderId, op.ProductId});   

            modelBuilder
               .Entity<Model.Product>()
               .ToTable("Product", "core");
        }

      

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)//syndeomai meton sql server
        {
            optionsBuilder.UseSqlServer(connectionString_);
        }
    }
}
