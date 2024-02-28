using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BL.Migrations
{
    public partial class paymentgatetoconsult : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PaymentGatePercent",
                table: "TbConsultingEstablish",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaymentGateTitle",
                table: "TbConsultingEstablish",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PaymentGatesId",
                table: "TbConsultingEstablish",
                type: "uniqueidentifier",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentGatePercent",
                table: "TbConsultingEstablish");

            migrationBuilder.DropColumn(
                name: "PaymentGateTitle",
                table: "TbConsultingEstablish");

            migrationBuilder.DropColumn(
                name: "PaymentGatesId",
                table: "TbConsultingEstablish");
        }
    }
}
