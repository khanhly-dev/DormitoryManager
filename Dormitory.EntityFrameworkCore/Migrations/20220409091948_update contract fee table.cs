using Microsoft.EntityFrameworkCore.Migrations;

namespace Dormitory.EntityFrameworkCore.Migrations
{
    public partial class updatecontractfeetable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "RoomPrice",
                table: "ContractFeeEntities",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "ServicePrice",
                table: "ContractFeeEntities",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoomPrice",
                table: "ContractFeeEntities");

            migrationBuilder.DropColumn(
                name: "ServicePrice",
                table: "ContractFeeEntities");
        }
    }
}
