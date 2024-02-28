using Microsoft.EntityFrameworkCore.Migrations;

namespace BL.Migrations
{
    public partial class addcancellationday : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DayOfCancellation",
                table: "TbConsultingEstablish",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DayOfCancellation",
                table: "TbConsultingEstablish");
        }
    }
}
