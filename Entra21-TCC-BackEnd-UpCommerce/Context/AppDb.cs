using Entra21_TCC_BackEnd_UpCommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace Entra21_TCC_BackEnd_UpCommerce.Context
{
    public class AppDb : DbContext
    {
        public AppDb(DbContextOptions<AppDb> options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
