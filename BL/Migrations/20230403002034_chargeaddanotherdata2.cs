using Microsoft.EntityFrameworkCore.Migrations;

namespace BL.Migrations
{
    public partial class chargeaddanotherdata2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RecieverName",
                table: "TbCharge",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SenderName",
                table: "TbCharge",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RecieverName",
                table: "TbCharge");

            migrationBuilder.DropColumn(
                name: "SenderName",
                table: "TbCharge");
        }
    }
}
