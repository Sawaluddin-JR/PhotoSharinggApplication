using Microsoft.EntityFrameworkCore;
using PhotoSharinggApplication.Models;

namespace PhotoSharinggApplication.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<SharePhoto> SharePhoto { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Status> Statuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // isi tabel roles
            modelBuilder.Entity<Roles>().HasData(new Roles[]
            {
                new Roles
                {
                    Id = "1",
                    Name = "Admin"
                },
                new Roles
                {
                    Id = "2",
                    Name ="User"
                }
            });
            // isi tabel status
            modelBuilder.Entity<Status>().HasData(new Status[]
            {
                new Status
                {
                    Id = "1",
                    Name = "Published"
                },
                new Status
                {
                    Id = "2",
                    Name ="NotPublished"
                }
            });
        }
    }
}
