using Microsoft.EntityFrameworkCore.Migrations;

namespace BL.Migrations
{
    public partial class qutationdelegation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "QutationDelegationValueSentFromUser",
                table: "TbConsultingEstablish",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QutationDelegationValueSentFromUser",
                table: "TbConsultingEstablish");
        }
    }
}
