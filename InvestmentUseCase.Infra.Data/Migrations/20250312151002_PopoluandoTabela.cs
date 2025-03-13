using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace InvestmentUseCase.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class PopoluandoTabela : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Account", "Agency", "DAC", "Email", "Name" },
                values: new object[,]
                {
                    { new Guid("a7291cb8-e069-4a75-b53c-f0d139a1e442"), "54321", "8765", "4", "ana_s@gmail.com", "Ana Souza" },
                    { new Guid("edda9d43-96a1-497b-bd1e-d0f2385f47f5"), "12345", "5678", "3", "jose.p@gmail.com", "José Pereira" }
                });

            migrationBuilder.InsertData(
                table: "InvestmentProducts",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[,]
                {
                    { new Guid("0c62f3db-1214-48d0-98b0-fdcc9ffc93fe"), "LCI004", "LCI Pré Fixado" },
                    { new Guid("41ef04f6-83c9-46b5-bd38-f08f91b1a66e"), "CDB003", "CDB Pós Fixado" }
                });

            migrationBuilder.InsertData(
                table: "Investments",
                columns: new[] { "Id", "CustomerId", "InvestedCapital", "InvestmentProductId" },
                values: new object[,]
                {
                    { new Guid("0c11f32e-7543-4601-8056-7ef89cd775d8"), new Guid("a7291cb8-e069-4a75-b53c-f0d139a1e442"), 2000.00m, new Guid("0c62f3db-1214-48d0-98b0-fdcc9ffc93fe") },
                    { new Guid("cd2a8bc4-d995-46eb-9a11-b65bfa566be7"), new Guid("edda9d43-96a1-497b-bd1e-d0f2385f47f5"), 3000.00m, new Guid("41ef04f6-83c9-46b5-bd38-f08f91b1a66e") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Investments",
                keyColumn: "Id",
                keyValue: new Guid("0c11f32e-7543-4601-8056-7ef89cd775d8"));

            migrationBuilder.DeleteData(
                table: "Investments",
                keyColumn: "Id",
                keyValue: new Guid("cd2a8bc4-d995-46eb-9a11-b65bfa566be7"));

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("a7291cb8-e069-4a75-b53c-f0d139a1e442"));

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("edda9d43-96a1-497b-bd1e-d0f2385f47f5"));

            migrationBuilder.DeleteData(
                table: "InvestmentProducts",
                keyColumn: "Id",
                keyValue: new Guid("0c62f3db-1214-48d0-98b0-fdcc9ffc93fe"));

            migrationBuilder.DeleteData(
                table: "InvestmentProducts",
                keyColumn: "Id",
                keyValue: new Guid("41ef04f6-83c9-46b5-bd38-f08f91b1a66e"));
        }
    }
}
