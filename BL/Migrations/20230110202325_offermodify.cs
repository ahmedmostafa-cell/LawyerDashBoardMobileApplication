using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BL.Migrations
{
    public partial class offermodify : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "TbOffer",
                type: "datetime2",
                fixedLength: true,
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nchar(10)",
                oldFixedLength: true,
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CurrentState",
                table: "TbOffer",
                type: "int",
                fixedLength: true,
                maxLength: 10,
                nullable: true,
                defaultValueSql: "((1))",
                oldClrType: typeof(string),
                oldType: "nchar(10)",
                oldFixedLength: true,
                oldMaxLength: 10,
                oldNullable: true,
                oldDefaultValueSql: "((1))");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "TbOffer",
                type: "datetime2",
                fixedLength: true,
                maxLength: 10,
                nullable: true,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(string),
                oldType: "nchar(10)",
                oldFixedLength: true,
                oldMaxLength: 10,
                oldNullable: true,
                oldDefaultValueSql: "(getdate())");

         

           

           

           
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UpdatedDate",
                table: "TbOffer",
                type: "nchar(10)",
                fixedLength: true,
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldFixedLength: true,
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CurrentState",
                table: "TbOffer",
                type: "nchar(10)",
                fixedLength: true,
                maxLength: 10,
                nullable: true,
                defaultValueSql: "((1))",
                oldClrType: typeof(int),
                oldType: "int",
                oldFixedLength: true,
                oldMaxLength: 10,
                oldNullable: true,
                oldDefaultValueSql: "((1))");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedDate",
                table: "TbOffer",
                type: "nchar(10)",
                fixedLength: true,
                maxLength: 10,
                nullable: true,
                defaultValueSql: "(getdate())",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldFixedLength: true,
                oldMaxLength: 10,
                oldNullable: true,
                oldDefaultValueSql: "(getdate())");

          
        }
    }
}
