using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Silverhand.DAL.Migrations
{
    /// <inheritdoc />
    public partial class season : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SeasonNumber",
                table: "Episodes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SeasonNumber",
                table: "Episodes");
        }
    }
}
