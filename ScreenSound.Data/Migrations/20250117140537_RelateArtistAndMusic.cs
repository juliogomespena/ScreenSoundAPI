using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScreenSound.Migrations
{
    /// <inheritdoc />
    public partial class RelateArtistAndMusic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ArtistId",
                table: "Music",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ReleaseYear",
                table: "Music",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Music_ArtistId",
                table: "Music",
                column: "ArtistId");

            migrationBuilder.AddForeignKey(
                name: "FK_Music_Artist_ArtistId",
                table: "Music",
                column: "ArtistId",
                principalTable: "Artist",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Music_Artist_ArtistId",
                table: "Music");

            migrationBuilder.DropIndex(
                name: "IX_Music_ArtistId",
                table: "Music");

            migrationBuilder.DropColumn(
                name: "ArtistId",
                table: "Music");

            migrationBuilder.DropColumn(
                name: "ReleaseYear",
                table: "Music");
        }
    }
}
