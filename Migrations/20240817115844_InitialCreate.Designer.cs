﻿// <auto-generated />
using System;
using Arkance.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Arkance.Migrations
{
    [DbContext(typeof(ArkanceTestContext))]
    [Migration("20240817115844_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Arkance.Models.Classe", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<int>("Id"));

                    b.Property<string>("Niveau")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("niveau");

                    b.Property<int?>("ProfesseurId")
                        .HasColumnType("integer")
                        .HasColumnName("professeur_id");

                    b.HasKey("Id")
                        .HasName("classe_pkey");

                    b.HasIndex(new[] { "ProfesseurId" }, "idx_classe_professeur_id");

                    b.ToTable("classe", (string)null);
                });

            modelBuilder.Entity("Arkance.Models.Eleve", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<int>("Id"));

                    b.Property<int?>("ClasseId")
                        .HasColumnType("integer")
                        .HasColumnName("classe_id");

                    b.Property<string>("Genre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("genre");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("nom");

                    b.Property<string>("Prenom")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("prenom");

                    b.HasKey("Id")
                        .HasName("eleve_pkey");

                    b.HasIndex(new[] { "ClasseId" }, "idx_eleve_classe_id");

                    b.ToTable("eleve", (string)null);
                });

            modelBuilder.Entity("Arkance.Models.Matiere", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<int>("Id"));

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("nom");

                    b.HasKey("Id")
                        .HasName("matiere_pkey");

                    b.ToTable("matiere", (string)null);
                });

            modelBuilder.Entity("Arkance.Models.Note", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<int>("Id"));

                    b.Property<int?>("EleveId")
                        .HasColumnType("integer")
                        .HasColumnName("eleve_id");

                    b.Property<int?>("MatiereId")
                        .HasColumnType("integer")
                        .HasColumnName("matiere_id");

                    b.Property<double?>("Valeur")
                        .HasColumnType("double precision")
                        .HasColumnName("valeur");

                    b.HasKey("Id")
                        .HasName("note_pkey");

                    b.HasIndex(new[] { "EleveId" }, "idx_note_eleve_id");

                    b.HasIndex(new[] { "EleveId", "MatiereId" }, "idx_note_eleve_id_matiere_id");

                    b.HasIndex(new[] { "MatiereId" }, "idx_note_matiere_id");

                    b.ToTable("note", (string)null);
                });

            modelBuilder.Entity("Arkance.Models.Professeur", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<int>("Id"));

                    b.Property<string>("Genre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("genre");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("nom");

                    b.Property<string>("Prenom")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("prenom");

                    b.HasKey("Id")
                        .HasName("professeur_pkey");

                    b.ToTable("professeur", (string)null);
                });

            modelBuilder.Entity("ProfesseurMatiere", b =>
                {
                    b.Property<int>("ProfesseurId")
                        .HasColumnType("integer")
                        .HasColumnName("professeur_id");

                    b.Property<int>("MatiereId")
                        .HasColumnType("integer")
                        .HasColumnName("matiere_id");

                    b.HasKey("ProfesseurId", "MatiereId")
                        .HasName("professeur_matiere_pkey");

                    b.HasIndex(new[] { "MatiereId" }, "idx_professeur_matiere_matiere_id");

                    b.HasIndex(new[] { "ProfesseurId" }, "idx_professeur_matiere_professeur_id");

                    b.ToTable("professeur_matiere", (string)null);
                });

            modelBuilder.Entity("Arkance.Models.Classe", b =>
                {
                    b.HasOne("Arkance.Models.Professeur", "Professeur")
                        .WithMany("Classes")
                        .HasForeignKey("ProfesseurId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .HasConstraintName("classe_professeur_id_fkey");

                    b.Navigation("Professeur");
                });

            modelBuilder.Entity("Arkance.Models.Eleve", b =>
                {
                    b.HasOne("Arkance.Models.Classe", "Classe")
                        .WithMany("Eleves")
                        .HasForeignKey("ClasseId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .HasConstraintName("eleve_classe_id_fkey");

                    b.Navigation("Classe");
                });

            modelBuilder.Entity("Arkance.Models.Note", b =>
                {
                    b.HasOne("Arkance.Models.Eleve", "Eleve")
                        .WithMany("Notes")
                        .HasForeignKey("EleveId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("note_eleve_id_fkey");

                    b.HasOne("Arkance.Models.Matiere", "Matiere")
                        .WithMany("Notes")
                        .HasForeignKey("MatiereId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .HasConstraintName("note_matiere_id_fkey");

                    b.Navigation("Eleve");

                    b.Navigation("Matiere");
                });

            modelBuilder.Entity("ProfesseurMatiere", b =>
                {
                    b.HasOne("Arkance.Models.Matiere", null)
                        .WithMany()
                        .HasForeignKey("MatiereId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("professeur_matiere_matiere_id_fkey");

                    b.HasOne("Arkance.Models.Professeur", null)
                        .WithMany()
                        .HasForeignKey("ProfesseurId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("professeur_matiere_professeur_id_fkey");
                });

            modelBuilder.Entity("Arkance.Models.Classe", b =>
                {
                    b.Navigation("Eleves");
                });

            modelBuilder.Entity("Arkance.Models.Eleve", b =>
                {
                    b.Navigation("Notes");
                });

            modelBuilder.Entity("Arkance.Models.Matiere", b =>
                {
                    b.Navigation("Notes");
                });

            modelBuilder.Entity("Arkance.Models.Professeur", b =>
                {
                    b.Navigation("Classes");
                });
#pragma warning restore 612, 618
        }
    }
}
