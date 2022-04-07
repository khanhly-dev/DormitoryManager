using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dormitory.EntityFrameworkCore.Migrations
{
    public partial class updatedbaddfeestaff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "Quantity",
                table: "RoomServiceEntities",
                type: "real",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

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

            migrationBuilder.AddColumn<float>(
                name: "WaterStatBegin",
                table: "RoomServiceEntities",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "WaterStatEnd",
                table: "RoomServiceEntities",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<bool>(
                name: "IsExtendContact",
                table: "ContractEntities",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "ContractFeeEntities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContractId = table.Column<int>(type: "int", nullable: false),
                    ContractPriceValue = table.Column<float>(type: "real", nullable: false),
                    PaidDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MoneyPaid = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractFeeEntities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PositionEntities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PositionEntities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StaffEntities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Adress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PositionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffEntities", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContractFeeEntities");

            migrationBuilder.DropTable(
                name: "PositionEntities");

            migrationBuilder.DropTable(
                name: "StaffEntities");

            migrationBuilder.DropColumn(
                name: "ElecticStatBegin",
                table: "RoomServiceEntities");

            migrationBuilder.DropColumn(
                name: "ElecticStatEnd",
                table: "RoomServiceEntities");

            migrationBuilder.DropColumn(
                name: "WaterStatBegin",
                table: "RoomServiceEntities");

            migrationBuilder.DropColumn(
                name: "WaterStatEnd",
                table: "RoomServiceEntities");

            migrationBuilder.DropColumn(
                name: "IsExtendContact",
                table: "ContractEntities");

            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "RoomServiceEntities",
                type: "int",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");
        }
    }
}
