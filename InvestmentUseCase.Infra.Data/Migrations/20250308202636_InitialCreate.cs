using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace InvestmentUseCase.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Agency = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Account = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    DAC = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InvestmentProducts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvestmentProducts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Investments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    InvestmentProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: false),
                    InvestedCapital = table.Column<decimal>(type: "numeric(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Investments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Investments_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Investments_InvestmentProducts_InvestmentProductId",
                        column: x => x.InvestmentProductId,
                        principalTable: "InvestmentProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Account", "Agency", "DAC", "Email", "Name" },
                values: new object[,]
                {
                    { new Guid("16aecf48-db83-479f-9556-913414c407bf"), "98765", "4321", "2", "maria_santos@gmail.com", "Maria dos Santos" },
                    { new Guid("5dfa53c1-973f-41d4-9366-cbbb2922b22e"), "56789", "1234", "1", "joao_silva@gmail.com", "João Silva" }
                });

            migrationBuilder.InsertData(
                table: "InvestmentProducts",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[,]
                {
                    { new Guid("0303d605-a32a-4587-b641-160f008e5d14"), "CDB002", "CDB Resgate Diário" },
                    { new Guid("cd939f0a-fc4b-423c-b001-f7a558ba483d"), "TD001", "Tesouro Direto" }
                });

            migrationBuilder.InsertData(
                table: "Investments",
                columns: new[] { "Id", "CustomerId", "InvestedCapital", "InvestmentProductId" },
                values: new object[,]
                {
                    { new Guid("627fffb3-1c2d-4272-851f-aa346f48b809"), new Guid("16aecf48-db83-479f-9556-913414c407bf"), 5000.00m, new Guid("0303d605-a32a-4587-b641-160f008e5d14") },
                    { new Guid("642161a6-92d1-4df9-99e8-8f2a99bcf458"), new Guid("5dfa53c1-973f-41d4-9366-cbbb2922b22e"), 5000.00m, new Guid("0303d605-a32a-4587-b641-160f008e5d14") },
                    { new Guid("bff7a047-04d6-4cd0-9724-f316fc958f68"), new Guid("5dfa53c1-973f-41d4-9366-cbbb2922b22e"), 1000.00m, new Guid("cd939f0a-fc4b-423c-b001-f7a558ba483d") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customers_Account",
                table: "Customers",
                column: "Account");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_Agency",
                table: "Customers",
                column: "Agency");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_DAC",
                table: "Customers",
                column: "DAC");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_Name",
                table: "Customers",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_InvestmentProducts_Name",
                table: "InvestmentProducts",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Investments_CustomerId",
                table: "Investments",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Investments_InvestmentProductId",
                table: "Investments",
                column: "InvestmentProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Investments");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "InvestmentProducts");
        }
    }
}
