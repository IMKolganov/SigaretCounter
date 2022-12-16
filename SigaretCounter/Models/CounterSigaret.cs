using System;
using System.Collections.Generic;

namespace SigaretCounter.Models;

public partial class CounterSigaret
{
    public int Id { get; set; }

    public int Userid { get; set; }

    public int SigaretsCount { get; set; }
}
