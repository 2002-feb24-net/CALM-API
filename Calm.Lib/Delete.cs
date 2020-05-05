using Calm.Dtb;
using Calm.Dtb.Models;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
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

        public async Task RemoveGathering(string username, string password, string title)
        {
            await Logic.Login(Output, username, password);
            var item = await Output.GetFind<Gathering>(x=> x.Title == title);

            if (item == null)
            {
                throw new Exception("400", new Exception("Gathering Title is incorect"));
            }

            foreach (var tag in await Output.GetFilter<Link>(x=> x.gatheringId == item.id))
            {
                await Input.Remove(tag);
            }

            await Input.Remove(item);
        }
    }
}
