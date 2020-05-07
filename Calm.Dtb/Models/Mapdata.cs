using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Calm.Dtb.Models
{
    public class Mapdata
    {
        [Key]
        public int Id { get; set; }
        public string city { get; set; }
    }
}
