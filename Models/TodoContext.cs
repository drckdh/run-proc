using Microsoft.EntityFrameworkCore;

namespace RunProcApi.Models
{
    public class RunProcContext : DbContext
    {
        public RunProcContext(DbContextOptions<RunProcContext> options)
            : base(options)
        {
        }

        public DbSet<RunProcItem> RunProcItems { get; set; }
    }
}