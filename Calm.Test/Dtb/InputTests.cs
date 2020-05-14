using Xunit;
using Microsoft.EntityFrameworkCore;
using Calm.App;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Calm.Dtb.Tests
{
    public class InputTests
    {
        public System.Data.SQLite.SQLiteConnection connection { get; set; }

        private void OpenContext()
        {
            connection = new System.Data.SQLite.SQLiteConnection("Data Source=:memory:");

            connection.Open();
            
            var temp = new CalmContext(
                new DbContextOptionsBuilder<CalmContext>()
                .UseSqlite(connection)
                .Options);

            temp.Database.Migrate();
            Seeder.Seed(temp);
            temp.SaveChanges();

            temp.Dispose();
        }

        private CalmContext GetContext()
        {
            if (connection == null) OpenContext();

            return new CalmContext(
                new DbContextOptionsBuilder<CalmContext>()
                .UseSqlite(connection)
                .Options);
        }

        [Fact]
        public void AddTest()
        {
            var context = GetContext();
            IInput input = new Input(context);
            var inUser = new User()
            {
                Username = "testUser",
                FName = "Test",
                LName = "User",
                Password = "jim"
            };

            input.Add(inUser);

            var output = from item in context.Users.AsEnumerable()
                            where
                            (
                            item.FName == inUser.FName &&
                            item.LName == inUser.LName &&
                            item.Username == inUser.Username &&
                            item.Password == inUser.Password
                            )
                            select item;

            Assert.True(output.Count() == 1);
        }

        [Fact]
        public async void SetTest()
        {
            var user1 = new User()
            {
                Username = "testUser",
                FName = "Test",
                LName = "User",
                Password = "jim"
            };

            EntityEntry<User> temp;

            using (var context = GetContext())
            {
                temp = context.Users.Add(user1);
                await context.SaveChangesAsync();
            }

            var user2 = new User()
            {
                Username = "testUser2",
                FName = "Test2",
                LName = "User2",
                Password = "jim2",
                Id = temp.Entity.Id
            };

            using (var context = GetContext())
            {
                IInput test = new Input(context);
                await test.Set(user2, user2.Id);
            }

            List<User> output;

            using (var context = GetContext())
            {
                output = new List<User>(from item in context.Users.AsEnumerable()
                                        where
                                        (
                                        item.FName == user1.FName &&
                                        item.LName == user1.LName &&
                                        item.Username == user1.Username &&
                                        item.Password == user1.Password
                                        )
                                        select item);
            }

            Assert.True(output.Count() == 0);

            using (var context = GetContext())
            {
                output = new List<User>(from item in context.Users.AsEnumerable()
                                        where
                                        (
                                        item.FName == user2.FName &&
                                        item.LName == user2.LName &&
                                        item.Username == user2.Username &&
                                        item.Password == user2.Password
                                        )
                                        select item);
            }
            

            Assert.True(output.Count() == 1);
        }

        [Fact]
        public void RemoveTest()
        {

        }
    }
}