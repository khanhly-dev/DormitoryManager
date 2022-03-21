using Microsoft.EntityFrameworkCore.Migrations;

namespace Dormitory.Admin.EntityFrameworkCore.Migrations
{
    public partial class edittableusername : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Phone",
                table: "UserEntities");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "UserEntities",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
