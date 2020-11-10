using Microsoft.EntityFrameworkCore.Migrations;

namespace Splitwise.DomainModels.Migrations
{
    public partial class Settelementidremovedfromtransaction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Settelements_SettelementId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_SettelementId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "SettelementId",
                table: "Payments");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SettelementId",
                table: "Payments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Payments_SettelementId",
                table: "Payments",
                column: "SettelementId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Settelements_SettelementId",
                table: "Payments",
                column: "SettelementId",
                principalTable: "Settelements",
                principalColumn: "SettelementId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
