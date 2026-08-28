using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Perfiles.Migrations
{
    /// <inheritdoc />
    public partial class AgregarTelefonos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Telefonos",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    numero = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    persona_id = table.Column<int>(type: "integer", nullable: false),
                    PersonaModelId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Telefonos", x => x.id);
                    table.ForeignKey(
                        name: "FK_Telefonos_Personas_PersonaModelId",
                        column: x => x.PersonaModelId,
                        principalTable: "Personas",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Telefonos_PersonaModelId",
                table: "Telefonos",
                column: "PersonaModelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Telefonos");
        }
    }
}
