using Calm.Dtb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
                organizerId = user.id,
                isEvent = isEvent
            };
        }

        [Required]
        public string City { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string occurrenceData { get; set; }
        public string details { get; set; }
        public bool isEvent { get; set; }
    }
}
