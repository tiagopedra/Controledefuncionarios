using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControleDeFuncionarios.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cargos",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    nome = table.Column<string>(type: "TEXT", nullable: false),
                    descricao = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cargos", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "DadosBancarios",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    banco = table.Column<string>(type: "TEXT", nullable: false),
                    agencia = table.Column<string>(type: "TEXT", nullable: false),
                    conta = table.Column<string>(type: "TEXT", nullable: false),
                    tipoConta = table.Column<string>(type: "TEXT", nullable: false),
                    cpfTitular = table.Column<string>(type: "TEXT", nullable: false),
                    nomeTitular = table.Column<string>(type: "TEXT", nullable: false),
                    telefoneTitular = table.Column<string>(type: "TEXT", nullable: false),
                    emailTitular = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DadosBancarios", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Enderecos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TipoLogradouro = table.Column<string>(type: "TEXT", nullable: false),
                    Logradouro = table.Column<string>(type: "TEXT", nullable: false),
                    Numero = table.Column<string>(type: "TEXT", nullable: false),
                    Complemento = table.Column<string>(type: "TEXT", nullable: false),
                    Bairro = table.Column<string>(type: "TEXT", nullable: false),
                    Estado = table.Column<string>(type: "TEXT", nullable: false),
                    Cep = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enderecos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pessoas",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    nome = table.Column<string>(type: "TEXT", nullable: false),
                    dataNascimento = table.Column<DateTime>(type: "TEXT", nullable: false),
                    cpf = table.Column<string>(type: "TEXT", nullable: false),
                    sexo = table.Column<string>(type: "TEXT", nullable: false),
                    telefone = table.Column<string>(type: "TEXT", nullable: false),
                    email = table.Column<string>(type: "TEXT", nullable: false),
                    enderecoID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pessoas", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Setores",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    nome = table.Column<string>(type: "TEXT", nullable: false),
                    descricao = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Setores", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Funcionarios",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    pessoaId = table.Column<int>(type: "INTEGER", nullable: false),
                    setorId = table.Column<int>(type: "INTEGER", nullable: false),
                    salario = table.Column<decimal>(type: "TEXT", nullable: false),
                    cargoId = table.Column<int>(type: "INTEGER", nullable: false),
                    dadosBancariosId = table.Column<int>(type: "INTEGER", nullable: false),
                    enderecoID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funcionarios", x => x.id);
                    table.ForeignKey(
                        name: "FK_Funcionarios_Cargos_cargoId",
                        column: x => x.cargoId,
                        principalTable: "Cargos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Funcionarios_DadosBancarios_dadosBancariosId",
                        column: x => x.dadosBancariosId,
                        principalTable: "DadosBancarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Funcionarios_Enderecos_enderecoID",
                        column: x => x.enderecoID,
                        principalTable: "Enderecos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Funcionarios_Pessoas_pessoaId",
                        column: x => x.pessoaId,
                        principalTable: "Pessoas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Funcionarios_Setores_setorId",
                        column: x => x.setorId,
                        principalTable: "Setores",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Funcionarios_cargoId",
                table: "Funcionarios",
                column: "cargoId");

            migrationBuilder.CreateIndex(
                name: "IX_Funcionarios_dadosBancariosId",
                table: "Funcionarios",
                column: "dadosBancariosId");

            migrationBuilder.CreateIndex(
                name: "IX_Funcionarios_enderecoID",
                table: "Funcionarios",
                column: "enderecoID");

            migrationBuilder.CreateIndex(
                name: "IX_Funcionarios_pessoaId",
                table: "Funcionarios",
                column: "pessoaId");

            migrationBuilder.CreateIndex(
                name: "IX_Funcionarios_setorId",
                table: "Funcionarios",
                column: "setorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Funcionarios");

            migrationBuilder.DropTable(
                name: "Cargos");

            migrationBuilder.DropTable(
                name: "DadosBancarios");

            migrationBuilder.DropTable(
                name: "Enderecos");

            migrationBuilder.DropTable(
                name: "Pessoas");

            migrationBuilder.DropTable(
                name: "Setores");
        }
    }
}
