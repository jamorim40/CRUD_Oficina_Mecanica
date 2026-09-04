using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mecanica.Migrations
{
    /// <inheritdoc />
    public partial class addCpfCnpj : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CpfCnpj",
                table: "Clientes",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_CpfCnpj",
                table: "Clientes",
                column: "CpfCnpj",
                unique: true,
                filter: "[CpfCnpj] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Clientes_CpfCnpj",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "CpfCnpj",
                table: "Clientes");
        }
    }
}
