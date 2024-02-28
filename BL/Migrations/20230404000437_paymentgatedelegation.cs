using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BL.Migrations
{
    public partial class paymentgatedelegation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PaymentGatePercentDelegation",
                table: "TbConsultingEstablish",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaymentGateTitleDelegation",
                table: "TbConsultingEstablish",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PaymentGatesIdDelegation",
                table: "TbConsultingEstablish",
                type: "uniqueidentifier",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentGatePercentDelegation",
                table: "TbConsultingEstablish");

            migrationBuilder.DropColumn(
                name: "PaymentGateTitleDelegation",
                table: "TbConsultingEstablish");

            migrationBuilder.DropColumn(
                name: "PaymentGatesIdDelegation",
                table: "TbConsultingEstablish");
        }
    }
}
