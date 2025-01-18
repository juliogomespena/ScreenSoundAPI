using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScreenSound.Migrations
{
    /// <inheritdoc />
    public partial class PopulateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder
                .InsertData
                (
                    "Artist",
                    new string[] { "Name", "Bio", "ProfilePicture" },
                    new object[] { "Outsiders", "Israel psytrance fullon v2", "image_url" }
                );

            migrationBuilder
                .InsertData
                (
                    "Artist",
                    new string[] { "Name", "Bio", "ProfilePicture" },
                    new object[] { "Astrix", "Israel psytrance king v2", "image_url" }
                );

            migrationBuilder
                .InsertData
                (
                    "Artist",
                    new string[] { "Name", "Bio", "ProfilePicture" },
                    new object[] { "Vegas", "Brazil psytrance most know v2", "image_url" }
                );

            migrationBuilder
                .InsertData
                (
                    "Artist",
                    new string[] { "Name", "Bio", "ProfilePicture" },
                    new object[] { "Spectro Senses", "Brazil psytrance scientist v2", "image_url" }
                );

            migrationBuilder
                .InsertData
                (
                    "Artist",
                    new string[] { "Name", "Bio", "ProfilePicture" },
                    new object[] { "Indio", "Brazil psytrance retired v2", "image_url" }
                );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Artist");
        }
    }
}
