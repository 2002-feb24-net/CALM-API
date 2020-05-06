using Calm.Dtb.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Calm.Lib.Items
{
    public class GatheringItemOut
    {
        public GatheringItemOut(Gathering data)
        {
            Title = data.Title;
            occurrenceData = data.occurrenceData;
            details = data.details;
            organizer = new UserItem(data.organizer, true);
            atendees = new List<UserItem>();

            foreach (var item in data.links)
            {
                var user = new UserItem()
                {
                    FName = item.user.FName,
                    LName = item.user.LName,
                    Username = item.user.Username,
                    IsAdmin = false,
                    Password = "the password and isadmin status are both inaccesable in this request"
                };
                atendees.Add(user);
            }
        }
        public string Title { get; set; }
        public string occurrenceData { get; set; }
        public string details { get; set; }
        public UserItem organizer { get; set; }
        public List<UserItem> atendees { get; set; }
    }
}
