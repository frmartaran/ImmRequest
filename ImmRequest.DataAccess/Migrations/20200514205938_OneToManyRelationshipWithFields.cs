using Microsoft.EntityFrameworkCore.Migrations;

namespace ImmRequest.DataAccess.Migrations
{
    public partial class OneToManyRelationshipWithFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RequestFieldValues_FieldId",
                table: "RequestFieldValues");

            migrationBuilder.CreateIndex(
                name: "IX_RequestFieldValues_FieldId",
                table: "RequestFieldValues",
                column: "FieldId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RequestFieldValues_FieldId",
                table: "RequestFieldValues");

            migrationBuilder.CreateIndex(
                name: "IX_RequestFieldValues_FieldId",
                table: "RequestFieldValues",
                column: "FieldId",
                unique: true);
        }
    }
}
