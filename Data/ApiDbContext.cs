using Microsoft.EntityFrameworkCore;
using dotnet_api.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace dotnet_api.Data
{
    //public class ApiDbContext : DbContext
    public class ApiDbContext: IdentityDbContext<User,Role,int>
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {

        }

        public DbSet<Part> Parts { get; set; }

    }
}
