using Calm.Dtb.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Calm.Dtb
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        public string FName { get; set; }

        [MaxLength(50)]
        public string LName { get; set; }

        [MaxLength(50)]
        public string Username { get; set; }

        [MaxLength(50)]
        public string Password { get; set; }

        public int MapDataId { get; set; }

        public Mapdata MapData { get; set; }

        public List<Link> links { get; set; }
    }
}