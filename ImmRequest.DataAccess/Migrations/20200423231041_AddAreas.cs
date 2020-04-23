using Microsoft.EntityFrameworkCore.Migrations;

namespace ImmRequest.DataAccess.Migrations
{
    public partial class AddAreas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO dbo.Areas (Name) VALUES ('Espacios Publicos y Calles')");
            migrationBuilder.Sql("INSERT INTO dbo.Areas (Name) VALUES ('Limpieza')");
            migrationBuilder.Sql("INSERT INTO dbo.Areas (Name) VALUES ('Saneamiento')");
            migrationBuilder.Sql("INSERT INTO dbo.Areas (Name) VALUES ('Transporte')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM dbo.Areas");
        }
    }
}
