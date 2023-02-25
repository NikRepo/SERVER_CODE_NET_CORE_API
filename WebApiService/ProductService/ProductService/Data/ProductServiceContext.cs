using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CommonServiceHelper.Service_Model;

namespace ProductService.Data
{
    public class ProductServiceContext : DbContext
    {
        public ProductServiceContext (DbContextOptions<ProductServiceContext> options)
            : base(options)
        {
        }

        public DbSet<CommonServiceHelper.Service_Model.Order> Orders { get; set; } = default!;
        public DbSet<Contact> Contacts { get; set; }
    }
}
