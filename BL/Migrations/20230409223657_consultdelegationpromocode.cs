using Microsoft.EntityFrameworkCore.Migrations;

namespace BL.Migrations
{
    public partial class consultdelegationpromocode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DelegationPromocodeDiscountValue",
                table: "TbConsultingEstablish",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DelegationPromocodeDiscountValue",
                table: "TbConsultingEstablish");
        }
    }
}
