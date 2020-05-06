using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Calm.Dtb.Migrations
{
    public partial class addGatherings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Gatheringid",
                table: "Users",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Gatherings",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(nullable: true),
                    occurrenceData = table.Column<string>(nullable: true),
                    details = table.Column<string>(nullable: true),
                    organizerId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gatherings", x => x.id);
                    table.ForeignKey(
                        name: "FK_Gatherings_Users_organizerId",
                        column: x => x.organizerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Gatheringid",
                table: "Users",
                column: "Gatheringid");

            migrationBuilder.CreateIndex(
                name: "IX_Gatherings_organizerId",
                table: "Gatherings",
                column: "organizerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Gatherings_Gatheringid",
                table: "Users",
                column: "Gatheringid",
                principalTable: "Gatherings",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Gatherings_Gatheringid",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Gatherings");

            migrationBuilder.DropIndex(
                name: "IX_Users_Gatheringid",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Gatheringid",
                table: "Users");
        }
    }
}
