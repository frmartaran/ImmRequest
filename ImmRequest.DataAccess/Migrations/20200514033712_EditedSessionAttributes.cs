using Microsoft.EntityFrameworkCore.Migrations;

namespace ImmRequest.DataAccess.Migrations
{
    public partial class EditedSessionAttributes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Administrators_AdministratorId",
                table: "Sessions");

            migrationBuilder.DropIndex(
                name: "IX_Sessions_AdministratorId",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "AdministratorId",
                table: "Sessions");

            migrationBuilder.RenameColumn(
                name: "PassWord",
                table: "Administrators",
                newName: "Password");

            migrationBuilder.AddColumn<long>(
                name: "AdministratorInSessionId",
                table: "Sessions",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_AdministratorInSessionId",
                table: "Sessions",
                column: "AdministratorInSessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Administrators_AdministratorInSessionId",
                table: "Sessions",
                column: "AdministratorInSessionId",
                principalTable: "Administrators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Administrators_AdministratorInSessionId",
                table: "Sessions");

            migrationBuilder.DropIndex(
                name: "IX_Sessions_AdministratorInSessionId",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "AdministratorInSessionId",
                table: "Sessions");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Administrators",
                newName: "PassWord");

            migrationBuilder.AddColumn<long>(
                name: "AdministratorId",
                table: "Sessions",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_AdministratorId",
                table: "Sessions",
                column: "AdministratorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Administrators_AdministratorId",
                table: "Sessions",
                column: "AdministratorId",
                principalTable: "Administrators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
