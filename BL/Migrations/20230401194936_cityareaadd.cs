using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BL.Migrations
{
    public partial class cityareaadd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DocumentationOfContractId",
                table: "TbConsultingEstablish",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DocumentationOfContractTilte",
                table: "TbConsultingEstablish",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ServiceName",
                table: "TbConsultingEstablish",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserPhoneNumber",
                table: "TbConsultingEstablish",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DocumentationOfContractId",
                table: "TbConsultingEstablish");

            migrationBuilder.DropColumn(
                name: "DocumentationOfContractTilte",
                table: "TbConsultingEstablish");

            migrationBuilder.DropColumn(
                name: "ServiceName",
                table: "TbConsultingEstablish");

            migrationBuilder.DropColumn(
                name: "UserPhoneNumber",
                table: "TbConsultingEstablish");
        }
    }
}
