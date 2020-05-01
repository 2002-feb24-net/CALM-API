using Calm.Dtb;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Calm.Lib
{
    public class Delete : IDelete
    {
        private IOutput output { get; set; }
        private IInput input { get; set; }

        public Delete(IOutput output, IInput input)
        {
            this.output = output;
            this.input = input;
        }

        public async Task RemoveUser(string username, string password)
        {
            var item = await output.GetFind<User>(x => x.Username == username && x.Password == password);
            await input.Remove(item);
        }
    }
}
