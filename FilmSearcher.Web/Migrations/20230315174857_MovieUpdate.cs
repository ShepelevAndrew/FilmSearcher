using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FilmSearcher.Web.Migrations
{
    /// <inheritdoc />
    public partial class MovieUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "InBookmark",
                table: "MoviesUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "MovieScore",
                table: "MoviesUsers",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InBookmark",
                table: "MoviesUsers");

            migrationBuilder.DropColumn(
                name: "MovieScore",
                table: "MoviesUsers");
        }
    }
}
