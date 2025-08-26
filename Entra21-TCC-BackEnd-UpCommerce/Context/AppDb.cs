using Entra21_TCC_BackEnd_UpCommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace Entra21_TCC_BackEnd_UpCommerce.Context
{
    public class AppDb : DbContext
    {
        public AppDb(DbContextOptions<AppDb> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Project>(entity =>
            {
                entity.HasKey(p => p.Id);

                entity.Property(p => p.Title)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(p => p.SubTitle)
                    .HasMaxLength(300);

                entity.Property(p => p.Description)
                    .HasMaxLength(2000);

                entity.Property(p => p.UrlLogo)
                    .HasMaxLength(3000);

                entity.Property(p => p.ComponentJson)
                    .HasColumnType("TEXT");
            });
        }
    }
}
