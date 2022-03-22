using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dormitory.EntityFrameworkCore.Migrations
{
    public partial class createdb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserAccountEntities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserStudentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAccountEntities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserStudentEntities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DOB = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StudentCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Major = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Adress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Class = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserStudentEntities", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserAccountEntities");

            migrationBuilder.DropTable(
                name: "UserStudentEntities");
        }
    }
}
