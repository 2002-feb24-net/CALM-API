using Calm.Dtb;
using Calm.Dtb.Models;
using Calm.Lib.Items;
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
            if (ret == null) throw new Exception("401", new Exception("User is not found"));
            return ret;
        }

        public async static Task<UserItem> AddUser(IOutput output, IInput input, UserItem user)
        {
            if (!await UsernameExists(output, user.Username))
            {
                var inUser = user.ToData();
                inUser.MapDataId = await CityId(output, user.City);
                if (user.IsAdmin)
                {
                    await input.Add(new AdminInfo() { user = inUser });
                }
                else
                {
                    await input.Add(inUser);
                }
                return user;
            }
            else
            {
                throw new Exception("409", new Exception("username is taken"));
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

        public async static Task<int> CityId(IOutput output, string City)
        {
            var item = await output.GetFind<Mapdata>(x => x.city == City);
            if (item == null)
            {
                throw new Exception("404", new Exception("City does not occur in the database"));
            }
            return item.Id;
        }

        public async static Task<UserItem> PopulateItem(IOutput output, User user)
        {
            return new UserItem()
            {
                FName = user.FName,
                LName = user.LName,
                Username = user.Username,
                Password = user.Password,
                City = (await output.Get<Mapdata>(user.MapDataId)).city,
                IsAdmin = null != (await output.GetFind<AdminInfo>(x => x.userId == user.Id))
            };
        }

        public async static Task<GatheringItemOut> PopulateItem(IOutput output, Gathering item)
        {
            var links = await output.GetFilter<Link>(x => x.gatheringId == item.id);
            var users = new List<UserItem>();
            foreach (var i in links)
            {
                users.Add(await PopulateItem(output, await output.Get<User>(i.userId)));
            }

            return new GatheringItemOut()
            {
                Title = item.Title,
                occurrenceData = item.occurrenceData,
                City = (await output.Get<Mapdata>(item.MapDataId)).city,
                details = item.details,
                organizer = await PopulateItem(output, await output.Get<User>(item.organizerId)),
                atendees = users,
                isEvent = item.isEvent
            };
        }
    }
}
