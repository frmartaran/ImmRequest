using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ImmRequest.DataAccess.Migrations
{
    public partial class AddSoftDeleteAndValuesToRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CitizenRequests_Areas_AreaId",
                table: "CitizenRequests");

            migrationBuilder.DropColumn(
                name: "RequestId",
                table: "TopicTypes");

            migrationBuilder.DropColumn(
                name: "RequestId",
                table: "Fields");

            migrationBuilder.DropColumn(
                name: "RequestId",
                table: "Areas");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "TopicTypes",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "TopicTypes",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "Fields",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Fields",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "TopicId",
                table: "CitizenRequests",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "TopicTypeId",
                table: "CitizenRequests",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "RequestFieldValues",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CitizenRequestId = table.Column<long>(nullable: false),
                    FieldId = table.Column<long>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestFieldValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestFieldValues_CitizenRequests_CitizenRequestId",
                        column: x => x.CitizenRequestId,
                        principalTable: "CitizenRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CitizenRequests_TopicId",
                table: "CitizenRequests",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_CitizenRequests_TopicTypeId",
                table: "CitizenRequests",
                column: "TopicTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestFieldValues_CitizenRequestId",
                table: "RequestFieldValues",
                column: "CitizenRequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_CitizenRequests_Areas_AreaId",
                table: "CitizenRequests",
                column: "AreaId",
                principalTable: "Areas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CitizenRequests_Topics_TopicId",
                table: "CitizenRequests",
                column: "TopicId",
                principalTable: "Topics",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CitizenRequests_TopicTypes_TopicTypeId",
                table: "CitizenRequests",
                column: "TopicTypeId",
                principalTable: "TopicTypes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CitizenRequests_Areas_AreaId",
                table: "CitizenRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_CitizenRequests_Topics_TopicId",
                table: "CitizenRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_CitizenRequests_TopicTypes_TopicTypeId",
                table: "CitizenRequests");

            migrationBuilder.DropTable(
                name: "RequestFieldValues");

            migrationBuilder.DropIndex(
                name: "IX_CitizenRequests_TopicId",
                table: "CitizenRequests");

            migrationBuilder.DropIndex(
                name: "IX_CitizenRequests_TopicTypeId",
                table: "CitizenRequests");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "TopicTypes");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "TopicTypes");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "Fields");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Fields");

            migrationBuilder.DropColumn(
                name: "TopicId",
                table: "CitizenRequests");

            migrationBuilder.DropColumn(
                name: "TopicTypeId",
                table: "CitizenRequests");

            migrationBuilder.AddColumn<long>(
                name: "RequestId",
                table: "TopicTypes",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "RequestId",
                table: "Fields",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "RequestId",
                table: "Areas",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddForeignKey(
                name: "FK_CitizenRequests_Areas_AreaId",
                table: "CitizenRequests",
                column: "AreaId",
                principalTable: "Areas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
