using Calm.Dtb;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Calm.Lib
{
    class Logic
    {
        public async static Task<User> Login(IOutput output, string username, string password)
        {
            var ret = await output.GetFind<User>(x => x.Username == username && x.Password == password);
            if (ret == null) throw new Exception("404", new Exception("User is not found"));
            return ret;
        }
    }
}
