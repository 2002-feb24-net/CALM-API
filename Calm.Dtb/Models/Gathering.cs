using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Calm.Dtb.Models
{
    class Gathering
    {
        [Key]
        public int id { get; set; }
        public string Title { get; set; }
        public string occurrenceData { get; set; }
        public string details { get; set; }
        public User organizer { get; set; }
        public List<User> atendees { get; set; }
    }
}
