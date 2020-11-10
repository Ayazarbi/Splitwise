using Microsoft.EntityFrameworkCore.Migrations;

namespace Splitwise.DomainModels.Migrations
{
    public partial class Changingexpenseidtoallownullinseetelements : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Settelements_Expenses_ExpenseId",
                table: "Settelements");

            migrationBuilder.AlterColumn<int>(
                name: "ExpenseId",
                table: "Settelements",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Settelements_Expenses_ExpenseId",
                table: "Settelements",
                column: "ExpenseId",
                principalTable: "Expenses",
                principalColumn: "ExpenseId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Settelements_Expenses_ExpenseId",
                table: "Settelements");

            migrationBuilder.AlterColumn<int>(
                name: "ExpenseId",
                table: "Settelements",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Settelements_Expenses_ExpenseId",
                table: "Settelements",
                column: "ExpenseId",
                principalTable: "Expenses",
                principalColumn: "ExpenseId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
