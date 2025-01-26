using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inf_Data.Migrations
{
    public partial class ExpandPlantWithDateWhenItWasBought : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "BoughtAt",
                table: "Plants",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "Plants",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BoughtAt",
                table: "Plants");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Plants");
        }
    }
}
