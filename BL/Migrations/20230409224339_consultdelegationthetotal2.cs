using Microsoft.EntityFrameworkCore.Migrations;

namespace BL.Migrations
{
    public partial class consultdelegationthetotal2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TheTotalDelegation",
                table: "TbConsultingEstablish",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TheTotalDelegation",
                table: "TbConsultingEstablish");
        }
    }
}
