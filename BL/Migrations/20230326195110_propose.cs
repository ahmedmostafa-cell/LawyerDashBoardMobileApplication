﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace BL.Migrations
{
    public partial class propose : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProposedCustomerPay",
                table: "TbConsultingEstablish",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProposedCustomerPay",
                table: "TbConsultingEstablish");
        }
    }
}
