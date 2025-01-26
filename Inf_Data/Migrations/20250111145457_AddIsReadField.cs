using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inf_Data.Migrations
{
    public partial class AddIsReadField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isRead",
                table: "Notifications",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isRead",
                table: "Notifications");
        }
    }
}
