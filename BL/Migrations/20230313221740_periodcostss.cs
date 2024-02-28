using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BL.Migrations
{
    public partial class periodcostss : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TbLawyerPeriodCostConsult",
                columns: table => new
                {
                    LawyerPeriodCostConsultId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    LawyerId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    LawyerName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ConsultingPeriod = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ConsultingPeriodCost = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CurrentState = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbLawyerPeriodCostConsult", x => x.LawyerPeriodCostConsultId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TbLawyerPeriodCostConsult");
        }
    }
}
