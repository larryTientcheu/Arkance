using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Arkance.Models;

public partial class Note
{
    public int Id { get; set; }

    [Range(0, 20, ErrorMessage = "The field Valeur must be between 0 and 20.")]
    public double? Valeur { get; set; }

    [Required(ErrorMessage = "The EleveId field is required.")]
    public int? EleveId { get; set; }

    [Required(ErrorMessage = "The MatiereId field is required.")]
    public int? MatiereId { get; set; }
    [JsonIgnore]
    public virtual Eleve? Eleve { get; }

    public virtual Matiere? Matiere { get; }
}
