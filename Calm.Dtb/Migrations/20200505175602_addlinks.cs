using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Calm.Dtb.Migrations
{
    public partial class addlinks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Gatherings_Gatheringid",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_Gatheringid",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Gatheringid",
                table: "Users");

            migrationBuilder.CreateTable(
                name: "links",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    userId = table.Column<int>(nullable: true),
                    gatheringid = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_links", x => x.Id);
                    table.ForeignKey(
                        name: "FK_links_Gatherings_gatheringid",
                        column: x => x.gatheringid,
                        principalTable: "Gatherings",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_links_Users_userId",
                        column: x => x.userId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_links_gatheringid",
                table: "links",
                column: "gatheringid");

            migrationBuilder.CreateIndex(
                name: "IX_links_userId",
                table: "links",
                column: "userId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "links");

            migrationBuilder.AddColumn<int>(
                name: "Gatheringid",
                table: "Users",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Gatheringid",
                table: "Users",
                column: "Gatheringid");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Gatherings_Gatheringid",
                table: "Users",
                column: "Gatheringid",
                principalTable: "Gatherings",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
