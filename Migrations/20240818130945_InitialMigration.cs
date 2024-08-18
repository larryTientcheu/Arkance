using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Arkance.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "matiere",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    nom = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("matiere_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "professeur",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    nom = table.Column<string>(type: "text", nullable: false),
                    prenom = table.Column<string>(type: "text", nullable: false),
                    genre = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("professeur_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "classe",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    niveau = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    professeur_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("classe_pkey", x => x.id);
                    table.ForeignKey(
                        name: "classe_professeur_id_fkey",
                        column: x => x.professeur_id,
                        principalTable: "professeur",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "professeur_matiere",
                columns: table => new
                {
                    professeur_id = table.Column<int>(type: "integer", nullable: false),
                    matiere_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("professeur_matiere_pkey", x => new { x.professeur_id, x.matiere_id });
                    table.ForeignKey(
                        name: "professeur_matiere_matiere_id_fkey",
                        column: x => x.matiere_id,
                        principalTable: "matiere",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "professeur_matiere_professeur_id_fkey",
                        column: x => x.professeur_id,
                        principalTable: "professeur",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "eleve",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    nom = table.Column<string>(type: "text", nullable: false),
                    prenom = table.Column<string>(type: "text", nullable: false),
                    genre = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    classe_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("eleve_pkey", x => x.id);
                    table.ForeignKey(
                        name: "eleve_classe_id_fkey",
                        column: x => x.classe_id,
                        principalTable: "classe",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "note",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    valeur = table.Column<double>(type: "double precision", nullable: true),
                    eleve_id = table.Column<int>(type: "integer", nullable: false),
                    matiere_id = table.Column<int>(type: "integer", nullable: false),
                    Appreciation = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("note_pkey", x => x.id);
                    table.ForeignKey(
                        name: "note_eleve_id_fkey",
                        column: x => x.eleve_id,
                        principalTable: "eleve",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "note_matiere_id_fkey",
                        column: x => x.matiere_id,
                        principalTable: "matiere",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "idx_classe_professeur_id",
                table: "classe",
                column: "professeur_id");

            migrationBuilder.CreateIndex(
                name: "idx_eleve_classe_id",
                table: "eleve",
                column: "classe_id");

            migrationBuilder.CreateIndex(
                name: "idx_note_eleve_id",
                table: "note",
                column: "eleve_id");

            migrationBuilder.CreateIndex(
                name: "idx_note_eleve_id_matiere_id",
                table: "note",
                columns: new[] { "eleve_id", "matiere_id" });

            migrationBuilder.CreateIndex(
                name: "idx_note_matiere_id",
                table: "note",
                column: "matiere_id");

            migrationBuilder.CreateIndex(
                name: "note_eleve_id_matiere_id_key",
                table: "note",
                columns: new[] { "eleve_id", "matiere_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_professeur_matiere_matiere_id",
                table: "professeur_matiere",
                column: "matiere_id");

            migrationBuilder.CreateIndex(
                name: "idx_professeur_matiere_professeur_id",
                table: "professeur_matiere",
                column: "professeur_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "note");

            migrationBuilder.DropTable(
                name: "professeur_matiere");

            migrationBuilder.DropTable(
                name: "eleve");

            migrationBuilder.DropTable(
                name: "matiere");

            migrationBuilder.DropTable(
                name: "classe");

            migrationBuilder.DropTable(
                name: "professeur");
        }
    }
}
