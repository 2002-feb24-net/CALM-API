using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Calm.Dtb.Models
{
    public class Gathering
    {
        [Key]
        public int id { get; set; }
        public string Title { get; set; }
        public string occurrenceData { get; set; }
        public string details { get; set; }
        public int organizerId { get; set; }
        public User organizer { get; set; }
        public int MapDataId { get; set; }
        public Mapdata MapData { get; set; }
        public List<Link> links { get; set; }
    }
}
