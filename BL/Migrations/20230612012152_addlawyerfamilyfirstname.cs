using Microsoft.EntityFrameworkCore.Migrations;

namespace BL.Migrations
{
    public partial class addlawyerfamilyfirstname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LawyerFamilyName",
                table: "TbOffer",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LawyerFamilyName",
                table: "TbConsultingEstablish",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LawyerFamilyName",
                table: "TbOffer");

            migrationBuilder.DropColumn(
                name: "LawyerFamilyName",
                table: "TbConsultingEstablish");
        }
    }
}
