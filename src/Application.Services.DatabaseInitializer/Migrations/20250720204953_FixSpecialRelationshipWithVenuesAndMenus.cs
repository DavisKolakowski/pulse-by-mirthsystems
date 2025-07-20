using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Application.Services.DatabaseInitializer.Migrations
{
    /// <inheritdoc />
    public partial class FixSpecialRelationshipWithVenuesAndMenus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_specials_special_menus_special_menu_id",
                table: "specials");

            migrationBuilder.DropIndex(
                name: "ix_specials_venue_active",
                table: "specials");

            migrationBuilder.AlterColumn<Guid>(
                name: "special_menu_id",
                table: "specials",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "fk_specials_special_menus_special_menu_id",
                table: "specials",
                column: "special_menu_id",
                principalTable: "special_menus",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_specials_special_menus_special_menu_id",
                table: "specials");

            migrationBuilder.AlterColumn<Guid>(
                name: "special_menu_id",
                table: "specials",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.CreateIndex(
                name: "ix_specials_venue_active",
                table: "specials",
                columns: new[] { "venue_id", "is_active" });

            migrationBuilder.AddForeignKey(
                name: "fk_specials_special_menus_special_menu_id",
                table: "specials",
                column: "special_menu_id",
                principalTable: "special_menus",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
