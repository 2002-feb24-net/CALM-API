using Microsoft.EntityFrameworkCore;
using System;

namespace Calm.Dtb
{
    public class CalmContext : DbContext
    {
        private string access;  
        public CalmContext(DbContextOptions<CalmContext> options)
            : base(options)
        {
        }

        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {
        //     if (!optionsBuilder.IsConfigured)
        //     {
        //         optionsBuilder.UseNpgsql(access);
        //     }
        // }

        public DbSet<User> Users { get; set; }
    }
}
