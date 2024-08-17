namespace Arkance.Models;

public partial class Classe
{
    public int Id { get; set; }

    public string Niveau { get; set; } = null!;

    public int? ProfesseurId { get; set; }

    public virtual ICollection<Eleve> Eleves { get; set; } = new List<Eleve>();

    public virtual Professeur? Professeur { get; set; }
}
