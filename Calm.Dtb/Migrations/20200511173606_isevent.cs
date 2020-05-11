using Microsoft.EntityFrameworkCore.Migrations;

namespace Calm.Dtb.Migrations
{
    public partial class isevent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isEvent",
                table: "Gatherings",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isEvent",
                table: "Gatherings");
        }
    }
}
