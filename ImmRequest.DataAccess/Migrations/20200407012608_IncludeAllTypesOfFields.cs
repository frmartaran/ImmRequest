using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ImmRequest.DataAccess.Migrations
{
    public partial class IncludeAllTypesOfFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "End",
                table: "Fields",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Start",
                table: "Fields",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RangeEnd",
                table: "Fields",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RangeStart",
                table: "Fields",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "End",
                table: "Fields");

            migrationBuilder.DropColumn(
                name: "Start",
                table: "Fields");

            migrationBuilder.DropColumn(
                name: "RangeEnd",
                table: "Fields");

            migrationBuilder.DropColumn(
                name: "RangeStart",
                table: "Fields");
        }
    }
}
