using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Spotify.Migrations
{
    /// <inheritdoc />
    public partial class updateSong : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AdminID",
                table: "Songs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdminID",
                table: "Songs");
        }
    }
}
