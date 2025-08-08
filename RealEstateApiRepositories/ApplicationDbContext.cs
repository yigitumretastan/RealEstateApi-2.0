using Microsoft.EntityFrameworkCore;
using RealEstateApiEntity.Models;

namespace RealEstateApiRepositories
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public new virtual Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(cancellationToken);
        }

        public DbSet<User> User { get; set; }
        public DbSet<Listing> Listing { get; set; }
        public DbSet<Payment> Payment { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Listing>()
                          .Property(p => p.Price)
                          .HasPrecision(18, 2);

            modelBuilder.Entity<Payment>()
                          .Property(p => p.Price)
                          .HasPrecision(18, 2);
        }
    }
}