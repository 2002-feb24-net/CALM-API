using Calm.Dtb;
using Calm.Dtb.Models;
using Microsoft.EntityFrameworkCore;
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
            context.Links.RemoveRange(context.Links);
            context.Admins.RemoveRange(context.Admins);
            context.Users.RemoveRange(context.Users);
            context.Gatherings.RemoveRange(context.Gatherings);
            context.Citys.RemoveRange(context.Citys);
            context.SaveChanges();

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

            context.SaveChanges();

            List<int> maps = new List<int>(from item in context.Citys.ToList() select item.Id);

            context.Admins.AddRange(new AdminInfo() { SuperAdmin = true,
                user = new User() { FName = "admin", LName = "user", Username = "admin", Password = "admin", MapDataId = maps[0] } });

            context.Users.AddRange(
                new User() { FName = "Jimmy", LName = "Timmy", Username = "JimmyT", Password = "JimJim", MapDataId = maps[0] },
                new User() { FName = "timmy", LName = "jimmy", Username = "TimmyJ", Password = "TimTim", MapDataId = maps[2] });

            context.SaveChanges();

            context.Gatherings.Add(
                new Gathering()
                {
                    Title = "first event",
                    details = "this is an event",
                    occurrenceData = "the event happens st this time, or occors at  this regular interval",
                    organizerId = context.Admins.FirstOrDefault().user.Id,
                    MapDataId = maps[0]
                });

            context.SaveChanges();

            List<int> users = new List<int>(from item in context.Users.ToList() select item.Id);

            context.Links.AddRange(
                new Link() { userId = users[1], gatheringId = context.Gatherings.FirstOrDefault().id },
                new Link() { userId = users[2], gatheringId = context.Gatherings.FirstOrDefault().id });
            

            context.SaveChanges();
        }
    }
}
