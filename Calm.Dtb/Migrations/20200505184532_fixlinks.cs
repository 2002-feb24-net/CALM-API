using Microsoft.EntityFrameworkCore.Migrations;

namespace Calm.Dtb.Migrations
{
    public partial class fixlinks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_links_Gatherings_gatheringid",
                table: "links");

            migrationBuilder.DropForeignKey(
                name: "FK_links_Users_userId",
                table: "links");

            migrationBuilder.RenameColumn(
                name: "gatheringid",
                table: "links",
                newName: "gatheringId");

            migrationBuilder.RenameIndex(
                name: "IX_links_gatheringid",
                table: "links",
                newName: "IX_links_gatheringId");

            migrationBuilder.AlterColumn<int>(
                name: "userId",
                table: "links",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "gatheringId",
                table: "links",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_links_Gatherings_gatheringId",
                table: "links",
                column: "gatheringId",
                principalTable: "Gatherings",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_links_Users_userId",
                table: "links",
                column: "userId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_links_Gatherings_gatheringId",
                table: "links");

            migrationBuilder.DropForeignKey(
                name: "FK_links_Users_userId",
                table: "links");

            migrationBuilder.RenameColumn(
                name: "gatheringId",
                table: "links",
                newName: "gatheringid");

            migrationBuilder.RenameIndex(
                name: "IX_links_gatheringId",
                table: "links",
                newName: "IX_links_gatheringid");

            migrationBuilder.AlterColumn<int>(
                name: "userId",
                table: "links",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "gatheringid",
                table: "links",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_links_Gatherings_gatheringid",
                table: "links",
                column: "gatheringid",
                principalTable: "Gatherings",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_links_Users_userId",
                table: "links",
                column: "userId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
