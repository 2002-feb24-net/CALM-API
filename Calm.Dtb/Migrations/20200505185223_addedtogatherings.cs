using Microsoft.EntityFrameworkCore.Migrations;

namespace Calm.Dtb.Migrations
{
    public partial class addedtogatherings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gatherings_Users_organizerId",
                table: "Gatherings");

            migrationBuilder.AlterColumn<int>(
                name: "organizerId",
                table: "Gatherings",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Gatherings_Users_organizerId",
                table: "Gatherings",
                column: "organizerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gatherings_Users_organizerId",
                table: "Gatherings");

            migrationBuilder.AlterColumn<int>(
                name: "organizerId",
                table: "Gatherings",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Gatherings_Users_organizerId",
                table: "Gatherings",
                column: "organizerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
