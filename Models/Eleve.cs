using System.Text.Json.Serialization;

namespace Arkance.Models;

public partial class Eleve
{
    public int Id { get; set; }

    public string Nom { get; set; } = null!;

    public string Prenom { get; set; } = null!;

    public string Genre { get; set; } = null!;

    public int? ClasseId { get; set; }

    [JsonIgnore]
    public virtual Classe? Classe { get; set; }

    public virtual ICollection<Note> Notes { get; } = new List<Note>();
}
