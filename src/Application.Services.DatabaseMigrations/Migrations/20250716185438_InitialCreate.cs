using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;
using NodaTime;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Application.Services.DatabaseMigrations.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:address_standardizer", ",,")
                .Annotation("Npgsql:PostgresExtension:address_standardizer_data_us", ",,")
                .Annotation("Npgsql:PostgresExtension:fuzzystrmatch", ",,")
                .Annotation("Npgsql:PostgresExtension:plpgsql", ",,")
                .Annotation("Npgsql:PostgresExtension:postgis", ",,")
                .Annotation("Npgsql:PostgresExtension:postgis_raster", ",,")
                .Annotation("Npgsql:PostgresExtension:postgis_sfcgal", ",,")
                .Annotation("Npgsql:PostgresExtension:postgis_tiger_geocoder", ",,")
                .Annotation("Npgsql:PostgresExtension:postgis_topology", ",,");

            migrationBuilder.CreateTable(
                name: "days_of_week",
                columns: table => new
                {
                    id = table.Column<byte>(type: "smallint", nullable: false),
                    name = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    short_name = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false),
                    iso_number = table.Column<int>(type: "integer", nullable: false),
                    is_weekday = table.Column<bool>(type: "boolean", nullable: false),
                    sort_order = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_days_of_week", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "special_categories",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    icon = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    sort_order = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_special_categories", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    sub = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    email = table.Column<string>(type: "character varying(254)", maxLength: 254, nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    last_login_at = table.Column<Instant>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "venue_categories",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    icon = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    sort_order = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_venue_categories", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "venue_roles",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_venue_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "venues",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    category_id = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    phone_number = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    website = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    profile_image = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    street_address = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    secondary_address = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    locality = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    region = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    postal_code = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    country = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    location = table.Column<Point>(type: "geography (point)", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_venues", x => x.id);
                    table.ForeignKey(
                        name: "fk_venues_venue_categories_category_id",
                        column: x => x.category_id,
                        principalTable: "venue_categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "business_hours",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    venue_id = table.Column<long>(type: "bigint", nullable: false),
                    day_of_week_id = table.Column<byte>(type: "smallint", nullable: false),
                    open_time = table.Column<LocalTime>(type: "time", nullable: true),
                    close_time = table.Column<LocalTime>(type: "time", nullable: true),
                    is_closed = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_business_hours", x => x.id);
                    table.ForeignKey(
                        name: "fk_business_hours_days_of_week_day_of_week_id",
                        column: x => x.day_of_week_id,
                        principalTable: "days_of_week",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_business_hours_venues_venue_id",
                        column: x => x.venue_id,
                        principalTable: "venues",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "special_menus",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    venue_id = table.Column<long>(type: "bigint", nullable: false),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    created_at = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<Instant>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_special_menus", x => x.id);
                    table.ForeignKey(
                        name: "fk_special_menus_venues_venue_id",
                        column: x => x.venue_id,
                        principalTable: "venues",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "venue_invitations",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    email = table.Column<string>(type: "character varying(254)", maxLength: 254, nullable: false),
                    venue_id = table.Column<long>(type: "bigint", nullable: false),
                    role_id = table.Column<int>(type: "integer", nullable: false),
                    invited_by_user_id = table.Column<long>(type: "bigint", nullable: false),
                    invited_at = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    expires_at = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    accepted_at = table.Column<Instant>(type: "timestamp with time zone", nullable: true),
                    accepted_by_user_id = table.Column<long>(type: "bigint", nullable: true),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    notes = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    venue_entity_id = table.Column<long>(type: "bigint", nullable: true),
                    venue_role_entity_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_venue_invitations", x => x.id);
                    table.ForeignKey(
                        name: "fk_venue_invitations_users_accepted_by_user_id",
                        column: x => x.accepted_by_user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_venue_invitations_users_invited_by_user_id",
                        column: x => x.invited_by_user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_venue_invitations_venue_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "venue_roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_venue_invitations_venue_roles_venue_role_entity_id",
                        column: x => x.venue_role_entity_id,
                        principalTable: "venue_roles",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_venue_invitations_venues_venue_entity_id",
                        column: x => x.venue_entity_id,
                        principalTable: "venues",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_venue_invitations_venues_venue_id",
                        column: x => x.venue_id,
                        principalTable: "venues",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "venue_user_roles",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    venue_id = table.Column<long>(type: "bigint", nullable: false),
                    role_id = table.Column<int>(type: "integer", nullable: false),
                    granted_by_user_id = table.Column<long>(type: "bigint", nullable: false),
                    granted_at = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    notes = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_venue_user_roles", x => x.id);
                    table.ForeignKey(
                        name: "fk_venue_user_roles_users_granted_by_user_id",
                        column: x => x.granted_by_user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_venue_user_roles_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_venue_user_roles_venue_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "venue_roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_venue_user_roles_venues_venue_id",
                        column: x => x.venue_id,
                        principalTable: "venues",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "special_menu_schedules",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    special_menu_id = table.Column<long>(type: "bigint", nullable: false),
                    start_date = table.Column<LocalDate>(type: "date", nullable: false),
                    start_time = table.Column<LocalTime>(type: "time", nullable: false),
                    end_time = table.Column<LocalTime>(type: "time", nullable: false),
                    expiration_date = table.Column<LocalDate>(type: "date", nullable: true),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    recurrence_pattern = table.Column<string>(type: "jsonb", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_special_menu_schedules", x => x.id);
                    table.ForeignKey(
                        name: "fk_special_menu_schedules_special_menus_special_menu_id",
                        column: x => x.special_menu_id,
                        principalTable: "special_menus",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "specials",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    venue_id = table.Column<long>(type: "bigint", nullable: false),
                    special_category_id = table.Column<int>(type: "integer", nullable: false),
                    description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    special_menu_id = table.Column<long>(type: "bigint", nullable: true),
                    additional_data = table.Column<string>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_specials", x => x.id);
                    table.ForeignKey(
                        name: "fk_specials_special_categories_special_category_id",
                        column: x => x.special_category_id,
                        principalTable: "special_categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_specials_special_menus_special_menu_id",
                        column: x => x.special_menu_id,
                        principalTable: "special_menus",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_specials_venues_venue_id",
                        column: x => x.venue_id,
                        principalTable: "venues",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "days_of_week",
                columns: new[] { "id", "is_weekday", "iso_number", "name", "short_name", "sort_order" },
                values: new object[,]
                {
                    { (byte)1, false, 7, "Sunday", "SUN", 1 },
                    { (byte)2, true, 1, "Monday", "MON", 2 },
                    { (byte)3, true, 2, "Tuesday", "TUE", 3 },
                    { (byte)4, true, 3, "Wednesday", "WED", 4 },
                    { (byte)5, true, 4, "Thursday", "THU", 5 },
                    { (byte)6, true, 5, "Friday", "FRI", 6 },
                    { (byte)7, false, 6, "Saturday", "SAT", 7 }
                });

            migrationBuilder.InsertData(
                table: "special_categories",
                columns: new[] { "id", "description", "icon", "name", "sort_order" },
                values: new object[,]
                {
                    { 1, "Food specials, appetizers, and meal deals", "🍔", "Food", 1 },
                    { 2, "Drink specials, happy hours, and beverage promotions", "🍺", "Drink", 2 },
                    { 3, "Live music, DJs, trivia, karaoke, and other events", "🎵", "Entertainment", 3 }
                });

            migrationBuilder.InsertData(
                table: "venue_categories",
                columns: new[] { "id", "description", "icon", "name", "sort_order" },
                values: new object[,]
                {
                    { 1, "Dining establishments offering food and beverages", "🍽️", "Restaurant", 1 },
                    { 2, "Venues focused on drinks and nightlife", "🍸", "Bar", 2 },
                    { 3, "Casual spots for coffee and light meals", "☕", "Cafe", 3 },
                    { 4, "Venues for dancing and late-night entertainment", "🌃", "Nightclub", 4 },
                    { 5, "Casual venues with food, drinks, and often live music", "🍺", "Pub", 5 },
                    { 6, "Venues producing wine, offering tastings, food pairings, and live music", "🍷", "Winery", 6 },
                    { 7, "Venues brewing their own beer, often with food and live music", "🍻", "Brewery", 7 },
                    { 8, "Sophisticated venues with cocktails, small plates, and live music", "🛋️", "Lounge", 8 },
                    { 9, "Intimate dining venues with quality food, wine, and occasional live music", "🥂", "Bistro", 9 }
                });

            migrationBuilder.InsertData(
                table: "venue_roles",
                columns: new[] { "id", "description", "name" },
                values: new object[,]
                {
                    { 1, "Full control over venue settings, staff, and content", "venue-owner" },
                    { 2, "Can manage venue content, specials, and view reports", "venue-manager" },
                    { 3, "Can update specials and business hours", "venue-staff" }
                });

            migrationBuilder.CreateIndex(
                name: "ix_business_hours_day_of_week_id",
                table: "business_hours",
                column: "day_of_week_id");

            migrationBuilder.CreateIndex(
                name: "ix_business_hours_venue_day",
                table: "business_hours",
                columns: new[] { "venue_id", "day_of_week_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_days_of_week_iso_number",
                table: "days_of_week",
                column: "iso_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_special_categories_name",
                table: "special_categories",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_special_menu_schedules_is_active",
                table: "special_menu_schedules",
                column: "is_active");

            migrationBuilder.CreateIndex(
                name: "ix_special_menu_schedules_menu_active",
                table: "special_menu_schedules",
                columns: new[] { "special_menu_id", "is_active" });

            migrationBuilder.CreateIndex(
                name: "ix_special_menu_schedules_menu_id",
                table: "special_menu_schedules",
                column: "special_menu_id");

            migrationBuilder.CreateIndex(
                name: "ix_special_menus_venue_id",
                table: "special_menus",
                column: "venue_id");

            migrationBuilder.CreateIndex(
                name: "ix_special_menus_venue_name",
                table: "special_menus",
                columns: new[] { "venue_id", "name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_specials_category_id",
                table: "specials",
                column: "special_category_id");

            migrationBuilder.CreateIndex(
                name: "ix_specials_is_active",
                table: "specials",
                column: "is_active");

            migrationBuilder.CreateIndex(
                name: "ix_specials_special_menu_id",
                table: "specials",
                column: "special_menu_id");

            migrationBuilder.CreateIndex(
                name: "ix_specials_venue_active",
                table: "specials",
                columns: new[] { "venue_id", "is_active" });

            migrationBuilder.CreateIndex(
                name: "ix_specials_venue_id",
                table: "specials",
                column: "venue_id");

            migrationBuilder.CreateIndex(
                name: "ix_users_email",
                table: "users",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_users_is_active",
                table: "users",
                column: "is_active");

            migrationBuilder.CreateIndex(
                name: "ix_users_sub",
                table: "users",
                column: "sub",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_venue_categories_name",
                table: "venue_categories",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_venue_invitations_accepted_by_user_id",
                table: "venue_invitations",
                column: "accepted_by_user_id");

            migrationBuilder.CreateIndex(
                name: "ix_venue_invitations_active_accepted",
                table: "venue_invitations",
                columns: new[] { "is_active", "accepted_at" });

            migrationBuilder.CreateIndex(
                name: "ix_venue_invitations_email_venue_active",
                table: "venue_invitations",
                columns: new[] { "email", "venue_id", "is_active" });

            migrationBuilder.CreateIndex(
                name: "ix_venue_invitations_expires_at",
                table: "venue_invitations",
                column: "expires_at");

            migrationBuilder.CreateIndex(
                name: "ix_venue_invitations_invited_by",
                table: "venue_invitations",
                column: "invited_by_user_id");

            migrationBuilder.CreateIndex(
                name: "ix_venue_invitations_role_id",
                table: "venue_invitations",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "ix_venue_invitations_venue_entity_id",
                table: "venue_invitations",
                column: "venue_entity_id");

            migrationBuilder.CreateIndex(
                name: "ix_venue_invitations_venue_id",
                table: "venue_invitations",
                column: "venue_id");

            migrationBuilder.CreateIndex(
                name: "ix_venue_invitations_venue_role_entity_id",
                table: "venue_invitations",
                column: "venue_role_entity_id");

            migrationBuilder.CreateIndex(
                name: "ix_venue_roles_name",
                table: "venue_roles",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_venue_user_roles_granted_by_user_id",
                table: "venue_user_roles",
                column: "granted_by_user_id");

            migrationBuilder.CreateIndex(
                name: "ix_venue_user_roles_is_active",
                table: "venue_user_roles",
                column: "is_active");

            migrationBuilder.CreateIndex(
                name: "ix_venue_user_roles_role_id",
                table: "venue_user_roles",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "ix_venue_user_roles_user_venue",
                table: "venue_user_roles",
                columns: new[] { "user_id", "venue_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_venue_user_roles_venue_id",
                table: "venue_user_roles",
                column: "venue_id");

            migrationBuilder.CreateIndex(
                name: "ix_venue_user_roles_venue_role_active",
                table: "venue_user_roles",
                columns: new[] { "venue_id", "role_id", "is_active" });

            migrationBuilder.CreateIndex(
                name: "ix_venues_active_category",
                table: "venues",
                columns: new[] { "is_active", "category_id" });

            migrationBuilder.CreateIndex(
                name: "ix_venues_category_id",
                table: "venues",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "ix_venues_is_active",
                table: "venues",
                column: "is_active");

            migrationBuilder.CreateIndex(
                name: "ix_venues_location",
                table: "venues",
                column: "location")
                .Annotation("Npgsql:IndexMethod", "GIST");

            migrationBuilder.CreateIndex(
                name: "ix_venues_name",
                table: "venues",
                column: "name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "business_hours");

            migrationBuilder.DropTable(
                name: "special_menu_schedules");

            migrationBuilder.DropTable(
                name: "specials");

            migrationBuilder.DropTable(
                name: "venue_invitations");

            migrationBuilder.DropTable(
                name: "venue_user_roles");

            migrationBuilder.DropTable(
                name: "days_of_week");

            migrationBuilder.DropTable(
                name: "special_categories");

            migrationBuilder.DropTable(
                name: "special_menus");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "venue_roles");

            migrationBuilder.DropTable(
                name: "venues");

            migrationBuilder.DropTable(
                name: "venue_categories");
        }
    }
}
