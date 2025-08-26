using Entra21_TCC_BackEnd_UpCommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace Entra21_TCC_BackEnd_UpCommerce.Context
{
    public class AppDb : DbContext
    {
        public AppDb(DbContextOptions<AppDb> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Cdk> Cdks { get; set; }
        public DbSet<Style> Styles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Project → Cdk
            modelBuilder.Entity<Project>()
                .HasMany(p => p.Component)
                .WithOne(c => c.Project)
                .HasForeignKey(c => c.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            // Cdk → Children recursivo
            modelBuilder.Entity<Cdk>()
                .HasMany(c => c.Children)
                .WithOne(c => c.Parent)
                .HasForeignKey(c => c.ParentCdkId)
                .OnDelete(DeleteBehavior.Cascade);

            // Cdk → Style (1:1)
            modelBuilder.Entity<Cdk>()
                .HasOne(c => c.Style)
                .WithOne(s => s.Cdk)
                .HasForeignKey<Style>(s => s.CdkId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configurar Ids varchar(255)
            modelBuilder.Entity<Cdk>()
                .Property(c => c.Id)
                .HasColumnType("varchar(255)");
        }
    }
}