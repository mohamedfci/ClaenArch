using Domains.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains.DTO
{
    public  class UserDTO:TblUsers
    {
        public string? Token { get; set; }
    }
}
