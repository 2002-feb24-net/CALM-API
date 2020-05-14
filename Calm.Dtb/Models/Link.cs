using System.ComponentModel.DataAnnotations;

namespace Calm.Dtb.Models
{
    public class Link
    {
        [Key]
        public int Id { get; set; }
        public int userId { get; set; }
        public int gatheringId { get; set; }
        public User user { get; set; }
        public Gathering gathering { get; set; }
    }
}
