using Microsoft.EntityFrameworkCore.Migrations;

namespace ImmRequest.DataAccess.Migrations
{
    public partial class AddDefaultAdministrator : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO dbo.Administrators (UserName, PassWord, Email) VALUES ('Administrator', '1234', 'administrator@global.com')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM dbo.Administrators WHERE Email = 'administrator@global.com'");
        }
    }
}
