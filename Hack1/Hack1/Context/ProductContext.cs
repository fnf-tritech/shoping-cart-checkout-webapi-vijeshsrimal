using Hack1.Models;
using Microsoft.EntityFrameworkCore;

namespace Hack1.Context
{
    public class ProductContext:DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        {

        }
        public DbSet<Product> Product { get; set; } = null!;
    }
}
