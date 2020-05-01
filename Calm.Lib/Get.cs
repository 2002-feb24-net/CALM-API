using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Calm.Dtb;

namespace Calm.Lib
{
    public class Get : IGet
    {
        private IOutput output { get; set; }

        public Get(IOutput output)
        {
            this.output = output;
        }

        public async Task<User> Login(string username, string password)
        {
            return await output.GetFind<User>(x => x.Username == username && x.Password == password);
        }
    }
}
