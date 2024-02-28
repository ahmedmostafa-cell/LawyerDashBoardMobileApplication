using Microsoft.EntityFrameworkCore.Migrations;

namespace BL.Migrations
{
    public partial class addapproveofficetoaspnetusers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsApprovedOffice",
                table: "AspNetUsers",
                type: "bit",
                nullable: true,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsApprovedOffice",
                table: "AspNetUsers");
        }
    }
}
