using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Calm.Dtb.Models
{
    public class AdminInfo
    {
        [Key]
        public int id { get; set; }

        public bool SuperAdmin { get; set; }

        public int userId { get; set; }

        public User user { get; set; }
    }
}
