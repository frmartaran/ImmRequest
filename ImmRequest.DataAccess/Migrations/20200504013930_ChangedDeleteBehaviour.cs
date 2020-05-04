using Microsoft.EntityFrameworkCore.Migrations;

namespace ImmRequest.DataAccess.Migrations
{
    public partial class ChangedDeleteBehaviour : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CitizenRequests_TopicTypes_TopicTypeId",
                table: "CitizenRequests");

            migrationBuilder.AddForeignKey(
                name: "FK_CitizenRequests_TopicTypes_TopicTypeId",
                table: "CitizenRequests",
                column: "TopicTypeId",
                principalTable: "TopicTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CitizenRequests_TopicTypes_TopicTypeId",
                table: "CitizenRequests");

            migrationBuilder.AddForeignKey(
                name: "FK_CitizenRequests_TopicTypes_TopicTypeId",
                table: "CitizenRequests",
                column: "TopicTypeId",
                principalTable: "TopicTypes",
                principalColumn: "Id");

        }
    }
}
