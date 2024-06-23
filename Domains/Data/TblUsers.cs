using System;
using System.Collections.Generic;

namespace Domains.Data;

public partial class TblUsers
{
    public int Id { get; set; }

    public string? UserName { get; set; }

    public string? Pass { get; set; }

    public string? Role { get; set; }

}

