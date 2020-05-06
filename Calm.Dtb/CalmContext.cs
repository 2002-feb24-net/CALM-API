using Calm.Dtb.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;

namespace Calm.Dtb
{
    public class CalmContext : DbContext
    {
        public CalmContext(DbContextOptions options) : base(options) {}

        public DbSet<User> Users { get; set; }
        public DbSet<AdminInfo> Admins { get; set; }
        public DbSet<Gathering> Gatherings { get; set; }
        public DbSet<Link> links { get; set; }
    }
}
