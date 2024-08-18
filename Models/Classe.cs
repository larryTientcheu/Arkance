﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Arkance.Models;

public partial class Classe
{
    public int Id { get; set; }

    [Required(ErrorMessage = "The field Niveau is required.")]
    public string Niveau { get; set; } = null!;

    [Required(ErrorMessage = "The ProfesseurId field is required.")]
    public int? ProfesseurId { get; set; }

    public virtual ICollection<Eleve> Eleves { get; set; } = new List<Eleve>();
    [JsonIgnore]
    public virtual Professeur? Professeur { get; }
}