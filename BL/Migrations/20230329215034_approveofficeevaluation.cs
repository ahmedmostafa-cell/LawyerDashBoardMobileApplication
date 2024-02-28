using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BL.Migrations
{
    public partial class approveofficeevaluation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TbEvaluationApprovedOffice",
                columns: table => new
                {
                    EvaluationApprovedOfficeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    EvaluaterId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    EvaluaterName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    EvaluationApprovedOfficeText = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    StartsNo = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ApprovedOfficeId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ApprovedOfficeName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    EvaluaterImage = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ApprovedOfficeLogo = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CurrentState = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbEvaluationApprovedOffice", x => x.EvaluationApprovedOfficeId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TbEvaluationApprovedOffice");
        }
    }
}
