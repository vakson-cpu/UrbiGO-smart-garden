using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inf_Data.Migrations
{
    public partial class AddDbSets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Device_AspNetUsers_AssignedToUserId",
                table: "Device");

            migrationBuilder.DropForeignKey(
                name: "FK_Device_Plant_PlantToMonitorId",
                table: "Device");

            migrationBuilder.DropForeignKey(
                name: "FK_Plant_AspNetUsers_AppUserId",
                table: "Plant");

            migrationBuilder.DropForeignKey(
                name: "FK_Plant_PlantSpecificaitons_PlantSpecificationId",
                table: "Plant");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Plant",
                table: "Plant");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Device",
                table: "Device");

            migrationBuilder.RenameTable(
                name: "Plant",
                newName: "Plants");

            migrationBuilder.RenameTable(
                name: "Device",
                newName: "Devices");

            migrationBuilder.RenameIndex(
                name: "IX_Plant_PlantSpecificationId",
                table: "Plants",
                newName: "IX_Plants_PlantSpecificationId");

            migrationBuilder.RenameIndex(
                name: "IX_Plant_AppUserId",
                table: "Plants",
                newName: "IX_Plants_AppUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Device_PlantToMonitorId",
                table: "Devices",
                newName: "IX_Devices_PlantToMonitorId");

            migrationBuilder.RenameIndex(
                name: "IX_Device_AssignedToUserId",
                table: "Devices",
                newName: "IX_Devices_AssignedToUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Plants",
                table: "Plants",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Devices",
                table: "Devices",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_AspNetUsers_AssignedToUserId",
                table: "Devices",
                column: "AssignedToUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_Plants_PlantToMonitorId",
                table: "Devices",
                column: "PlantToMonitorId",
                principalTable: "Plants",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Plants_AspNetUsers_AppUserId",
                table: "Plants",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Plants_PlantSpecificaitons_PlantSpecificationId",
                table: "Plants",
                column: "PlantSpecificationId",
                principalTable: "PlantSpecificaitons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Devices_AspNetUsers_AssignedToUserId",
                table: "Devices");

            migrationBuilder.DropForeignKey(
                name: "FK_Devices_Plants_PlantToMonitorId",
                table: "Devices");

            migrationBuilder.DropForeignKey(
                name: "FK_Plants_AspNetUsers_AppUserId",
                table: "Plants");

            migrationBuilder.DropForeignKey(
                name: "FK_Plants_PlantSpecificaitons_PlantSpecificationId",
                table: "Plants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Plants",
                table: "Plants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Devices",
                table: "Devices");

            migrationBuilder.RenameTable(
                name: "Plants",
                newName: "Plant");

            migrationBuilder.RenameTable(
                name: "Devices",
                newName: "Device");

            migrationBuilder.RenameIndex(
                name: "IX_Plants_PlantSpecificationId",
                table: "Plant",
                newName: "IX_Plant_PlantSpecificationId");

            migrationBuilder.RenameIndex(
                name: "IX_Plants_AppUserId",
                table: "Plant",
                newName: "IX_Plant_AppUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Devices_PlantToMonitorId",
                table: "Device",
                newName: "IX_Device_PlantToMonitorId");

            migrationBuilder.RenameIndex(
                name: "IX_Devices_AssignedToUserId",
                table: "Device",
                newName: "IX_Device_AssignedToUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Plant",
                table: "Plant",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Device",
                table: "Device",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Device_AspNetUsers_AssignedToUserId",
                table: "Device",
                column: "AssignedToUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Device_Plant_PlantToMonitorId",
                table: "Device",
                column: "PlantToMonitorId",
                principalTable: "Plant",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Plant_AspNetUsers_AppUserId",
                table: "Plant",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Plant_PlantSpecificaitons_PlantSpecificationId",
                table: "Plant",
                column: "PlantSpecificationId",
                principalTable: "PlantSpecificaitons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
