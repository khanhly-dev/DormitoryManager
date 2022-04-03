using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dormitory.EntityFrameworkCore.Migrations
{
    public partial class changecontracttimeconfig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MonthConfig",
                table: "ContractTimeConfigEntities");

            migrationBuilder.AddColumn<DateTime>(
                name: "FromDate",
                table: "ContractTimeConfigEntities",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ToDate",
                table: "ContractTimeConfigEntities",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FromDate",
                table: "ContractTimeConfigEntities");

            migrationBuilder.DropColumn(
                name: "ToDate",
                table: "ContractTimeConfigEntities");

            migrationBuilder.AddColumn<int>(
                name: "MonthConfig",
                table: "ContractTimeConfigEntities",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
