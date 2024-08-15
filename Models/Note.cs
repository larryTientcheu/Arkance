using System;
using System.Collections.Generic;

namespace Arkance.Models;

public partial class Note
{
    public int Id { get; set; }

    public double? Valeur { get; set; }

    public int? EleveId { get; set; }

    public int? MatiereId { get; set; }

    public virtual Eleve? Eleve { get; set; }

    public virtual Matiere? Matiere { get; set; }
}
