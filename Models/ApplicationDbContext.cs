using Microsoft.EntityFrameworkCore;

namespace WebApi.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employee { get; set; }
        public DbSet<Employee> Sp_InsertEmployee { get; set; }
    }
}
