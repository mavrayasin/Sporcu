using System;
using System.Collections.Generic;

namespace Sporcu.Entity;

public partial class TblIlce
{
    public int Id { get; set; }

    public int IlId { get; set; }

    public string Ad { get; set; } = null!;

    public virtual TblIl Il { get; set; } = null!;
}
