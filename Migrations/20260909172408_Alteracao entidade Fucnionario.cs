using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mecanica.Migrations
{
    /// <inheritdoc />
    public partial class AlteracaoentidadeFucnionario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence<int>(
                name: "MatriculaSequence");

            migrationBuilder.AddColumn<int>(
                name: "Matricula",
                table: "Funcionarios",
                type: "int",
                nullable: false,
                defaultValueSql: "NEXT VALUE FOR MatriculaSequence");

            migrationBuilder.CreateIndex(
                name: "IX_Funcionarios_Matricula",
                table: "Funcionarios",
                column: "Matricula",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Funcionarios_Matricula",
                table: "Funcionarios");

            migrationBuilder.DropColumn(
                name: "Matricula",
                table: "Funcionarios");

            migrationBuilder.DropSequence(
                name: "MatriculaSequence");
        }
    }
}
