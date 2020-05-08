using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Calm.Dtb.Migrations
{
    public partial class mapdata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_links_Gatherings_gatheringId",
                table: "links");

            migrationBuilder.DropForeignKey(
                name: "FK_links_Users_userId",
                table: "links");

            migrationBuilder.DropPrimaryKey(
                name: "PK_links",
                table: "links");

            migrationBuilder.RenameTable(
                name: "links",
                newName: "Links");

            migrationBuilder.RenameIndex(
                name: "IX_links_userId",
                table: "Links",
                newName: "IX_Links_userId");

            migrationBuilder.RenameIndex(
                name: "IX_links_gatheringId",
                table: "Links",
                newName: "IX_Links_gatheringId");

            migrationBuilder.AddColumn<int>(
                name: "MapDataId",
                table: "Users",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MapDataId",
                table: "Gatherings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Links",
                table: "Links",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Citys",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    city = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Citys", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_MapDataId",
                table: "Users",
                column: "MapDataId");

            migrationBuilder.CreateIndex(
                name: "IX_Gatherings_MapDataId",
                table: "Gatherings",
                column: "MapDataId");

            migrationBuilder.AddForeignKey(
                name: "FK_Gatherings_Citys_MapDataId",
                table: "Gatherings",
                column: "MapDataId",
                principalTable: "Citys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Links_Gatherings_gatheringId",
                table: "Links",
                column: "gatheringId",
                principalTable: "Gatherings",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Links_Users_userId",
                table: "Links",
                column: "userId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Citys_MapDataId",
                table: "Users",
                column: "MapDataId",
                principalTable: "Citys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gatherings_Citys_MapDataId",
                table: "Gatherings");

            migrationBuilder.DropForeignKey(
                name: "FK_Links_Gatherings_gatheringId",
                table: "Links");

            migrationBuilder.DropForeignKey(
                name: "FK_Links_Users_userId",
                table: "Links");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Citys_MapDataId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Citys");

            migrationBuilder.DropIndex(
                name: "IX_Users_MapDataId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Links",
                table: "Links");

            migrationBuilder.DropIndex(
                name: "IX_Gatherings_MapDataId",
                table: "Gatherings");

            migrationBuilder.DropColumn(
                name: "MapDataId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "MapDataId",
                table: "Gatherings");

            migrationBuilder.RenameTable(
                name: "Links",
                newName: "links");

            migrationBuilder.RenameIndex(
                name: "IX_Links_userId",
                table: "links",
                newName: "IX_links_userId");

            migrationBuilder.RenameIndex(
                name: "IX_Links_gatheringId",
                table: "links",
                newName: "IX_links_gatheringId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_links",
                table: "links",
                column: "Id");

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
    }
}
