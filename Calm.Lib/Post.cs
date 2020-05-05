using Calm.Dtb;
using Calm.Dtb.Models;
using Calm.Lib.Items;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
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

        public async Task AddGathering(string username, string password, GatheringItemIn gathering)
        {
            if (!await Logic.CheckAdmin(Output, username, password))
            {
                throw new Exception("400", new Exception("this request must be made with admin permitions"));
            }
            if (null == await Output.GetFind<Gathering>(x=> x.Title == gathering.Title))
            {
                throw new Exception("400", new Exception("A gathering of this title allready exists"));
            }

            await Input.Add(gathering.ToData(await Output.GetFind<AdminInfo>(x=> x.user.Username == username)));
        }

        public async Task Enter(string username, string password, string title)
        {
            var gathering = await Output.GetFind<Gathering>(x=> x.Title == title);
            if (gathering == null)
            {
                throw new Exception("400", new Exception("a gathering with the title \""+title+"\" does not exist"));
            }
            await Logic.Login(Output, username, password);
            gathering.atendees.Add(await Output.GetFind<User>(x=> x.Username == username));
            await Input.Set(gathering, gathering.id);
        }
    }
}
