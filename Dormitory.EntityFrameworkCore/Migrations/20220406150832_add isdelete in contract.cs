using Microsoft.EntityFrameworkCore.Migrations;

namespace Dormitory.EntityFrameworkCore.Migrations
{
    public partial class addisdeleteincontract : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ContractEntities",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ContractEntities");
        }
    }
}
