using Calm.Dtb;
using Calm.Dtb.Models;
using Calm.Lib.Items;
using System;
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
            if (item.IsAdmin) throw new Exception("401", new Exception(
                "cannot add an admin without existing admin credentials," +
                " use \"api/admin/{username}/{password} with the same body\""));

            return await Logic.AddUser(Output, Input, item);
        }

        public async Task<UserItem> AdminUser(string username, string password, UserItem item)
        {
            if (await Logic.CheckAdmin(Output, username, password))
            {
                return await Logic.AddUser(Output, Input, item);
            }
            else
            {
                throw new Exception("403", new Exception("User is not an admin"));
            }
        }

        public async Task AddGathering(string username, string password, GatheringItemIn gathering)
        {
            if (!await Logic.CheckAdmin(Output, username, password))
            {
                throw new Exception("403", new Exception("this request must be made with admin permitions"));
            }
            if (null != await Output.GetFind<Gathering>(x => x.Title == gathering.Title))
            {
                throw new Exception("409", new Exception("A gathering of this title allready exists"));
            }

            await Input.Add(gathering.ToData(await Output.GetFind<AdminInfo>(x => x.user.Username == username)));
        }

        public async Task Enter(string username, string password, string title)
        {
            var user = await Logic.Login(Output, username, password);
            var gathering = await Output.GetFind<Gathering>(x => x.Title == title);
            if (gathering == null)
            {
                throw new Exception("404", new Exception("a gathering with the title \"" + title + "\" does not exist"));
            }
            if ((await Output.GetFind<Link>(x => x.userId == user.Id && x.gatheringId == gathering.id)) != null)
            {
                throw new Exception("409", new Exception("this user is allready signed up for this gathering"));
            }
            await Input.Add(new Link() { userId = user.Id, gatheringId = gathering.id });
        }
    }
}
