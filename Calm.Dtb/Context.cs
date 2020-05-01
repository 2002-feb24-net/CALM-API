using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;

namespace Calm.Dtb
{
    public class CalmContext : DbContext
    {
        public CalmContext(DbContextOptions options) : base(options) {}

        public DbSet<User> Users { get; set; }
    }
}
