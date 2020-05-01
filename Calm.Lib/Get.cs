using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Calm.Dtb;

namespace Calm.Lib
{
    public class Get : IGet
    {
        private IOutput Output { get; set; }

        public Get(IOutput output)
        {
            this.Output = output;
        }

        public async Task<User> Login(string username, string password)
        {
            return await Output.GetFind<User>(x => x.Username == username && x.Password == password);
        }
    }
}
