using Microsoft.EntityFrameworkCore.Migrations;

namespace AniRateV1.DAL.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AnimeCollections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimeCollections", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AnimeTitles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimeTitles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AnimeCollectionAnimeTitle",
                columns: table => new
                {
                    AnimeCollectionsId = table.Column<int>(type: "int", nullable: false),
                    AnimeTitlesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimeCollectionAnimeTitle", x => new { x.AnimeCollectionsId, x.AnimeTitlesId });
                    table.ForeignKey(
                        name: "FK_AnimeCollectionAnimeTitle_AnimeCollections_AnimeCollectionsId",
                        column: x => x.AnimeCollectionsId,
                        principalTable: "AnimeCollections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnimeCollectionAnimeTitle_AnimeTitles_AnimeTitlesId",
                        column: x => x.AnimeTitlesId,
                        principalTable: "AnimeTitles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnimeCollectionAnimeTitle_AnimeTitlesId",
                table: "AnimeCollectionAnimeTitle",
                column: "AnimeTitlesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnimeCollectionAnimeTitle");

            migrationBuilder.DropTable(
                name: "AnimeCollections");

            migrationBuilder.DropTable(
                name: "AnimeTitles");
        }
    }
}
