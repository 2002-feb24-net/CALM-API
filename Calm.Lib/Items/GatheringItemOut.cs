﻿using Calm.Dtb.Models;
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

            foreach (var item in data.atendees)
            {
                var user = new UserItem()
                {
                    FName = item.FName,
                    LName = item.LName,
                    Username = item.Username,
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