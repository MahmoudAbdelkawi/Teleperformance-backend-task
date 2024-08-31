using Authentication_CRUD_Operation.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Authentication_CRUD_Operation.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Employee> Employees { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); // for configuration
            builder.Entity<IdentityRole>().HasData(
                       new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" },
                       new IdentityRole { Name = "User", NormalizedName = "USER" }
                       );
        }
    }
}
