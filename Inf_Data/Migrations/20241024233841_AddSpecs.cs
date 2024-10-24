using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inf_Data.Migrations
{
    public partial class AddSpecs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plants_AspNetUsers_AppUserId",
                table: "Plants");

            migrationBuilder.DropForeignKey(
                name: "FK_Plants_PlantSpecificaitons_PlantSpecificationId",
                table: "Plants");

            migrationBuilder.DropTable(
                name: "PlantSpecificaitons");

            migrationBuilder.DropIndex(
                name: "IX_Plants_AppUserId",
                table: "Plants");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Plants");

            migrationBuilder.DropColumn(
                name: "DeatPlantCount",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "PlantSpecifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Wattering = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Temperature = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Illumination = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SpecieName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlantSpecifications", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Plants_PlantSpecifications_PlantSpecificationId",
                table: "Plants",
                column: "PlantSpecificationId",
                principalTable: "PlantSpecifications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plants_PlantSpecifications_PlantSpecificationId",
                table: "Plants");

            migrationBuilder.DropTable(
                name: "PlantSpecifications");

            migrationBuilder.AddColumn<int>(
                name: "AppUserId",
                table: "Plants",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeatPlantCount",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "PlantSpecificaitons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Illumination = table.Column<float>(type: "real", nullable: false),
                    SpecieName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Temperature = table.Column<float>(type: "real", nullable: false),
                    Wattering = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlantSpecificaitons", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Plants_AppUserId",
                table: "Plants",
                column: "AppUserId");

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
    }
}
