using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SGCC_API.Migrations
{
    public partial class MigrationInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Empresa",
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
                    table.PrimaryKey("PK_Empresa", x => x.IdEmpresa);
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
                        name: "FK_Locais_Empresa_LocadorIdEmpresa",
                        column: x => x.LocadorIdEmpresa,
                        principalTable: "Empresa",
                        principalColumn: "IdEmpresa",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Locais_Empresa_LocatarioIdEmpresa",
                        column: x => x.LocatarioIdEmpresa,
                        principalTable: "Empresa",
                        principalColumn: "IdEmpresa",
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Locais");

            migrationBuilder.DropTable(
                name: "Visitantes");

            migrationBuilder.DropTable(
                name: "Empresa");
        }
    }
}
