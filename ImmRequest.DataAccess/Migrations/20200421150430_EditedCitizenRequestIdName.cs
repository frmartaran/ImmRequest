using Microsoft.EntityFrameworkCore.Migrations;

namespace ImmRequest.DataAccess.Migrations
{
    public partial class EditedCitizenRequestIdName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequestFieldValues_CitizenRequests_CitizenRequestId",
                table: "RequestFieldValues");

            migrationBuilder.DropIndex(
                name: "IX_RequestFieldValues_CitizenRequestId",
                table: "RequestFieldValues");

            migrationBuilder.DropColumn(
                name: "CitizenRequestId",
                table: "RequestFieldValues");

            migrationBuilder.AddColumn<long>(
                name: "ParentCitizenRequestId",
                table: "RequestFieldValues",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_RequestFieldValues_ParentCitizenRequestId",
                table: "RequestFieldValues",
                column: "ParentCitizenRequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_RequestFieldValues_CitizenRequests_ParentCitizenRequestId",
                table: "RequestFieldValues",
                column: "ParentCitizenRequestId",
                principalTable: "CitizenRequests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequestFieldValues_CitizenRequests_ParentCitizenRequestId",
                table: "RequestFieldValues");

            migrationBuilder.DropIndex(
                name: "IX_RequestFieldValues_ParentCitizenRequestId",
                table: "RequestFieldValues");

            migrationBuilder.DropColumn(
                name: "ParentCitizenRequestId",
                table: "RequestFieldValues");

            migrationBuilder.AddColumn<long>(
                name: "CitizenRequestId",
                table: "RequestFieldValues",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_RequestFieldValues_CitizenRequestId",
                table: "RequestFieldValues",
                column: "CitizenRequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_RequestFieldValues_CitizenRequests_CitizenRequestId",
                table: "RequestFieldValues",
                column: "CitizenRequestId",
                principalTable: "CitizenRequests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
