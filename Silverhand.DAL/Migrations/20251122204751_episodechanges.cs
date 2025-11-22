using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Silverhand.DAL.Migrations
{
    /// <inheritdoc />
    public partial class episodechanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Episodes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Episodes",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Episodes");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Episodes");
        }
    }
}
