using Calm.Dtb.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Calm.Lib.Items
{
    public class GatheringItemIn
    {
        public Gathering ToData(AdminInfo user)
        {
            return new Gathering()
            {
                Title = Title,
                occurrenceData = occurrenceData,
                details = details,
                organizerId = user.id
            };
        }

        public string Title { get; set; }
        public string occurrenceData { get; set; }
        public string details { get; set; }
    }
}
