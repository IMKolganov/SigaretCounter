using System;
using System.Collections.Generic;

namespace SigaretCounter.Models;

public partial class Sigaret
{
    public int Id { get; set; }

    public int? TypeId { get; set; }

    public string Brand { get; set; } = null!;

    public DateOnly ProductionDate { get; set; }

    public int Quantity { get; set; }

    public decimal Price { get; set; }

    public string? CountryOfOrigin { get; set; }

    public decimal? NicotineContent { get; set; }

    public virtual SigaretsType? Type { get; set; }
}
