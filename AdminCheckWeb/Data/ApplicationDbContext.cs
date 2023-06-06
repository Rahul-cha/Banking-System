using AdminCheckWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace AdminCheckWeb.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<UserDetails> UserDetails { get; set; }

        public DbSet<Login> logins { get; set; }    
    }

}
