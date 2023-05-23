using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repositorio.Migrations
{
    public partial class Migracao1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Compra",
                columns: table => new
                {
                    id_compra = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    data = table.Column<DateTime>(type: "datetime", nullable: true),
                    id_status = table.Column<int>(type: "int", nullable: true),
                    valor = table.Column<decimal>(type: "decimal(8,2)", nullable: true),
                    id_preferencia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    url = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carrinho", x => x.id_compra);
                });

            migrationBuilder.CreateTable(
                name: "Genero",
                columns: table => new
                {
                    id_genero = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nome = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    descricao = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genero", x => x.id_genero);
                });

            migrationBuilder.CreateTable(
                name: "Idioma",
                columns: table => new
                {
                    id_idioma = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nome = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Idioma", x => x.id_idioma);
                });

            migrationBuilder.CreateTable(
                name: "Status_Compra",
                columns: table => new
                {
                    id_status = table.Column<int>(type: "int", nullable: false),
                    descricao = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status_Compra", x => x.id_status);
                });

            migrationBuilder.CreateTable(
                name: "Tipo_Usuario",
                columns: table => new
                {
                    id_tipousuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    descricao = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tipo_Usuario", x => x.id_tipousuario);
                });

            migrationBuilder.CreateTable(
                name: "Filme",
                columns: table => new
                {
                    id_filme = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nome = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    descricao = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    duracao = table.Column<int>(type: "int", nullable: true),
                    ano_lancamento = table.Column<string>(type: "varchar(4)", unicode: false, maxLength: 4, nullable: true),
                    imagem = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    id_idioma = table.Column<int>(type: "int", nullable: true),
                    valor = table.Column<decimal>(type: "decimal(8,2)", nullable: true),
                    id_genero = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Filme", x => x.id_filme);
                    table.ForeignKey(
                        name: "FK_Filme_Genero",
                        column: x => x.id_genero,
                        principalTable: "Genero",
                        principalColumn: "id_genero",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Filme_Idioma",
                        column: x => x.id_idioma,
                        principalTable: "Idioma",
                        principalColumn: "id_idioma",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    id_usuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    senha = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true),
                    nome = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    id_tipousuario = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.id_usuario);
                    table.ForeignKey(
                        name: "FK_Usuario_Tipo_Usuario",
                        column: x => x.id_tipousuario,
                        principalTable: "Tipo_Usuario",
                        principalColumn: "id_tipousuario",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Compra_Filme",
                columns: table => new
                {
                    id_compra_filme = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    quantidade = table.Column<int>(type: "int", nullable: true),
                    valor = table.Column<decimal>(type: "decimal(8,2)", nullable: true),
                    id_compra = table.Column<int>(type: "int", nullable: true),
                    id_filme = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Compra_Filme", x => x.id_compra_filme);
                    table.ForeignKey(
                        name: "FK_CompraFilme_Compra",
                        column: x => x.id_compra,
                        principalTable: "Compra",
                        principalColumn: "id_compra",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompraFilme_Filme",
                        column: x => x.id_filme,
                        principalTable: "Filme",
                        principalColumn: "id_filme",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Compra_Filme_id_compra",
                table: "Compra_Filme",
                column: "id_compra");

            migrationBuilder.CreateIndex(
                name: "IX_Compra_Filme_id_filme",
                table: "Compra_Filme",
                column: "id_filme");

            migrationBuilder.CreateIndex(
                name: "IX_Filme_id_genero",
                table: "Filme",
                column: "id_genero");

            migrationBuilder.CreateIndex(
                name: "IX_Filme_id_idioma",
                table: "Filme",
                column: "id_idioma");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_id_tipousuario",
                table: "Usuario",
                column: "id_tipousuario");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Compra_Filme");

            migrationBuilder.DropTable(
                name: "Status_Compra");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Compra");

            migrationBuilder.DropTable(
                name: "Filme");

            migrationBuilder.DropTable(
                name: "Tipo_Usuario");

            migrationBuilder.DropTable(
                name: "Genero");

            migrationBuilder.DropTable(
                name: "Idioma");
        }
    }
}
