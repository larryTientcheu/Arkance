using System.Text.Json.Serialization;

namespace Arkance.Models;

public partial class Matiere
{
    public int Id { get; set; }

    public string Nom { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<Note> Notes { get; set; } = new List<Note>();
    
    public virtual ICollection<Professeur> Professeurs { get; set; } = new List<Professeur>();
}
