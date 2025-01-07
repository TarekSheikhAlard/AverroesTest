﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Data.BaseDbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Order.Data.DbContext
{
    public class OrderDbContext : BaseDbContext<OrderDbContext>
    {
        private readonly IConfiguration _configuration;

        public OrderDbContext(DbContextOptions<OrderDbContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }
        protected override string GetSchemaName()
        {
            return Assembly.GetExecutingAssembly().GetName().Name.Split('.')[0].ToUpper();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(GetSchemaName());

        }
        public DbSet<Domain.Models.Order> Orders { get; set; }
    }
}