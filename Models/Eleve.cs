using System;
using System.Collections.Generic;

namespace Arkance.Models;

public partial class Eleve
{
    public int Id { get; set; }

    public string Nom { get; set; } = null!;

    public string Prenom { get; set; } = null!;

    public string Genre { get; set; } = null!;

    public int? ClasseId { get; set; }

    public virtual Classe? Classe { get; set; }

    public virtual ICollection<Note> Notes { get; set; } = new List<Note>();
}
