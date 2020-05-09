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
                context.Citys.AddRange(
                    new Mapdata() { city = "Houston" },
                    new Mapdata() { city = "San Antonio" },
                    new Mapdata() { city = "Dallas" },
                    new Mapdata() { city = "Austin" },
                    new Mapdata() { city = "Fort Worth" },
                    new Mapdata() { city = "El Paso" },
                    new Mapdata() { city = "Round Rock" },
                    new Mapdata() { city = "Arlington" },
                    new Mapdata() { city = "Alberta" },
                    new Mapdata() { city = "Yukon" });
                
                context.Admins.AddRange(new AdminInfo() { SuperAdmin = true,
                    user = new User() { FName = "admin", LName = "user", Username = "admin", Password = "admin", MapDataId = 1 } });

                context.Users.AddRange(
                    new User() { FName = "Jimmy", LName = "Timmy", Username = "JimmyT", Password = "JimJim", MapDataId = 1 },
                    new User() { FName = "timmy", LName = "jimmy", Username = "TimmyJ", Password = "TimTim", MapDataId = 3 });

                context.SaveChanges();

                context.Gatherings.Add(
                    new Gathering()
                    {
                        Title = "first event",
                        details = "this is an event",
                        occurrenceData = "the event happens st this time, or occors at  this regular interval",
                        organizerId = context.Admins.FirstOrDefault().user.Id,
                        MapDataId = 1,
                        links = new List<Link>() {}
                    });
            }

            context.SaveChanges();
        }
    }
}
