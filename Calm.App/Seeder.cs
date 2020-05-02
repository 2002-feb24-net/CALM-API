using Calm.Dtb;
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
                context.Users.AddRange(
                    new User() { FName = "admin", LName = "user", Username = "admin", Password = "admin", IsAdmin = true },
                    new User() { FName = "timmy", LName = "jimmy", Username = "TimmyJ", Password = "TimTim", IsAdmin = true });
            }

            context.SaveChanges();
        }
    }
}
