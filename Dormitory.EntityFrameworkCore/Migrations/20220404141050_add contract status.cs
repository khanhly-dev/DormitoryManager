using Microsoft.EntityFrameworkCore.Migrations;

namespace Dormitory.EntityFrameworkCore.Migrations
{
    public partial class addcontractstatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ContractCompletedStatus",
                table: "ContractEntities",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContractCompletedStatus",
                table: "ContractEntities");
        }
    }
}
