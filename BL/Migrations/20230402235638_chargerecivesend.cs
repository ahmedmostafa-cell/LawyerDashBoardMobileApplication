using Microsoft.EntityFrameworkCore.Migrations;

namespace BL.Migrations
{
    public partial class chargerecivesend : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "TbCharge",
                newName: "IdSender");

            migrationBuilder.AddColumn<string>(
                name: "IdReciever",
                table: "TbCharge",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdReciever",
                table: "TbCharge");

            migrationBuilder.RenameColumn(
                name: "IdSender",
                table: "TbCharge",
                newName: "Id");
        }
    }
}
