using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inf_Data.Migrations
{
    public partial class AddCodeForPlants : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Plants",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "Plants");
        }
    }
}
