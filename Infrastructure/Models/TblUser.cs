using System;
using System.Collections.Generic;

namespace Infrastructure.Models;

public partial class TblUser
{
    public int Id { get; set; }

    public string? UserName { get; set; }

    public string? Pass { get; set; }

    public string? Role { get; set; }
}
