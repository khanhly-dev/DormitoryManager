using Microsoft.EntityFrameworkCore.Migrations;

namespace Dormitory.Student.EntityFrameworkCore.Migrations
{
    public partial class edittablename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserEntities",
                table: "UserEntities");

            migrationBuilder.RenameTable(
                name: "UserEntities",
                newName: "UserAccountEntities");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserAccountEntities",
                table: "UserAccountEntities",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserAccountEntities",
                table: "UserAccountEntities");

            migrationBuilder.RenameTable(
                name: "UserAccountEntities",
                newName: "UserEntities");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserEntities",
                table: "UserEntities",
                column: "Id");
        }
    }
}
