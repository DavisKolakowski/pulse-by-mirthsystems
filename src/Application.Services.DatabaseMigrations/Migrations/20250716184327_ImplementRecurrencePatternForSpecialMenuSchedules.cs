using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Application.Services.DatabaseMigrations.Migrations
{
    /// <inheritdoc />
    public partial class ImplementRecurrencePatternForSpecialMenuSchedules : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_recurring",
                table: "special_menu_schedules");

            migrationBuilder.AlterColumn<string>(
                name: "recurrence_pattern",
                table: "special_menu_schedules",
                type: "jsonb",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "recurrence_pattern",
                table: "special_menu_schedules",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "jsonb");

            migrationBuilder.AddColumn<bool>(
                name: "is_recurring",
                table: "special_menu_schedules",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
