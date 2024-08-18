using Arkance.Models;
using Bogus;

namespace Arkance.Interface
{
    public static class DbSeeder
    {
        public static void Seed(ArkanceTestContext context)
        {
            // Check if the database is already seeded
            if (context.Professeurs.Any() || context.Classes.Any() || context.Eleves.Any() || context.Matieres.Any() || context.Notes.Any())
            {
                return; // Database has already been seeded
            }

            // Initialize Faker for data generation
            var faker = new Faker("fr");
            Random random = new Random();

            // Seed Professeurs
            var professeurs = new List<Professeur>();
            for (int i = 0; i < 30; i++)
            {
                var professeur = new Professeur
                {
                    Nom = faker.Name.LastName(),
                    Prenom = faker.Name.FirstName(),
                    Genre = faker.PickRandom("Male", "Female")
                };
                professeurs.Add(professeur);
            }

            context.Professeurs.AddRange(professeurs);
            context.SaveChanges();

            // Seed Matieres
            var matieres = new List<Matiere>
        {
            new Matiere { Nom = "Math" },
            new Matiere { Nom = "Physique" },
            new Matiere { Nom = "Chimie" },
            new Matiere { Nom = "Biologie" },
            new Matiere { Nom = "Histoire" }
        };

            context.Matieres.AddRange(matieres);
            context.SaveChanges();

            // Associate professeurs with matieres
            foreach (var matiere in matieres)
            {
                var randomProf = faker.PickRandom(professeurs, faker.Random.Int(0, 3)).ToList();
                foreach (var prof in randomProf)
                {
                    context.Entry(matiere).Collection(p => p.Professeurs).Load();
                    matiere.Professeurs.Add(prof);
                }
            }
            context.SaveChanges();

            // Seed Classes
            var classeList = new List<string>
            { "Sixieme", "Cinquieme", "Quatrieme", "Troisieme", "Seconde", "Premiere", "Terminal" };

            var classes = new List<Classe>();
            foreach (string classe in classeList)
            {
                var randomId = random.Next(0, 6);
                var singleClass = new Classe
                {
                    Niveau = classe,
                    ProfesseurId = professeurs[randomId].Id
                };
                classes.Add(singleClass);
            }

            context.Classes.AddRange(classes);
            context.SaveChanges();

            // Seed Eleves
            var eleves = new List<Eleve>();
            foreach (var classe in classes)
            {
                var randomNbEleves = random.Next(1, 30);
                for (int i = 0; i < randomNbEleves; i++)
                {
                    var eleve = new Eleve
                    {
                        Nom = faker.Name.LastName(),
                        Prenom = faker.Name.FirstName(),
                        Genre = faker.PickRandom("Male", "Female"),
                        ClasseId = classe.Id
                    };
                    eleves.Add(eleve);
                }
            }

            context.Eleves.AddRange(eleves);
            context.SaveChanges();

            // Seed Notes
            var notes = new List<Note>();
            foreach (var eleve in eleves)
            {
                var randomMatieres = faker.PickRandom(matieres, faker.Random.Int(1, matieres.Count())).ToList();
                foreach (var matiere in randomMatieres)
                {
                    var note = new Note
                    {
                        Valeur = faker.Random.Double(0, 20),
                        EleveId = eleve.Id,
                        MatiereId = matiere.Id
                    };
                    notes.Add(note);
                }
            }

            context.Notes.AddRange(notes);
            context.SaveChanges();
        }
    }
}
