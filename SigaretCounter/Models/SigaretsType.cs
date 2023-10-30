using System;
using System.Collections.Generic;

namespace SigaretCounter.Models;

public partial class SigaretsType
{
    public int TypeId { get; set; }

    public string Description { get; set; } = null!;

    public DateOnly CreationDate { get; set; }

    public virtual ICollection<Sigaret> Sigarets { get; } = new List<Sigaret>();
}
