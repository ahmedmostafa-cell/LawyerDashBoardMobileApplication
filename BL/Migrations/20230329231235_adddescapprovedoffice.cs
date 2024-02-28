using Microsoft.EntityFrameworkCore.Migrations;

namespace BL.Migrations
{
    public partial class adddescapprovedoffice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApprovedOfficeShortDescription",
                table: "TbApprovedOffice",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApprovedOfficeShortDescription",
                table: "TbApprovedOffice");
        }
    }
}
