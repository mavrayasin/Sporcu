using System;
using System.Collections.Generic;

namespace Sporcu.Entity;

public partial class UserSporcu
{
    public int Id { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public string? PasswordHash { get; set; }
}
