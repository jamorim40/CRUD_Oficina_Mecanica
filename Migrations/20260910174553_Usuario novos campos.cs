using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mecanica.Migrations
{
    /// <inheritdoc />
    public partial class Usuarionovoscampos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "PrimeiroAcesso",
                table: "Usuarios",
                type: "bit",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<bool>(
                name: "Bloqueado",
                table: "Usuarios",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataPrimeiroAcesso",
                table: "Usuarios",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataUltimoAcesso",
                table: "Usuarios",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TentativasLogin",
                table: "Usuarios",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataPrimeiroAcesso",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "DataUltimoAcesso",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "TentativasLogin",
                table: "Usuarios");

            migrationBuilder.AlterColumn<bool>(
                name: "PrimeiroAcesso",
                table: "Usuarios",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Bloqueado",
                table: "Usuarios",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);
        }
    }
}
