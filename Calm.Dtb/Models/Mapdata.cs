using System.ComponentModel.DataAnnotations;

namespace Calm.Dtb.Models
{
    public class Mapdata
    {
        [Key]
        public int Id { get; set; }
        public string city { get; set; }
    }
}
