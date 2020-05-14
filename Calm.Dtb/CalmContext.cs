using Calm.Dtb.Models;
using Microsoft.EntityFrameworkCore;

namespace Calm.Dtb
{
    public class CalmContext : DbContext
    {
        public CalmContext(DbContextOptions<CalmContext> options) : base(options) {}

        public DbSet<User> Users { get; set; }
        public DbSet<AdminInfo> Admins { get; set; }
        public DbSet<Gathering> Gatherings { get; set; }
        public DbSet<Link> Links { get; set; }
        public DbSet<Mapdata> Citys { get; set; }
    }
}
