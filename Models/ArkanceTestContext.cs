using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Arkance.Models;

public partial class ArkanceTestContext : DbContext
{
    public ArkanceTestContext()
    {
    }

    public ArkanceTestContext(DbContextOptions<ArkanceTestContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Classe> Classes { get; set; }

    public virtual DbSet<Eleve> Eleves { get; set; }

    public virtual DbSet<Matiere> Matieres { get; set; }

    public virtual DbSet<Note> Notes { get; set; }

    public virtual DbSet<Professeur> Professeurs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("name=dbcon");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Classe>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("classe_pkey");

            entity.ToTable("classe");

            entity.HasIndex(e => e.ProfesseurId, "idx_classe_professeur_id");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Niveau)
                .HasMaxLength(50)
                .HasColumnName("niveau");
            entity.Property(e => e.ProfesseurId).HasColumnName("professeur_id");

            entity.HasOne(d => d.Professeur).WithMany(p => p.Classes)
                .HasForeignKey(d => d.ProfesseurId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("classe_professeur_id_fkey");
        });

        modelBuilder.Entity<Eleve>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("eleve_pkey");

            entity.ToTable("eleve");

            entity.HasIndex(e => e.ClasseId, "idx_eleve_classe_id");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.ClasseId).HasColumnName("classe_id");
            entity.Property(e => e.Genre)
                .HasMaxLength(50)
                .HasColumnName("genre");
            entity.Property(e => e.Nom).HasColumnName("nom");
            entity.Property(e => e.Prenom).HasColumnName("prenom");

            entity.HasOne(d => d.Classe).WithMany(p => p.Eleves)
                .HasForeignKey(d => d.ClasseId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("eleve_classe_id_fkey");
        });

        modelBuilder.Entity<Matiere>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("matiere_pkey");

            entity.ToTable("matiere");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Nom).HasColumnName("nom");
        });

        modelBuilder.Entity<Note>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("note_pkey");

            entity.ToTable("note");

            entity.HasIndex(e => e.EleveId, "idx_note_eleve_id");

            entity.HasIndex(e => new { e.EleveId, e.MatiereId }, "idx_note_eleve_id_matiere_id");

            entity.HasIndex(e => e.MatiereId, "idx_note_matiere_id");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.EleveId).HasColumnName("eleve_id");
            entity.Property(e => e.MatiereId).HasColumnName("matiere_id");
            entity.Property(e => e.Valeur).HasColumnName("valeur");

            entity.HasOne(d => d.Eleve).WithMany(p => p.Notes)
                .HasForeignKey(d => d.EleveId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("note_eleve_id_fkey");

            entity.HasOne(d => d.Matiere).WithMany(p => p.Notes)
                .HasForeignKey(d => d.MatiereId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("note_matiere_id_fkey");
        });

        modelBuilder.Entity<Professeur>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("professeur_pkey");

            entity.ToTable("professeur");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Genre)
                .HasMaxLength(50)
                .HasColumnName("genre");
            entity.Property(e => e.Nom).HasColumnName("nom");
            entity.Property(e => e.Prenom).HasColumnName("prenom");

            entity.HasMany(d => d.Matieres).WithMany(p => p.Professeurs)
                .UsingEntity<Dictionary<string, object>>(
                    "ProfesseurMatiere",
                    r => r.HasOne<Matiere>().WithMany()
                        .HasForeignKey("MatiereId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .HasConstraintName("professeur_matiere_matiere_id_fkey"),
                    l => l.HasOne<Professeur>().WithMany()
                        .HasForeignKey("ProfesseurId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .HasConstraintName("professeur_matiere_professeur_id_fkey"),
                    j =>
                    {
                        j.HasKey("ProfesseurId", "MatiereId").HasName("professeur_matiere_pkey");
                        j.ToTable("professeur_matiere");
                        j.HasIndex(new[] { "MatiereId" }, "idx_professeur_matiere_matiere_id");
                        j.HasIndex(new[] { "ProfesseurId" }, "idx_professeur_matiere_professeur_id");
                        j.IndexerProperty<int>("ProfesseurId").HasColumnName("professeur_id");
                        j.IndexerProperty<int>("MatiereId").HasColumnName("matiere_id");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
