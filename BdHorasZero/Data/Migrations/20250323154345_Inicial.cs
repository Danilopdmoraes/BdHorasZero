using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BdHorasZero.Data.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_Funcionarios",
                columns: table => new
                {
                    IdFuncionario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdExclusivo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MatriculaFuncionario = table.Column<int>(type: "int", nullable: false),
                    NomeFuncionario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailFuncionario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NomeGrupo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PerfilFuncionario = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Funcionarios", x => x.IdFuncionario);
                });

            migrationBuilder.CreateTable(
                name: "TB_Gestores",
                columns: table => new
                {
                    IdGestor = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdExclusivo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NomeGestor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailGestor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NomeGrupo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Gestores", x => x.IdGestor);
                });

            migrationBuilder.CreateTable(
                name: "TB_Ocorrencias",
                columns: table => new
                {
                    IdOcorrencia = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdGestor = table.Column<int>(type: "int", nullable: false),
                    IdFuncionario = table.Column<int>(type: "int", nullable: false),
                    DataOcorrencia = table.Column<DateOnly>(type: "date", nullable: false),
                    QtdHorasOcorrencia = table.Column<TimeOnly>(type: "time", nullable: false),
                    TipoOcorrencia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Observacao = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Ocorrencias", x => x.IdOcorrencia);
                    table.ForeignKey(
                        name: "FK_TB_Ocorrencias_TB_Funcionarios_IdFuncionario",
                        column: x => x.IdFuncionario,
                        principalTable: "TB_Funcionarios",
                        principalColumn: "IdFuncionario",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TB_Ocorrencias_TB_Gestores_IdGestor",
                        column: x => x.IdGestor,
                        principalTable: "TB_Gestores",
                        principalColumn: "IdGestor",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_Vinculos",
                columns: table => new
                {
                    IdVinculo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdGestor = table.Column<int>(type: "int", nullable: false),
                    IdFuncionario = table.Column<int>(type: "int", nullable: false),
                    DataInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataFim = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Vinculos", x => x.IdVinculo);
                    table.ForeignKey(
                        name: "FK_TB_Vinculos_TB_Funcionarios_IdFuncionario",
                        column: x => x.IdFuncionario,
                        principalTable: "TB_Funcionarios",
                        principalColumn: "IdFuncionario",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TB_Vinculos_TB_Gestores_IdGestor",
                        column: x => x.IdGestor,
                        principalTable: "TB_Gestores",
                        principalColumn: "IdGestor",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_Ocorrencias_IdFuncionario",
                table: "TB_Ocorrencias",
                column: "IdFuncionario");

            migrationBuilder.CreateIndex(
                name: "IX_TB_Ocorrencias_IdGestor",
                table: "TB_Ocorrencias",
                column: "IdGestor");

            migrationBuilder.CreateIndex(
                name: "IX_TB_Vinculos_IdFuncionario",
                table: "TB_Vinculos",
                column: "IdFuncionario",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TB_Vinculos_IdGestor",
                table: "TB_Vinculos",
                column: "IdGestor");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_Ocorrencias");

            migrationBuilder.DropTable(
                name: "TB_Vinculos");

            migrationBuilder.DropTable(
                name: "TB_Funcionarios");

            migrationBuilder.DropTable(
                name: "TB_Gestores");
        }
    }
}
