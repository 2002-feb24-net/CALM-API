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
                new Gathering()
                {
                    Title = "Marvin’s Room: with guest Drake",
                    details = "Location: TBA",
                    occurrenceData = "October 15th",
                    organizerId = context.Admins.FirstOrDefault().user.Id,
                    MapDataId = CityId("Houston"),
                    isEvent = true,
                    links = new List<Link>
                    {
                        new Link { userId = UserId("Samuel") },
                        new Link { userId = UserId("Andrew") }
                    }
                }, new Gathering()
                {
                    Title = "Love and Happiness: with guest Kevin Love",
                    details = "",
                    occurrenceData = "June 12th",
                    organizerId = context.Admins.FirstOrDefault().user.Id,
                    MapDataId = CityId("Online"),
                    isEvent = true,
                    links = new List<Link>
                    {
                        new Link { userId = UserId("Ulysses") },
                        new Link { userId = UserId("Samuel") }
                    }
                }, new Gathering()
                {
                    Title = "Struggles of Balance: guest Demi Lovato",
                    details = "",
                    occurrenceData = "August 1st",
                    organizerId = context.Admins.FirstOrDefault().user.Id,
                    MapDataId = CityId("Online"),
                    isEvent = true,
                    links = new List<Link>
                    {
                        new Link { userId = UserId("Ulysses") },
                        new Link { userId = UserId("Andrew") }
                    }
                }, new Gathering()
                {
                    Title = "Generalized Anxiety",
                    details = "",
                    occurrenceData = "Monday - Friday",
                    organizerId = context.Admins.FirstOrDefault().user.Id,
                    MapDataId = CityId("Dallas"),
                    isEvent = false,
                    links = new List<Link>
                    {
                        new Link { userId = UserId("Samuel") },
                        new Link { userId = UserId("Rutherford") }
                    }
                }, new Gathering()
                {
                    Title = "Post Traumatic Anxiety Disorder",
                    details = "",
                    occurrenceData = "Monday - Friday",
                    organizerId = context.Admins.FirstOrDefault().user.Id,
                    MapDataId = CityId("Dallas"),
                    isEvent = false,
                    links = new List<Link>
                    {
                        new Link { userId = UserId("Grover") },
                        new Link { userId = UserId("Chester") }
                    }
                }, new Gathering()
                {
                    Title = "Social Anxiety",
                    details = "",
                    occurrenceData = "Monday - Friday",
                    organizerId = context.Admins.FirstOrDefault().user.Id,
                    MapDataId = CityId("Online"),
                    isEvent = false,
                    links = new List<Link>
                    {
                        new Link { userId = UserId("James") },
                        new Link { userId = UserId("Ulysses") }
                    }
                }, new Gathering()
                {
                    Title = "Panic Disorder",
                    details = "",
                    occurrenceData = "Monday - Friday",
                    organizerId = context.Admins.FirstOrDefault().user.Id,
                    MapDataId = CityId("Dallas"),
                    isEvent = false,
                    links = new List<Link>
                    {
                        new Link { userId = UserId("Samuel") },
                        new Link { userId = UserId("James") }
                    }
                }, new Gathering()
                {
                    Title = "Obsessive Compulsive Disorder",
                    details = "",
                    occurrenceData = "Monday - Friday",
                    organizerId = context.Admins.FirstOrDefault().user.Id,
                    MapDataId = CityId("Online"),
                    isEvent = false,
                    links = new List<Link>
                    {
                        new Link { userId = UserId("Theadore") },
                        new Link { userId = UserId("William") }
                    }
                }, new Gathering()
                {
                    Title = "Specific phobias",
                    details = "",
                    occurrenceData = "Monday - Friday",
                    organizerId = context.Admins.FirstOrDefault().user.Id,
                    MapDataId = CityId("Dallas"),
                    isEvent = false,
                    links = new List<Link>
                    {
                        new Link { userId = UserId("Samuel") },
                        new Link { userId = UserId("Benjamin") }
                    }
                });
            
            context.SaveChanges();
        }
    }
}
