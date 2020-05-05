using Calm.Dtb;
using Calm.Dtb.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Calm.Lib
{
    class Logic
    {
        public async static Task<User> Login(IOutput output, string username, string password)
        {
            var ret = await output.GetFind<User>(x => x.Username == username && x.Password == password);
            if (ret == null) throw new Exception("404", new Exception("User is not found"));
            return ret;
        }

        public async static Task<UserItem> AddUser(IOutput output, IInput input, UserItem user)
        {
            if (!await UsernameExists(output, user.Username))
            {
                var inUser = user.ToData();
                if (user.IsAdmin)
                {
                    await input.Add(new AdminInfo() { user = inUser });
                }
                else
                {
                    await input.Add(user.ToData());
                }
                return user;
            }
            else
            {
                throw new Exception("400", new Exception("username is taken"));
            }
        }

        public async static Task<bool> UsernameExists(IOutput output, string username)
            => await output.GetFind<User>(x => x.Username == username) != null;

        public async static Task<bool> CheckAdmin(IOutput output, string username, string password)
        {
            await Login(output, username, password);
            return await CheckAdmin(output, username);
        }

        public async static Task<bool> CheckAdmin(IOutput output, string username)
            => await output.GetFind<AdminInfo>(x => x.user.Username == username) != null;
    }
}
