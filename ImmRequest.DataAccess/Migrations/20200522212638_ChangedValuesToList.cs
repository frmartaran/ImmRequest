using Microsoft.EntityFrameworkCore.Migrations;

namespace ImmRequest.DataAccess.Migrations
{
    public partial class ChangedValuesToList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Value",
                table: "RequestFieldValues");

            migrationBuilder.AddColumn<string>(
                name: "Values",
                table: "RequestFieldValues",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Values",
                table: "RequestFieldValues");

            migrationBuilder.AddColumn<string>(
                name: "Value",
                table: "RequestFieldValues",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
