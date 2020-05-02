using Calm.Dtb;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Calm.Lib
{
    public class Delete : IDelete
    {
        private IOutput Output { get; set; }
        private IInput Input { get; set; }

        public Delete(IOutput output, IInput input)
        {
            this.Output = output;
            this.Input = input;
        }

        public async Task RemoveUser(string username, string password)
        {
            var item = await Logic.Login(Output, username, password);
            await Input.Remove(item);
        }
    }
}
