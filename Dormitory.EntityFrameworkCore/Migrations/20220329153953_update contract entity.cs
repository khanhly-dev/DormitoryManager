using Microsoft.EntityFrameworkCore.Migrations;

namespace Dormitory.EntityFrameworkCore.Migrations
{
    public partial class updatecontractentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DesiredRoomId",
                table: "ContractEntities");

            migrationBuilder.AddColumn<float>(
                name: "DesiredPrice",
                table: "ContractEntities",
                type: "real",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DesiredPrice",
                table: "ContractEntities");

            migrationBuilder.AddColumn<int>(
                name: "DesiredRoomId",
                table: "ContractEntities",
                type: "int",
                nullable: true);
        }
    }
}
