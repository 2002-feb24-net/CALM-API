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

        public bool IsAdmin { get; set; }
    }
}