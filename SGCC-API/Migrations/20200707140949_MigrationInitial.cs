using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SGCC_API.Migrations
{
    public partial class MigrationInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Empresas",
                columns: table => new
                {
                    IdEmpresa = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NomeReal = table.Column<string>(nullable: true),
                    NomeFantasia = table.Column<string>(nullable: true),
                    Cnpj = table.Column<string>(nullable: true),
                    Telefone = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    AgenciaBancaria = table.Column<string>(nullable: true),
                    ContaBancaria = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empresas", x => x.IdEmpresa);
                });

            migrationBuilder.CreateTable(
                name: "Recepcoes",
                columns: table => new
                {
                    IdRecepcao = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NomeEntrada = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recepcoes", x => x.IdRecepcao);
                });

            migrationBuilder.CreateTable(
                name: "Visitantes",
                columns: table => new
                {
                    IdVisitante = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TipoPessoa = table.Column<int>(nullable: false),
                    Nome = table.Column<string>(nullable: true),
                    Cpf = table.Column<string>(nullable: true),
                    DataNasc = table.Column<string>(nullable: true),
                    Telefone = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visitantes", x => x.IdVisitante);
                });

            migrationBuilder.CreateTable(
                name: "Locais",
                columns: table => new
                {
                    IdLocal = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Andar = table.Column<int>(nullable: false),
                    Numero = table.Column<int>(nullable: false),
                    TamanhoM2 = table.Column<float>(nullable: false),
                    Valor = table.Column<double>(nullable: false),
                    LocatarioIdEmpresa = table.Column<int>(nullable: true),
                    LocadorIdEmpresa = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locais", x => x.IdLocal);
                    table.ForeignKey(
                        name: "FK_Locais_Empresas_LocadorIdEmpresa",
                        column: x => x.LocadorIdEmpresa,
                        principalTable: "Empresas",
                        principalColumn: "IdEmpresa",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Locais_Empresas_LocatarioIdEmpresa",
                        column: x => x.LocatarioIdEmpresa,
                        principalTable: "Empresas",
                        principalColumn: "IdEmpresa",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    IdLog = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    VisitanteIdVisitante = table.Column<int>(nullable: true),
                    RecepcaoEntradaIdRecepcao = table.Column<int>(nullable: true),
                    dataEntrada = table.Column<DateTime>(nullable: false),
                    RecepcaoSaidaIdRecepcao = table.Column<int>(nullable: true),
                    dataSaida = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.IdLog);
                    table.ForeignKey(
                        name: "FK_Logs_Recepcoes_RecepcaoEntradaIdRecepcao",
                        column: x => x.RecepcaoEntradaIdRecepcao,
                        principalTable: "Recepcoes",
                        principalColumn: "IdRecepcao",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Logs_Recepcoes_RecepcaoSaidaIdRecepcao",
                        column: x => x.RecepcaoSaidaIdRecepcao,
                        principalTable: "Recepcoes",
                        principalColumn: "IdRecepcao",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Logs_Visitantes_VisitanteIdVisitante",
                        column: x => x.VisitanteIdVisitante,
                        principalTable: "Visitantes",
                        principalColumn: "IdVisitante",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Locais_LocadorIdEmpresa",
                table: "Locais",
                column: "LocadorIdEmpresa");

            migrationBuilder.CreateIndex(
                name: "IX_Locais_LocatarioIdEmpresa",
                table: "Locais",
                column: "LocatarioIdEmpresa");

            migrationBuilder.CreateIndex(
                name: "IX_Logs_RecepcaoEntradaIdRecepcao",
                table: "Logs",
                column: "RecepcaoEntradaIdRecepcao");

            migrationBuilder.CreateIndex(
                name: "IX_Logs_RecepcaoSaidaIdRecepcao",
                table: "Logs",
                column: "RecepcaoSaidaIdRecepcao");

            migrationBuilder.CreateIndex(
                name: "IX_Logs_VisitanteIdVisitante",
                table: "Logs",
                column: "VisitanteIdVisitante");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Locais");

            migrationBuilder.DropTable(
                name: "Logs");

            migrationBuilder.DropTable(
                name: "Empresas");

            migrationBuilder.DropTable(
                name: "Recepcoes");

            migrationBuilder.DropTable(
                name: "Visitantes");
        }
    }
}
