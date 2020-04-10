using Microsoft.EntityFrameworkCore.Migrations;

namespace ImmRequest.DataAccess.Migrations
{
    public partial class AdminEmail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Administrators",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Administrators");
        }
    }
}
