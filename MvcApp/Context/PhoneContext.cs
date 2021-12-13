using Microsoft.EntityFrameworkCore;
using MvcApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcApp.Context
{
    public class PhoneContext:DbContext
    {
        public DbSet<Phone> Phones { get; set; }
        public DbSet<Order> Orders { get; set; }
        public PhoneContext(DbContextOptions<PhoneContext> options):base (options)
        {
            Database.EnsureCreated();
        }
    }
}
