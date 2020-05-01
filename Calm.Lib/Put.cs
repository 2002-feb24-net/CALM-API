using Calm.Dtb;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Calm.Lib
{
    public class Put : IPut
    {
        private IOutput output { get; set; }
        private IInput input { get; set; }

        public Put(IOutput output, IInput input)
        {
            this.output = output;
            this.input = input;
        }

        public async Task SetUser(string username, string password, UserItem value)
        {
            var item = await output.GetFind<User>(x => x.Username == username && x.Password == password);
            value.Id = item.Id;
            await input.Set(value.ToData(),item.Id);
        }
    }
}
