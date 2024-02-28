using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BL.Migrations
{
    public partial class aap : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TbLawyerAppintments",
                columns: table => new
                {
                    LawyerAppointmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    LawyerId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    LawyerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WeekDay = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    FromHour = table.Column<double>(type: "float", nullable: true),
                    MorEveFrst = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ToHour = table.Column<double>(type: "float", nullable: true),
                    MorEveScond = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CurrentState = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((1))"),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbLawyerAppintments", x => x.LawyerAppointmentId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TbLawyerAppintments");
        }
    }
}
