using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phase3.Models
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {

        }
        public virtual DbSet<CustomerModel> Customers { get; set; }
        public virtual DbSet<SellerModel> Sellers { get; set; }
        public virtual DbSet<LaptopModel> Laptops { get; set; }
    }
}
