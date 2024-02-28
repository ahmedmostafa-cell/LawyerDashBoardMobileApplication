using Microsoft.EntityFrameworkCore.Migrations;

namespace BL.Migrations
{
    public partial class undefinedsubcins : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UnDefinedSubConsultingName",
                table: "TbConsultingEstablish",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnDefinedSubConsultingName",
                table: "TbConsultingEstablish");
        }
    }
}
