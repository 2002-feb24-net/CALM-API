using System.Collections.Generic;
using System.Threading.Tasks;
using Calm.Dtb;
using Calm.Dtb.Models;
using Calm.Lib.Items;

namespace Calm.Lib
{
    public class Get : IGet
    {
        private IOutput Output { get; set; }

        public Get(IOutput output)
        {
            this.Output = output;
        }

        public async Task<UserItem> Login(string username, string password)
        {
            return await Logic.PopulateItem(Output, await Logic.Login(Output, username, password));
        }

        public async Task<IEnumerable<UserItem>> UserList()
        {
            var data = await Output.Get<User>();
            var ret = new List<UserItem>();

            foreach (var item in data)
            {
                item.Password = "";
                ret.Add(await Logic.PopulateItem(Output, item));
            }

            return ret;
        }

        public async Task<IEnumerable<GatheringItemOut>> ListGatherings()
        {
            var query = await Output.Get<Gathering>();
            var ret = new List<GatheringItemOut>();
            foreach (var item in query)
            {
                ret.Add(await Logic.PopulateItem(Output, item));
            }
            return ret;
        }

        public async Task<IEnumerable<string>> CityList()
        {
            var objs = await Output.Get<Mapdata>();
            var ret = new List<string>();
            foreach (var item in objs)
            {
                ret.Add(item.city);
            }
            return ret;
        }

        public async Task<IEnumerable<UserItem>> CityListUsers(string city)
        {
            int id = await Logic.CityId(Output, city);
            var data = await Output.GetFilter<User>(x=> x.MapDataId == id);
            var ret = new List<UserItem>();
            foreach (var item in data)
            {
                UserItem retItem = await Logic.PopulateItem(Output, item);
                retItem.Password = "";
                ret.Add(retItem);
            }
            return ret;
        }

        public async Task<IEnumerable<GatheringItemOut>> CityListGatherings(string city)
        {
            int id = await Logic.CityId(Output, city);
            var data = await Output.GetFilter<Gathering>(x => x.MapDataId == id);
            var ret = new List<GatheringItemOut>();
            foreach (var item in data)
            {
                GatheringItemOut retItem = await Logic.PopulateItem(Output, item);
                retItem.organizer.Password = "";
                foreach (var item2 in retItem.atendees)
                {
                    item2.Password = "";
                }
                ret.Add(retItem);
            }
            return ret;
        }

        public async Task<object> ListGatherings(bool v)
        {
            var query = await Output.Get<Gathering>();
            var ret = new List<GatheringItemOut>();
            foreach (var item in query)
            {
                if (v == item.isEvent)
                {
                    ret.Add(await Logic.PopulateItem(Output, item));
                }
            }
            return ret;
        }
    }
}
