using Microsoft.EntityFrameworkCore.Migrations;

namespace BL.Migrations
{
    public partial class delegationshortdescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CaseShortDescription",
                table: "TbConsultingEstablish",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CaseShortDescription",
                table: "TbConsultingEstablish");
        }
    }
}
