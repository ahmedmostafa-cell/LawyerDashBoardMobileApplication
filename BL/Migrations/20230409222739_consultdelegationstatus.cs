using Microsoft.EntityFrameworkCore.Migrations;

namespace BL.Migrations
{
    public partial class consultdelegationstatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DelegationStatus",
                table: "TbConsultingEstablish",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DelegationStatus",
                table: "TbConsultingEstablish");
        }
    }
}
