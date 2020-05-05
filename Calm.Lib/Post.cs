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
            if (item.IsAdmin) throw new Exception("403", new Exception(
                "cannot add an admin without existing admin credentials," +
                " use \"api/admin/{username}/{password} with the same body\""));

            return await Logic.AddUser(Output, Input, item);
        }

        public async Task<object> AdminUser(string username, string password, UserItem item)
        {
            if (await Logic.CheckAdmin(Output, username, password))
            {
                return await Logic.AddUser(Output, Input, item);
            }
            else
            {
                throw new Exception("404",new Exception("User is not an admin"));
            }
        }
    }
}
