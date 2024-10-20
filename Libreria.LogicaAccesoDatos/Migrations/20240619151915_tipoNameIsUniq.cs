using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Libreria.LogicaAccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class tipoNameIsUniq : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TipoMovimientos_Nombre",
                table: "TipoMovimientos");

            migrationBuilder.CreateIndex(
                name: "IX_TipoMovimientos_Nombre",
                table: "TipoMovimientos",
                column: "Nombre",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TipoMovimientos_Nombre",
                table: "TipoMovimientos");

            migrationBuilder.CreateIndex(
                name: "IX_TipoMovimientos_Nombre",
                table: "TipoMovimientos",
                column: "Nombre");
        }
    }
}
