using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mecanica.Migrations
{
    /// <inheritdoc />
    public partial class CriacaoDePermissoes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GrupoPermissoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GrupoPermissoes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permissoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissoes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioGrupoPermissoes",
                columns: table => new
                {
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    GrupoPermissaoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioGrupoPermissoes", x => new { x.UsuarioId, x.GrupoPermissaoId });
                    table.ForeignKey(
                        name: "FK_UsuarioGrupoPermissoes_GrupoPermissoes_GrupoPermissaoId",
                        column: x => x.GrupoPermissaoId,
                        principalTable: "GrupoPermissoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UsuarioGrupoPermissoes_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GrupoPermissaoItens",
                columns: table => new
                {
                    GrupoPermissaoId = table.Column<int>(type: "int", nullable: false),
                    PermissaoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GrupoPermissaoItens", x => new { x.GrupoPermissaoId, x.PermissaoId });
                    table.ForeignKey(
                        name: "FK_GrupoPermissaoItens_GrupoPermissoes_GrupoPermissaoId",
                        column: x => x.GrupoPermissaoId,
                        principalTable: "GrupoPermissoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GrupoPermissaoItens_Permissoes_PermissaoId",
                        column: x => x.PermissaoId,
                        principalTable: "Permissoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GrupoPermissaoItens_PermissaoId",
                table: "GrupoPermissaoItens",
                column: "PermissaoId");

            migrationBuilder.CreateIndex(
                name: "IX_GrupoPermissoes_Nome",
                table: "GrupoPermissoes",
                column: "Nome",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Permissoes_Codigo",
                table: "Permissoes",
                column: "Codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioGrupoPermissoes_GrupoPermissaoId",
                table: "UsuarioGrupoPermissoes",
                column: "GrupoPermissaoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GrupoPermissaoItens");

            migrationBuilder.DropTable(
                name: "UsuarioGrupoPermissoes");

            migrationBuilder.DropTable(
                name: "Permissoes");

            migrationBuilder.DropTable(
                name: "GrupoPermissoes");
        }
    }
}
