using Microsoft.EntityFrameworkCore.Migrations;

namespace Dormitory.EntityFrameworkCore.Migrations
{
    public partial class fixcolname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsExtendContact",
                table: "ContractEntities",
                newName: "IsExtendContract");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsExtendContract",
                table: "ContractEntities",
                newName: "IsExtendContact");
        }
    }
}
