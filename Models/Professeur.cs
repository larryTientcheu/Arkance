using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Arkance.Models;

public partial class Professeur
{
    public int Id { get; set; }

    [Required(ErrorMessage = "The field Nom is required.")]
    public string Nom { get; set; } = null!;

    [Required(ErrorMessage = "The field Prenom is required.")]
    public string Prenom { get; set; } = null!;

    public string Genre { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<Classe> Classes { get; } = new List<Classe>();
    [JsonIgnore]
    public virtual ICollection<Matiere> Matieres { get; } = new List<Matiere>();
}
