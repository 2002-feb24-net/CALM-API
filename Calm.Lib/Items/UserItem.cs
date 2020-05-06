using Calm.Dtb;
using System.ComponentModel.DataAnnotations;

namespace Calm.Lib
{
    public class UserItem
    {
        public UserItem() { }

        public UserItem(User item, bool IsAdmin)
        {
            FName = item.FName;
            LName = item.LName;
            Username = item.Username;
            Password = item.Password;
            this.IsAdmin = IsAdmin;
        }

        public User ToData()
        {
            return new User()
            {
                FName = this.FName,
                LName = this.LName,
                Username = this.Username,
                Password = this.Password
            };
        }

        [Required]
        public string FName { get; set; }

        public string LName { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public bool IsAdmin { get; set; }
    }
}