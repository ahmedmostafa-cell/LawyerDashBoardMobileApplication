using Microsoft.EntityFrameworkCore.Migrations;

namespace BL.Migrations
{
    public partial class multiimagesupload : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageFive",
                table: "TbConsultingEstablish",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageFour",
                table: "TbConsultingEstablish",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageOne",
                table: "TbConsultingEstablish",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageThree",
                table: "TbConsultingEstablish",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageTwo",
                table: "TbConsultingEstablish",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageFive",
                table: "TbConsultingEstablish");

            migrationBuilder.DropColumn(
                name: "ImageFour",
                table: "TbConsultingEstablish");

            migrationBuilder.DropColumn(
                name: "ImageOne",
                table: "TbConsultingEstablish");

            migrationBuilder.DropColumn(
                name: "ImageThree",
                table: "TbConsultingEstablish");

            migrationBuilder.DropColumn(
                name: "ImageTwo",
                table: "TbConsultingEstablish");
        }
    }
}
