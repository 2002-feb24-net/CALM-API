using Calm.Lib;
using Calm.Lib.Items;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;

namespace Calm.Test
{
    [HelpKeyword]
    class Logic
    {
        internal static bool UserItemEquality(UserItem x, UserItem y)
        {
            return
                x.IsAdmin == y.IsAdmin &&
                x.FName == y.FName &&
                x.LName == y.LName &&
                x.Username == y.Username &&
                x.Password == y.Password &&
                x.City == y.City;
        }

        internal static bool GatheringItemInEquality(GatheringItemIn x, GatheringItemIn y)
        {
            return
                x.City == y.City &&
                x.details == y.details &&
                x.occurrenceData == y.details &&
                x.Title == y.Title;
        }

        internal static bool GatheringItemOutEquality(GatheringItemOut x, GatheringItemOut y)
        {
            return
                x.City == y.City &&
                x.Title == y.Title &&
                x.details == y.details &&
                x.occurrenceData == y.occurrenceData &&
                UserItemEquality(x.organizer, y.organizer) &&
                CompareList(x.atendees, y.atendees, UserItemEquality);
        }

        internal static bool CompareList<T>(List<T> listA, List<T> listB, Func<T,T,bool> check)
        {
            if (listA.Count() != listB.Count()) return false;
            for (int i = 0; i < listB.Count(); i++)
            {
                if (!check(listA[i], listB[i]))
                {
                    return false;
                }
            }
            return true;
        }

        internal static UserItem BlankUserItem()
        {
            return new UserItem()
            {
                IsAdmin = false,
                Username = "",
                Password = "",
                FName = "",
                LName = "",
                City = ""
            };
        }

        internal static GatheringItemIn BlankGatheringItemIn()
        {
            return new GatheringItemIn()
            {
                City = "",
                Title = "",
                occurrenceData = "",
                details = ""
            };
        }

        internal static GatheringItemOut BlankGatheringItemOut()
        {
            return new GatheringItemOut()
            {
                City = "",
                Title = "",
                details = "",
                occurrenceData = "",
                organizer = BlankUserItem(),
                atendees = new List<UserItem>() { BlankUserItem() }
            };
        }
    }
}
