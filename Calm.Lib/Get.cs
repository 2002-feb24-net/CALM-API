﻿using System;
using System.Collections;
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
            return new UserItem(await Logic.Login(Output, username, password), false);
        }

        public async Task<IEnumerable<UserItem>> UserList()
        {
            var data = await Output.Get<User>();
            var ret = new List<UserItem>();

            foreach (var item in data)
            {
                item.Password = "";
                ret.Add(new UserItem(item, await Logic.CheckAdmin(Output, item.Username)));
            }

            return ret;
        }

        public async Task<IEnumerable<GatheringItemOut>> ListGatherings()
        {
            var query = await Output.Get<Gathering>();
            var ret = new List<GatheringItemOut>();
            foreach (var item in query)
            {
                ret.Add(new GatheringItemOut(item));
            }
            return ret;
        }
    }
}
