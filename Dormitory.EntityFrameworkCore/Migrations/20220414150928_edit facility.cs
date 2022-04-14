using Microsoft.EntityFrameworkCore.Migrations;

namespace Dormitory.EntityFrameworkCore.Migrations
{
    public partial class editfacility : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "FacilityEntities");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "FacilityInRoomEntities",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "FacilityInRoomEntities");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "FacilityEntities",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
