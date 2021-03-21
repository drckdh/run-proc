using Microsoft.EntityFrameworkCore;

namespace ExampleApi.Models
{
    public class ExampleContext : DbContext
    {
        public ExampleContext(DbContextOptions<ExampleContext> options)
            : base(options)
        {
        }

        public DbSet<ExampleItem> ExampleItems { get; set; }
    }
}