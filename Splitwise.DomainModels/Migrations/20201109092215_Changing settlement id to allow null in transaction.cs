using Microsoft.EntityFrameworkCore.Migrations;

namespace Splitwise.DomainModels.Migrations
{
    public partial class Changingsettlementidtoallownullintransaction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Settelements_SettelementId",
                table: "Payments");

            migrationBuilder.AlterColumn<int>(
                name: "SettelementId",
                table: "Payments",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Settelements_SettelementId",
                table: "Payments",
                column: "SettelementId",
                principalTable: "Settelements",
                principalColumn: "SettelementId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Settelements_SettelementId",
                table: "Payments");

            migrationBuilder.AlterColumn<int>(
                name: "SettelementId",
                table: "Payments",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Settelements_SettelementId",
                table: "Payments",
                column: "SettelementId",
                principalTable: "Settelements",
                principalColumn: "SettelementId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
