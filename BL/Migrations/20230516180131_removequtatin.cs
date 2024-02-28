using Microsoft.EntityFrameworkCore.Migrations;

namespace BL.Migrations
{
    public partial class removequtatin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QutationDelegationValueSentFromUser",
                table: "TbConsultingEstablish");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "QutationDelegationValueSentFromUser",
                table: "TbConsultingEstablish",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
