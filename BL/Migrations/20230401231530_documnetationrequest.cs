using Microsoft.EntityFrameworkCore.Migrations;

namespace BL.Migrations
{
    public partial class documnetationrequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DocumnetationRequestShortDescription",
                table: "TbConsultingEstablish",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DocumnetationRequestShortDescription",
                table: "TbConsultingEstablish");
        }
    }
}
