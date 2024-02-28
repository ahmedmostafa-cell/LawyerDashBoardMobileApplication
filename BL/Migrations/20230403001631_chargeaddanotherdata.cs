using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BL.Migrations
{
    public partial class chargeaddanotherdata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ChargeType",
                table: "TbCharge",
                newName: "ChargeTypeSender");

            migrationBuilder.AddColumn<string>(
                name: "ChargeTypeReciever",
                table: "TbCharge",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ConsultingId",
                table: "TbCharge",
                type: "uniqueidentifier",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChargeTypeReciever",
                table: "TbCharge");

            migrationBuilder.DropColumn(
                name: "ConsultingId",
                table: "TbCharge");

            migrationBuilder.RenameColumn(
                name: "ChargeTypeSender",
                table: "TbCharge",
                newName: "ChargeType");
        }
    }
}
