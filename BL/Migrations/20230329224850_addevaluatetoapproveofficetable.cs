using Microsoft.EntityFrameworkCore.Migrations;

namespace BL.Migrations
{
    public partial class addevaluatetoapproveofficetable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EvaluationNoOfStatrs",
                table: "TbApprovedOffice",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EvaluationNumerical",
                table: "TbApprovedOffice",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EvaluationNoOfStatrs",
                table: "TbApprovedOffice");

            migrationBuilder.DropColumn(
                name: "EvaluationNumerical",
                table: "TbApprovedOffice");
        }
    }
}
