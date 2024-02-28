using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BL.Migrations
{
    public partial class addingchat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PaymentGateTitle",
                table: "TbPaymentGates",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ActivationStatus",
                table: "TbPaymentGates",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "TbChat",
                columns: table => new
                {
                    ChatId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ConsultingId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SenderId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    SenderFirstName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    SenderEmail = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    SenderText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SenderAudio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SenderDocument = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SenderUserType = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RecieverId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RecieverFirstName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RecieverEmail = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RecieverUserType = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CurrentState = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbChat", x => x.ChatId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TbChat");

            migrationBuilder.AlterColumn<string>(
                name: "PaymentGateTitle",
                table: "TbPaymentGates",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "ActivationStatus",
                table: "TbPaymentGates",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);
        }
    }
}
