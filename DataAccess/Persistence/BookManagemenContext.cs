using Core.Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Persistence
{
    public class BookManagemenContext : DbContext
    {
        public BookManagemenContext(DbContextOptions<BookManagemenContext> options)
        : base(options)
        {
            
        }

        public DbSet<Claim> Claims { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RoleClaim> RoleClaims { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserClaim> UserClaims { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

    }
}
