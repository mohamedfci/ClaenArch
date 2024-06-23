using Domains.Enum;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains.Data
{
    public  class UsersDTO: IdentityUser
    {
        public Role Role { get; set; }
    }
}
