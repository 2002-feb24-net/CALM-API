using Calm.Dtb;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Calm.Lib
{
    public class Get : IGet
    {
        private IOutput output { get; set; }

        public Get(IOutput output)
        {
            this.output = output;
        }

        public async Task<IEnumerable<UserItem>> Users()
        {
            var querry = await output.Get<User>();
            var ret = new List<UserItem>();

            foreach (var item in querry)
            {
                ret.Add(new UserItem(item));
            }

            return ret;
        }
    }
}
