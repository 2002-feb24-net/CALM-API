using Calm.Dtb;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Calm.Lib
{
    public class Put : IPut
    {
        private IOutput Output { get; set; }
        private IInput Input { get; set; }

        public Put(IOutput output, IInput input)
        {
            this.Output = output;
            this.Input = input;
        }

        public async Task SetUser(string username, string password, UserItem value)
        {
            var item = await Logic.Login(Output, username, password);
            value.Id = item.Id;
            value.IsAdmin = item.IsAdmin;
            await Input.Set(value.ToData(),item.Id);
        }
    }
}
