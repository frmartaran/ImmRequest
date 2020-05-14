using Microsoft.EntityFrameworkCore.Migrations;

namespace ImmRequest.DataAccess.Migrations
{
    public partial class AddedFieldToRequestFieldValues : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_RequestFieldValues_FieldId",
                table: "RequestFieldValues",
                column: "FieldId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RequestFieldValues_Fields_FieldId",
                table: "RequestFieldValues",
                column: "FieldId",
                principalTable: "Fields",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequestFieldValues_Fields_FieldId",
                table: "RequestFieldValues");

            migrationBuilder.DropIndex(
                name: "IX_RequestFieldValues_FieldId",
                table: "RequestFieldValues");

            migrationBuilder.CreateIndex(
                name: "IX_RequestFieldValues_FieldId",
                table: "RequestFieldValues",
                column: "FieldId");

            migrationBuilder.AddForeignKey(
                name: "FK_RequestFieldValues_Fields_FieldId",
                table: "RequestFieldValues",
                column: "FieldId",
                principalTable: "Fields",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
