using FormAuthentication.Models;
using Microsoft.EntityFrameworkCore;

namespace FormAuthentication.Data
{
    public class AuthDbContext : DbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }
        public DbSet<SignUp> SignUps { get; set; }
    }
}
