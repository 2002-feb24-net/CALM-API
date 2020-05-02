using Calm.Dtb;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Calm.Lib
{
    public class Post : IPost
    {
        private IOutput Output { get; set; }
        private IInput Input { get; set; }

        public Post(IOutput output, IInput input)
        {
            Output = output;
            Input = input;
        }

        public async Task<UserItem> User(UserItem item)
        {
            item.IsAdmin = false;
            item.Id = (await Input.Add(item.ToData())).Id;
            return item;
        }

        public async Task<object> AdminUser(string username, string password, UserItem item)
        {
            if ((await Logic.Login(Output, username, password)).IsAdmin)
            {
                item.Id = (await Input.Add(item.ToData())).Id;
                return item;
            }
            else
            {
                throw new Exception("404",new Exception("User is not an admin"));
            }
        }
    }
}
