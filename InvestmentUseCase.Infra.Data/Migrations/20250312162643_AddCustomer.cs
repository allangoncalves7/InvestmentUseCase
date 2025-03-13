using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvestmentUseCase.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCustomer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Account", "Agency", "DAC", "Email", "Name" },
                values: new object[] { new Guid("cfa5377c-b839-45f0-8ed3-c5c6fc067a8b"), "678980", "9876", "5", "francisco@gmail.com", "Francisco Oliveira" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("cfa5377c-b839-45f0-8ed3-c5c6fc067a8b"));
        }
    }
}
