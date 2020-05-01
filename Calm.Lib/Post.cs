using Calm.Dtb;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Calm.Lib
{
    public class Post : IPost
    {
        private IInput Input { get; set; }

        public Post(IInput input)
        {
            this.Input = input;
        }

        public async Task<UserItem> User(UserItem item)
        {
            item.IsAdmin = false;
            item.Id = (await Input.Add(item)).Id;
            return item;
        }

        public async Task<object> AdminUser(string username, string password, UserItem item)
        {
            item.Id = (await Input.Add(item)).Id;
            return item;
        }
    }
}
