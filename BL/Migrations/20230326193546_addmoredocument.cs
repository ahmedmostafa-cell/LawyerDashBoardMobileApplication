using Microsoft.EntityFrameworkCore.Migrations;

namespace BL.Migrations
{
    public partial class addmoredocument : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RequestDocument2",
                table: "TbConsultingEstablish",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RequestDocument3",
                table: "TbConsultingEstablish",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RequestDocument4",
                table: "TbConsultingEstablish",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RequestDocument5",
                table: "TbConsultingEstablish",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequestDocument2",
                table: "TbConsultingEstablish");

            migrationBuilder.DropColumn(
                name: "RequestDocument3",
                table: "TbConsultingEstablish");

            migrationBuilder.DropColumn(
                name: "RequestDocument4",
                table: "TbConsultingEstablish");

            migrationBuilder.DropColumn(
                name: "RequestDocument5",
                table: "TbConsultingEstablish");
        }
    }
}
