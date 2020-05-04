using Calm.Dtb;
using Calm.Dtb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Calm.App
{
    public class Seeder
    {
        public static void Seed(CalmContext context)
        {
            if (context.Users.Count() == 0)
            {
                context.Admins.AddRange(new AdminInfo() { SuperAdmin = true,
                    user = new User() { FName = "admin", LName = "user", Username = "admin", Password = "admin" } });

                context.Users.AddRange(
                    new User() { FName = "Jimmy", LName = "Timmy", Username = "JimmyT", Password = "JimJim" },
                    new User() { FName = "timmy", LName = "jimmy", Username = "TimmyJ", Password = "TimTim" });
            }

            context.SaveChanges();
        }
    }
}
