using Calm.Dtb;
using Calm.Dtb.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Calm.App
{
    public static class Seeder
    {
        public static void Seed(CalmContext context)
        {
            context.Links.RemoveRange(context.Links);
            context.Admins.RemoveRange(context.Admins);
            context.Users.RemoveRange(context.Users);
            context.Gatherings.RemoveRange(context.Gatherings);
            context.Citys.RemoveRange(context.Citys);
            context.SaveChanges();

            Func<string, int> CityId = x =>
            {
                return (from item in context.Citys.ToList()
                        where item.city.Contains(x)
                        select item.Id).FirstOrDefault();
            };
            Func<string, int> UserId = x =>
            {
                return (from item in context.Users.ToList()
                        where item.FName.Contains(x)
                        select item.Id).FirstOrDefault();
            };
            Func<string, string, string, string, bool, string, string, Gathering> gather = 
                (title, details, occurance, map, isEvent, u1, u2) =>
            new Gathering()
            {
                Title = title,
                details = details,
                occurrenceData = occurance,
                organizerId = context.Admins.FirstOrDefault().user.Id,
                MapDataId = CityId(map),
                isEvent = isEvent,
                links = new List<Link>
                {
                    new Link { userId = UserId(u1) },
                    new Link { userId = UserId(u2) }
                }
            };

            context.Citys.AddRange(
                new Mapdata() { city = "Houston, TX" },
                new Mapdata() { city = "Dallas, TX" },
                new Mapdata() { city = "Nashville, TN" },
                new Mapdata() { city = "Minneapolis, MN" },
                new Mapdata() { city = "Kansas City, MI" },
                new Mapdata() { city = "New York, NY" },
                new Mapdata() { city = "Los Angeles, CA" },
                new Mapdata() { city = "Chicago, IL" },
                new Mapdata() { city = "Seattle, WA" },
                new Mapdata() { city = "Atlanta, GA" },
                new Mapdata() { city = "Orlando, FL" },
                new Mapdata() { city = "Washington DC," },
                new Mapdata() { city = "New Orleans, LA" },
                new Mapdata() { city = "Denver, CO" },
                new Mapdata() { city = "San Francisco, CA" },
                new Mapdata() { city = "Detroit, MI" },
                new Mapdata() { city = "Online" });

            context.SaveChanges();

            context.Admins.AddRange(new AdminInfo() { SuperAdmin = true,
                user = new User() { FName = "admin", LName = "user", Username = "admin", Password = "admin", MapDataId = CityId("Houston") } });

            context.Users.AddRange(
                new User() { FName = "Samuel", LName = "Philander", Username = "SamP", Password = "DinoKid", MapDataId = CityId("Dallas") },
                new User() { FName = "Andrew", LName = "Jackson", Username = "AJ", Password = "JackieChan", MapDataId = CityId("Houston") },
                new User() { FName = "Ulysses", LName = "Grant", Username = "Gr4nt", Password = "Password", MapDataId = CityId("Dallas") },
                new User() { FName = "Rutherford", LName = "Hayes", Username = "Ruth", Password = "Hays", MapDataId = CityId("Houston") },
                new User() { FName = "James", LName = "Garfield", Username = "Lasagna", Password = "JohnArbuckle", MapDataId = CityId("Houston") },
                new User() { FName = "Chester", LName = "Aurthur", Username = "CCheetah", Password = "AntE4ter", MapDataId = CityId("Dallas") },
                new User() { FName = "Grover", LName = "Cleveland", Username = "GroveLand", Password = "ClevelandBrown", MapDataId = CityId("Dallas") },
                new User() { FName = "Benjamin", LName = "Harrison", Username = "BenTen", Password = "Omnitrix", MapDataId = CityId("Dallas") },
                new User() { FName = "Theadore", LName = "Roosevelt", Username = "Nite@Musem", Password = "BuildABear", MapDataId = CityId("Houston") },
                new User() { FName = "William", LName = "McKinley", Username = "Will", Password = "McDonnies", MapDataId = CityId("Houston") });

            context.SaveChanges();

            context.Gatherings.AddRange(
                gather(
                    "Marvin’s Room: with guest Drake",
                    "Location: TBA",
                    "June 12th",
                    "Houston",
                    true,
                    "Samuel",
                    "Andrew"),
                gather(
                    "Love and Happiness: with guest Kevin Love",
                    "",
                    "June 12th",
                    "Online",
                    true,
                    "Ulysses",
                    "Samuel"),
                gather(
                    "Struggles of Balance: guest Demi Lovato",
                    "",
                    "August 1st",
                    "Online",
                    true,
                    "Ulysses",
                    "Andrew"),
                gather(
                    "Generalized Anxiety",
                    "",
                    "Monday - Friday",
                    "Dallas",
                    false,
                    "Samuel",
                    "Rutherford"),
                gather(
                    "Post Traumatic Anxiety Disorder",
                    "",
                    "Monday - Friday",
                    "Dallas",
                    false,
                    "Grover",
                    "Chester"),
                gather(
                    "Social Anxiety",
                    "",
                    "Monday - Friday",
                    "Online",
                    false,
                    "James",
                    "Ulysses"),
                gather(
                    "Panic Disorder",
                    "",
                    "Monday - Friday",
                    "Dallas",
                    false,
                    "Samuel",
                    "James"),
                gather(
                    "Obsessive Compulsive Disorder",
                    "",
                    "Monday - Friday",
                    "Online",
                    false,
                    "Theadore",
                    "William"),
                gather(
                    "Specific phobias",
                    "",
                    "Monday - Friday",
                    "Dallas",
                    false,
                    "Samuel",
                    "Benjamin"));
            
            context.SaveChanges();
        }
    }
}
