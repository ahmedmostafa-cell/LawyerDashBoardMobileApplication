using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BL.Migrations
{
    public partial class addlawyermainconsultingtable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TbLawyersMainConsultingss",
                columns: table => new
                {
                    LawyersMainConsultingsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LawyerId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LawyerUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConsultingNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DaysOfWork = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HoursOfWork = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EvaluationNoOfStatrs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EvaluationNumerical = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YearsOfExperience = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocumentA = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocumentB = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocumentC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocumentD = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdentityDocument = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShortDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OTP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FamilyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeviceToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActivated = table.Column<bool>(type: "bit", nullable: true),
                    MainConsultingId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MainConsultingTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MainConsultingImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Consulting30MinutesCost = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Consulting60MinutesCost = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Consulting90MinutesCost = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentState = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbLawyersMainConsultingss", x => x.LawyersMainConsultingsId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TbLawyersMainConsultingss");
        }
    }
}
