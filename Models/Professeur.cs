using System;
using System.Collections.Generic;

namespace Arkance.Models;

public partial class Professeur
{
    public int Id { get; set; }

    public string Nom { get; set; } = null!;

    public string Prenom { get; set; } = null!;

    public string Genre { get; set; } = null!;

    public virtual ICollection<Classe> Classes { get; set; } = new List<Classe>();

    public virtual ICollection<Matiere> Matieres { get; set; } = new List<Matiere>();
}
