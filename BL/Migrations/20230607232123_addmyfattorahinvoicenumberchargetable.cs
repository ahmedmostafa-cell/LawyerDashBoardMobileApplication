using Microsoft.EntityFrameworkCore.Migrations;

namespace BL.Migrations
{
    public partial class addmyfattorahinvoicenumberchargetable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MyfatoorahInvoiceId",
                table: "TbCharge",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MyfatoorahInvoiceId",
                table: "TbCharge");
        }
    }
}
