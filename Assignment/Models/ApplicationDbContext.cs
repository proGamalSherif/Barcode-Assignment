using Microsoft.EntityFrameworkCore;

namespace Assignment.Models
{
    public class ApplicationDbContext:DbContext
    {
        public DbSet<ContentData> tbl_ContentData { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
