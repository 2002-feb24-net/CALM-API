using Calm.Dtb;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using System.Data.SQLite;

namespace Calm.Test.Dtb
{
    class TestContext : CalmContext
    {
        public TestContext()
            : base(
                  new DbContextOptionsBuilder<CalmContext>()
                  .UseSqlite(CreateInMemoryDatabase())
                  .Options)
        { }

        private static DbConnection CreateInMemoryDatabase()
        {
            var connection = new SQLiteConnection("Data Source=:memory:");

            connection.Open();

            return connection;
        }
    }
}
