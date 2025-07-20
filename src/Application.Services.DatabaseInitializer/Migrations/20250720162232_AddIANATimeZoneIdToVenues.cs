using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Application.Services.DatabaseInitializer.Migrations
{
    /// <inheritdoc />
    public partial class AddIANATimeZoneIdToVenues : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "ix_venues_category_id",
                table: "venues",
                newName: "ix_venues_primary_category_id");

            migrationBuilder.AddColumn<string>(
                name: "time_zone_id",
                table: "venues",
                type: "character varying(32)",
                maxLength: 32,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "ix_venues_time_zone_id",
                table: "venues",
                column: "time_zone_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_venues_time_zone_id",
                table: "venues");

            migrationBuilder.DropColumn(
                name: "time_zone_id",
                table: "venues");

            migrationBuilder.RenameIndex(
                name: "ix_venues_primary_category_id",
                table: "venues",
                newName: "ix_venues_category_id");
        }
    }
}
