using System;
using System.Collections.Generic;

namespace Sporcu.Entity;

public partial class TblIl
{
    public int Id { get; set; }

    public string Ad { get; set; } = null!;

    public virtual ICollection<TblIlce> TblIlces { get; set; } = new List<TblIlce>();
}
