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
            foreach (var link in await Output.GetFilter<Link>(x=> x.userId == item.Id))
            {
                await Input.Remove(link);
            }
            await Input.Remove(item);
        }

        public async Task RemoveGathering(string username, string password, string title)
        {
            int id = (await Logic.Login(Output, username, password)).Id;
            var item = await Output.GetFind<Gathering>(x=> x.Title == title);

            if (item == null)
            {
                throw new Exception("404", new Exception("Gathering Title is incorect"));
            }
            if (!(item.organizerId == id || null != await Output.GetFind<AdminInfo>(x=> x.SuperAdmin && x.userId == id)))
            {
                throw new Exception("403", new Exception("User is not the gathering organiser"));
            }
            foreach (var tag in await Output.GetFilter<Link>(x=> x.gatheringId == item.id))
            {
                await Input.Remove(tag);
            }

            await Input.Remove(item);
        }
    }
}
