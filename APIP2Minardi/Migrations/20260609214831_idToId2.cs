using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIP2Minardi.Migrations
{
    /// <inheritdoc />
    public partial class idToId2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "id",
                table: "Setores",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "PrescricoesMedicamentos",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "PrescricoesGerais",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Pacientes",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Medicos",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Medicamentos",
                newName: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Setores",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "PrescricoesMedicamentos",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "PrescricoesGerais",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Pacientes",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Medicos",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Medicamentos",
                newName: "id");
        }
    }
}
