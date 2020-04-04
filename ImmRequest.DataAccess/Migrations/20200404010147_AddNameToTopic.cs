using Microsoft.EntityFrameworkCore.Migrations;

namespace ImmRequest.DataAccess.Migrations
{
    public partial class AddNameToTopic : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequestId",
                table: "Topics");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Topics",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Topics");

            migrationBuilder.AddColumn<long>(
                name: "RequestId",
                table: "Topics",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
