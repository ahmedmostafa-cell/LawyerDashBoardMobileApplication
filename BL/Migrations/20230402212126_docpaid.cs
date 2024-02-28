using Microsoft.EntityFrameworkCore.Migrations;

namespace BL.Migrations
{
    public partial class docpaid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TheDocumentationPaidValue",
                table: "TbConsultingEstablish",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TheDocumentationPaidValue",
                table: "TbConsultingEstablish");
        }
    }
}
