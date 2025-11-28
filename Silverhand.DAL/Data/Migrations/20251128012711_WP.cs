using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Silverhand.DAL.Migrations
{
    /// <inheritdoc />
    public partial class WP : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WatchProgresses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TitleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EpisodeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProgressSeconds = table.Column<int>(type: "int", nullable: false),
                    DurationSeconds = table.Column<int>(type: "int", nullable: false),
                    Completed = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WatchProgresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WatchProgresses_Episodes_EpisodeId",
                        column: x => x.EpisodeId,
                        principalTable: "Episodes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WatchProgresses_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WatchProgresses_Titles_TitleId",
                        column: x => x.TitleId,
                        principalTable: "Titles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WatchProgresses_EpisodeId",
                table: "WatchProgresses",
                column: "EpisodeId");

            migrationBuilder.CreateIndex(
                name: "IX_WatchProgresses_ProfileId",
                table: "WatchProgresses",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_WatchProgresses_TitleId",
                table: "WatchProgresses",
                column: "TitleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WatchProgresses");
        }
    }
}
