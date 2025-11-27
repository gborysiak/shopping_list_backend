using dotnet_api.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace dotnet_api.Data
{
    //public class ApiDbContext : DbContext
    public class ApiDbContext : IdentityDbContext<User, Role, int>
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {

        }

        public DbSet<Part> Parts { get; set; }
        public DbSet<ShoppingList> ShoppingLists { get; set; }
        public DbSet<Category> Categories { get; set; }

        public DbSet<ShoppingListItem> ShoppingListItem { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ShoppingList>()
                .HasMany(e => e.ShoppingListItem)
                .WithOne()
                .HasForeignKey("ShoppingListId");

        }
    }

}