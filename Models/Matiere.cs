using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Arkance.Models;

public partial class Matiere
{
    public int Id { get; set; }

    [Required(ErrorMessage = "The field Nom is required.")]
    public string Nom { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<Note> Notes { get; set; } = new List<Note>();

    public virtual ICollection<Professeur> Professeurs { get; set; } = new List<Professeur>();
}
