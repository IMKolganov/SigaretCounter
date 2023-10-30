using System;
using System.Collections.Generic;

namespace SigaretCounter.Models;

public partial class User
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public string Displayname { get; set; } = null!;

    public string Languageuser { get; set; } = null!;

    public string? Photourl { get; set; }

    public DateTime? Dateofbirth { get; set; }

    public int Emailverified { get; set; }

    public string? Phonenumber { get; set; }

    public DateTime Createdat { get; set; }

    public string Username { get; set; } = null!;
}
