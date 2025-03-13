using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvestmentUseCase.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class Ajuste_Investment_Product : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "InvestmentProducts",
                type: "character varying(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_InvestmentProducts_Code",
                table: "InvestmentProducts",
                column: "Code");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_InvestmentProducts_Code",
                table: "InvestmentProducts");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "InvestmentProducts",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(10)",
                oldMaxLength: 10);
        }
    }
}
