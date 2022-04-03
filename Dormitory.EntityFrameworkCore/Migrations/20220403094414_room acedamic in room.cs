using Microsoft.EntityFrameworkCore.Migrations;

namespace Dormitory.EntityFrameworkCore.Migrations
{
    public partial class roomacedamicinroom : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoomAcedemic",
                table: "RoomEntities",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RoomGender",
                table: "RoomEntities",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoomAcedemic",
                table: "RoomEntities");

            migrationBuilder.DropColumn(
                name: "RoomGender",
                table: "RoomEntities");
        }
    }
}
