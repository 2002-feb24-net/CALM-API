using Calm.Dtb;
using Calm.Dtb.Models;
using Calm.Lib.Items;
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
            value.IsAdmin = await Logic.CheckAdmin(Output, username, password);
            await Input.Set(value.ToData(),item.Id);
        }

        public async Task SwapUserStatus(string username, string password, string subjectUsername)
        {
            if (!await Logic.CheckAdmin(Output, username, password))
            {
                throw new Exception("400", new Exception("to set another users admin status," +
                    " the credentials for an existing admin user must be provided"));
            }

            bool isAdmin = null != await Output.GetFind<AdminInfo>(x => x.user.Username == subjectUsername);

            if (!isAdmin)
            {
                await Input.Add(new AdminInfo() { userId = Output.GetFind<User>(x=> x.Username == subjectUsername).Id });
            }
            else
            {
                if ((await Output.GetFind<AdminInfo>(x => x.user.Username == subjectUsername)).SuperAdmin)
                {
                    await Input.Remove(Output.GetFind<AdminInfo>(x=> x.user.Username==subjectUsername));
                }
                else
                {
                    throw new Exception("400", new Exception(
                        "cannot revoke admin status without super admin credentials"));
                }
            }
        }

        public async Task EditGatheringInfo(string username, string password, string formerTitle, GatheringItemIn gathering)
        {
            var subject = await Output.GetFind<Gathering>(x => x.Title == formerTitle);
            if (subject == null)
            {
                throw new Exception("400", new Exception("subject cannot be found"));
            }
            if (subject.organizer.Username != (await Logic.Login(Output, username, password)).Username)
            {
                throw new Exception("400", new Exception("only the orginizer can edit the gathering item"));
            }
            if (null != await Output.GetFind<Gathering>(x=> x.Title == gathering.Title))
            {
                throw new Exception("400", new Exception("given title \""+gathering.Title+"\" is allready used"));
            }
            subject.Title = gathering.Title;
            subject.occurrenceData = gathering.occurrenceData;
            subject.details = gathering.occurrenceData;

            await Input.Set(subject, subject.id);
        }
    }
}
