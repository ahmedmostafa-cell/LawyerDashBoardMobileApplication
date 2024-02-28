using Microsoft.EntityFrameworkCore.Migrations;

namespace BL.Migrations
{
    public partial class addtitlemaintosub : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MainConsultingTitle",
                table: "TbSubMainConsulting",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MainConsultingTitle",
                table: "TbSubMainConsulting");
        }
    }
}
