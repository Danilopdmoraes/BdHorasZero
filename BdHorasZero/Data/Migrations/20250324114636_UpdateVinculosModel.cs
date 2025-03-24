using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BdHorasZero.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateVinculosModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TB_Vinculos_IdFuncionario",
                table: "TB_Vinculos");

            migrationBuilder.CreateIndex(
                name: "IX_TB_Vinculos_IdFuncionario",
                table: "TB_Vinculos",
                column: "IdFuncionario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TB_Vinculos_IdFuncionario",
                table: "TB_Vinculos");

            migrationBuilder.CreateIndex(
                name: "IX_TB_Vinculos_IdFuncionario",
                table: "TB_Vinculos",
                column: "IdFuncionario",
                unique: true);
        }
    }
}
