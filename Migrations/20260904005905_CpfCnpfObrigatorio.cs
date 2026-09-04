using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mecanica.Migrations
{
    /// <inheritdoc />
    public partial class CpfCnpfObrigatorio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Clientes_CpfCnpj",
                table: "Clientes");

            migrationBuilder.AlterColumn<string>(
                name: "CpfCnpj",
                table: "Clientes",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_CpfCnpj",
                table: "Clientes",
                column: "CpfCnpj",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Clientes_CpfCnpj",
                table: "Clientes");

            migrationBuilder.AlterColumn<string>(
                name: "CpfCnpj",
                table: "Clientes",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_CpfCnpj",
                table: "Clientes",
                column: "CpfCnpj",
                unique: true,
                filter: "[CpfCnpj] IS NOT NULL");
        }
    }
}
