using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CupyVoiceAPI.Migrations
{
    /// <inheritdoc />
    public partial class HybridAnnotationsAndMappings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Episodes_Seasons_SeasonId",
                table: "Episodes");

            migrationBuilder.DropForeignKey(
                name: "FK_Seasons_Series_SeriesId",
                table: "Seasons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Series",
                table: "Series");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Seasons",
                table: "Seasons");

            migrationBuilder.DropIndex(
                name: "IX_Seasons_SeriesId",
                table: "Seasons");

            migrationBuilder.DropIndex(
                name: "IX_Episodes_SeasonId",
                table: "Episodes");

            migrationBuilder.RenameTable(
                name: "Series",
                newName: "series");

            migrationBuilder.RenameTable(
                name: "Seasons",
                newName: "seasons");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "series",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "PosterUrl",
                table: "series",
                type: "character varying(2000)",
                maxLength: 2000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "series",
                type: "character varying(2000)",
                maxLength: 2000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "VideoUrl",
                table: "Episodes",
                type: "character varying(4000)",
                maxLength: 4000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Episodes",
                type: "character varying(300)",
                maxLength: 300,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddPrimaryKey(
                name: "PK_series",
                table: "series",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_seasons",
                table: "seasons",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "player_preferences",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    PreferredQuality = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    PlaybackRate = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_player_preferences", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "watch_progress",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    EpisodeId = table.Column<Guid>(type: "uuid", nullable: false),
                    PositionSeconds = table.Column<int>(type: "integer", nullable: false),
                    UpdatedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_watch_progress", x => new { x.UserId, x.EpisodeId });
                });

            migrationBuilder.CreateIndex(
                name: "IX_seasons_SeriesId_Number",
                table: "seasons",
                columns: new[] { "SeriesId", "Number" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Episodes_SeasonId_Number",
                table: "Episodes",
                columns: new[] { "SeasonId", "Number" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_watch_progress_UpdatedAtUtc",
                table: "watch_progress",
                column: "UpdatedAtUtc");

            migrationBuilder.AddForeignKey(
                name: "FK_Episodes_seasons_SeasonId",
                table: "Episodes",
                column: "SeasonId",
                principalTable: "seasons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_seasons_series_SeriesId",
                table: "seasons",
                column: "SeriesId",
                principalTable: "series",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Episodes_seasons_SeasonId",
                table: "Episodes");

            migrationBuilder.DropForeignKey(
                name: "FK_seasons_series_SeriesId",
                table: "seasons");

            migrationBuilder.DropTable(
                name: "player_preferences");

            migrationBuilder.DropTable(
                name: "watch_progress");

            migrationBuilder.DropPrimaryKey(
                name: "PK_series",
                table: "series");

            migrationBuilder.DropPrimaryKey(
                name: "PK_seasons",
                table: "seasons");

            migrationBuilder.DropIndex(
                name: "IX_seasons_SeriesId_Number",
                table: "seasons");

            migrationBuilder.DropIndex(
                name: "IX_Episodes_SeasonId_Number",
                table: "Episodes");

            migrationBuilder.RenameTable(
                name: "series",
                newName: "Series");

            migrationBuilder.RenameTable(
                name: "seasons",
                newName: "Seasons");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Series",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "PosterUrl",
                table: "Series",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(2000)",
                oldMaxLength: 2000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Series",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(2000)",
                oldMaxLength: 2000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "VideoUrl",
                table: "Episodes",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(4000)",
                oldMaxLength: 4000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Episodes",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(300)",
                oldMaxLength: 300);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Series",
                table: "Series",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Seasons",
                table: "Seasons",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Seasons_SeriesId",
                table: "Seasons",
                column: "SeriesId");

            migrationBuilder.CreateIndex(
                name: "IX_Episodes_SeasonId",
                table: "Episodes",
                column: "SeasonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Episodes_Seasons_SeasonId",
                table: "Episodes",
                column: "SeasonId",
                principalTable: "Seasons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Seasons_Series_SeriesId",
                table: "Seasons",
                column: "SeriesId",
                principalTable: "Series",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
