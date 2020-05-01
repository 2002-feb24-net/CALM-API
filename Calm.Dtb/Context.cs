using Microsoft.EntityFrameworkCore;
using System;

namespace Calm.Dtb
{
    public class CalmContext : DbContext
    {
        private string access;
        public CalmContext(string access)
        {
            this.access = access;
            Database.Migrate();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(access);
            }
        }

        public DbSet<User> Users { get; set; }
    }
}
