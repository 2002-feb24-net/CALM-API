using Calm.Dtb;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Calm.Lib
{
    public class Post : IPost
    {
        private IInput input { get; set; }

        public Post(IInput input)
        {
            this.input = input;
        }

        public async Task<UserItem> User(UserItem item)
        {
            var data = await input.Add(item.ToData());
            item.Id = data.Id;
            return item;
        }
    }
}
