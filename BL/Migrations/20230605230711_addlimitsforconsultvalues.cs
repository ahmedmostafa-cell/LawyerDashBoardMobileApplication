using Microsoft.EntityFrameworkCore.Migrations;

namespace BL.Migrations
{
    public partial class addlimitsforconsultvalues : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Consulting30MinutesCost",
                table: "TbSetting",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Consulting60MinutesCost",
                table: "TbSetting",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Consulting90MinutesCost",
                table: "TbSetting",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LowestConsultUnSpecifiedValue",
                table: "TbSetting",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Consulting30MinutesCost",
                table: "TbSetting");

            migrationBuilder.DropColumn(
                name: "Consulting60MinutesCost",
                table: "TbSetting");

            migrationBuilder.DropColumn(
                name: "Consulting90MinutesCost",
                table: "TbSetting");

            migrationBuilder.DropColumn(
                name: "LowestConsultUnSpecifiedValue",
                table: "TbSetting");
        }
    }
}
