using Microsoft.EntityFrameworkCore.Migrations;

namespace Dormitory.EntityFrameworkCore.Migrations
{
    public partial class updateroomservice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ElecticStatBegin",
                table: "RoomServiceEntities");

            migrationBuilder.DropColumn(
                name: "ElecticStatEnd",
                table: "RoomServiceEntities");

            migrationBuilder.RenameColumn(
                name: "WaterStatEnd",
                table: "RoomServiceEntities",
                newName: "StatEnd");

            migrationBuilder.RenameColumn(
                name: "WaterStatBegin",
                table: "RoomServiceEntities",
                newName: "StatBegin");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StatEnd",
                table: "RoomServiceEntities",
                newName: "WaterStatEnd");

            migrationBuilder.RenameColumn(
                name: "StatBegin",
                table: "RoomServiceEntities",
                newName: "WaterStatBegin");

            migrationBuilder.AddColumn<float>(
                name: "ElecticStatBegin",
                table: "RoomServiceEntities",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "ElecticStatEnd",
                table: "RoomServiceEntities",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }
    }
}
