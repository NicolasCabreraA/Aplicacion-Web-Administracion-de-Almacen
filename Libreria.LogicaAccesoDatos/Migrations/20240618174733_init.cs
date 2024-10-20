using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Libreria.LogicaAccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Articulos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CodigoProveedor = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articulos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Configuracion",
                columns: table => new
                {
                    Nombre = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Valor = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Configuracion", x => x.Nombre);
                });

            migrationBuilder.CreateTable(
                name: "TipoMovimientos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoMovimientos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordSE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rol = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NombreCompleto_Apellido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NombreCompleto_Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MovientoStocks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaMovimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ArticuloId = table.Column<int>(type: "int", nullable: false),
                    TipoId = table.Column<int>(type: "int", nullable: false),
                    EmailUsuario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CantidadAMover = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovientoStocks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MovientoStocks_Articulos_ArticuloId",
                        column: x => x.ArticuloId,
                        principalTable: "Articulos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovientoStocks_TipoMovimientos_TipoId",
                        column: x => x.TipoId,
                        principalTable: "TipoMovimientos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MovientoStocks_ArticuloId",
                table: "MovientoStocks",
                column: "ArticuloId");

            migrationBuilder.CreateIndex(
                name: "IX_MovientoStocks_TipoId",
                table: "MovientoStocks",
                column: "TipoId");

            migrationBuilder.CreateIndex(
                name: "IX_TipoMovimientos_Nombre",
                table: "TipoMovimientos",
                column: "Nombre");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Configuracion");

            migrationBuilder.DropTable(
                name: "MovientoStocks");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Articulos");

            migrationBuilder.DropTable(
                name: "TipoMovimientos");
        }
    }
}
