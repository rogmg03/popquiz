using System;
using System.Collections.Generic;

namespace PAW.Data.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Password { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public DateTime DateBirth { get; set; }

    public int RoleId { get; set; }

    public virtual Role Role { get; set; } = null!;
}
