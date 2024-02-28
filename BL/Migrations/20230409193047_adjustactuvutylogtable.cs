using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BL.Migrations
{
    public partial class adjustactuvutylogtable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "TbActivityLog",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "TbActivityLog",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CurrentState",
                table: "TbActivityLog",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "TbActivityLog",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "TbActivityLog",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "TbActivityLog",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "TbActivityLog",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "TbActivityLog");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "TbActivityLog");

            migrationBuilder.DropColumn(
                name: "CurrentState",
                table: "TbActivityLog");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "TbActivityLog");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "TbActivityLog");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "TbActivityLog");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "TbActivityLog");
        }
    }
}
