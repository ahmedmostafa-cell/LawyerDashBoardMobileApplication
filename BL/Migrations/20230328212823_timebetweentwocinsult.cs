using Microsoft.EntityFrameworkCore.Migrations;

namespace BL.Migrations
{
    public partial class timebetweentwocinsult : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TimeBetweenTwoConsultation",
                table: "TbSetting",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "ApprovalStatus",
                table: "TbApprovedOffice",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeBetweenTwoConsultation",
                table: "TbSetting");

            migrationBuilder.AlterColumn<string>(
                name: "ApprovalStatus",
                table: "TbApprovedOffice",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);
        }
    }
}
