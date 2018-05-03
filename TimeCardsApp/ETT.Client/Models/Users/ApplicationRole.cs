using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETT.Web.Models.Users
{
    public class Role : IdentityRole<int>
    {
        public Role(string name)
            : base(name) { }

        public Role() { }
    }
}
