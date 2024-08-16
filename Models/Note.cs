using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Arkance.Models;

public partial class Note
{
    public int Id { get; set; }

    public double? Valeur { get; set; }

    public int? EleveId { get; set; }

    public int? MatiereId { get; set; }
    [JsonIgnore]
    public virtual Eleve? Eleve { get; }

    public virtual Matiere? Matiere { get; }
}
