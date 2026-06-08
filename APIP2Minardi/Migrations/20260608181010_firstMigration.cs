using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIP2Minardi.Migrations
{
    /// <inheritdoc />
    public partial class firstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Medicamentos",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: true),
                    Dosagem = table.Column<string>(type: "TEXT", nullable: true),
                    Via_Administracao = table.Column<string>(type: "TEXT", nullable: true),
                    Estoque = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicamentos", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Medicos",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: true),
                    CRM = table.Column<string>(type: "TEXT", nullable: false),
                    Especialidade = table.Column<string>(type: "TEXT", nullable: true),
                    Telefone = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicos", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Setores",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: true),
                    Andar = table.Column<string>(type: "TEXT", nullable: true),
                    Descricao = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Setores", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Pacientes",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: true),
                    CPF = table.Column<string>(type: "TEXT", nullable: true),
                    Data_Nascimento = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Leito = table.Column<string>(type: "TEXT", nullable: true),
                    SETOR_id = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pacientes", x => x.id);
                    table.ForeignKey(
                        name: "FK_Pacientes_Setores_SETOR_id",
                        column: x => x.SETOR_id,
                        principalTable: "Setores",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrescricoesGerais",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Data = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Observacao = table.Column<string>(type: "TEXT", nullable: true),
                    MEDICO_id = table.Column<int>(type: "INTEGER", nullable: false),
                    PACIENTE_id = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrescricoesGerais", x => x.id);
                    table.ForeignKey(
                        name: "FK_PrescricoesGerais_Medicos_MEDICO_id",
                        column: x => x.MEDICO_id,
                        principalTable: "Medicos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PrescricoesGerais_Pacientes_PACIENTE_id",
                        column: x => x.PACIENTE_id,
                        principalTable: "Pacientes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrescricoesMedicamentos",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Quantidade = table.Column<string>(type: "TEXT", nullable: true),
                    Frequencia = table.Column<string>(type: "TEXT", nullable: true),
                    Horario = table.Column<TimeSpan>(type: "TEXT", nullable: false),
                    PRESCRICAO_id = table.Column<int>(type: "INTEGER", nullable: false),
                    MEDICAMENTO_id = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrescricoesMedicamentos", x => x.id);
                    table.ForeignKey(
                        name: "FK_PrescricoesMedicamentos_Medicamentos_MEDICAMENTO_id",
                        column: x => x.MEDICAMENTO_id,
                        principalTable: "Medicamentos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PrescricoesMedicamentos_PrescricoesGerais_PRESCRICAO_id",
                        column: x => x.PRESCRICAO_id,
                        principalTable: "PrescricoesGerais",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pacientes_SETOR_id",
                table: "Pacientes",
                column: "SETOR_id");

            migrationBuilder.CreateIndex(
                name: "IX_PrescricoesGerais_MEDICO_id",
                table: "PrescricoesGerais",
                column: "MEDICO_id");

            migrationBuilder.CreateIndex(
                name: "IX_PrescricoesGerais_PACIENTE_id",
                table: "PrescricoesGerais",
                column: "PACIENTE_id");

            migrationBuilder.CreateIndex(
                name: "IX_PrescricoesMedicamentos_MEDICAMENTO_id",
                table: "PrescricoesMedicamentos",
                column: "MEDICAMENTO_id");

            migrationBuilder.CreateIndex(
                name: "IX_PrescricoesMedicamentos_PRESCRICAO_id",
                table: "PrescricoesMedicamentos",
                column: "PRESCRICAO_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PrescricoesMedicamentos");

            migrationBuilder.DropTable(
                name: "Medicamentos");

            migrationBuilder.DropTable(
                name: "PrescricoesGerais");

            migrationBuilder.DropTable(
                name: "Medicos");

            migrationBuilder.DropTable(
                name: "Pacientes");

            migrationBuilder.DropTable(
                name: "Setores");
        }
    }
}
