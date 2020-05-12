using Xunit;
using Calm.Dtb;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Calm.App;
using Microsoft.Data.Sqlite;
using Calm.Test.Dtb;
using Calm.Test;
using System.Linq;
using Calm.Dtb.Models;

namespace Calm.Dtb.Tests
{
    public class InputTests
    {
        private static CalmContext getContext()
        {
            var ret = new TestContext();

            ret.Database.EnsureCreated();
            ret.Database.Migrate();
            Seeder.Seed(ret);
            ret.SaveChanges();

            return ret;
        }

        [Fact]
        public void AddTest()
        {
            var context = getContext();
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
        public void SetTest()
        {
            var context = getContext();
            IInput input = new Input(context);
            var user1 = new User()
            {
                Username = "testUser",
                FName = "Test",
                LName = "User",
                Password = "jim"
            };
            var user2 = new User()
            {
                Username = "testUser2",
                FName = "Test2",
                LName = "User2",
                Password = "jim2"
            };

            var ent = context.Users.Add(user1);
            var obj = ent.Entity;
            user2.Id = obj.Id;

            context.SaveChanges();

            var x = context.Users.ToArray();

            input.Set(user2, user2.Id);

            context.SaveChanges();

            var y = context.Users.ToArray();

            List<User> output = new List<User>(from item in context.Users.AsEnumerable()
                                               where
                                               (
                                               item.FName == user1.FName &&
                                               item.LName == user1.LName &&
                                               item.Username == user1.Username &&
                                               item.Password == user1.Password
                                               )
                                               select item);

            Assert.True(output.Count() == 0);

            output = new List<User>(from item in context.Users.AsEnumerable()
                                    where
                                    (
                                    item.FName == user2.FName &&
                                    item.LName == user2.LName &&
                                    item.Username == user2.Username &&
                                    item.Password == user2.Password
                                    )
                                    select item);

            Assert.True(output.Count() == 1);
        }

        [Fact]
        public void RemoveTest()
        {

        }
    }
}