using Microsoft.EntityFrameworkCore.Migrations;

namespace BL.Migrations
{
    public partial class myfatiirahinvoiceid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MyfatoorahInvoiceId",
                table: "TbConsultingEstablish",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MyfatoorahInvoiceId",
                table: "TbConsultingEstablish");
        }
    }
}
