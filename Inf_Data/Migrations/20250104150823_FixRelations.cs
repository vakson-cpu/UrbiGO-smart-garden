using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inf_Data.Migrations
{
    public partial class FixRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Devices_PlantToMonitorId",
                table: "Devices");

            migrationBuilder.CreateIndex(
                name: "IX_Devices_PlantToMonitorId",
                table: "Devices",
                column: "PlantToMonitorId",
                unique: true,
                filter: "[PlantToMonitorId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Devices_PlantToMonitorId",
                table: "Devices");

            migrationBuilder.CreateIndex(
                name: "IX_Devices_PlantToMonitorId",
                table: "Devices",
                column: "PlantToMonitorId");
        }
    }
}
