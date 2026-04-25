using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FitCore_API.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:uuid-ossp", ",,");

            migrationBuilder.CreateTable(
                name: "locations",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    address = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_locations", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "membership_types",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    duration = table.Column<int>(type: "integer", nullable: false),
                    price = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_membership_types", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    email = table.Column<string>(type: "character varying(254)", maxLength: 254, nullable: false),
                    first_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    last_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    phone_number = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    user_type = table.Column<string>(type: "text", nullable: false),
                    password_hash = table.Column<string>(type: "character varying(44)", maxLength: 44, nullable: false),
                    password_salt = table.Column<string>(type: "character varying(24)", maxLength: 24, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "equipments",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    location_id = table.Column<Guid>(type: "uuid", nullable: false),
                    equipment_type = table.Column<string>(type: "text", nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_equipments", x => x.id);
                    table.ForeignKey(
                        name: "FK_equipments_locations_location_id",
                        column: x => x.location_id,
                        principalSchema: "public",
                        principalTable: "locations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "rooms",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    location_id = table.Column<Guid>(type: "uuid", nullable: false),
                    room_type = table.Column<string>(type: "text", nullable: false),
                    capacity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rooms", x => x.id);
                    table.ForeignKey(
                        name: "FK_rooms_locations_location_id",
                        column: x => x.location_id,
                        principalSchema: "public",
                        principalTable: "locations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "admins",
                schema: "public",
                columns: table => new
                {
                    user_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_admins", x => x.user_id);
                    table.ForeignKey(
                        name: "FK_admins_users_user_id",
                        column: x => x.user_id,
                        principalSchema: "public",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "clients",
                schema: "public",
                columns: table => new
                {
                    user_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clients", x => x.user_id);
                    table.ForeignKey(
                        name: "FK_clients_users_user_id",
                        column: x => x.user_id,
                        principalSchema: "public",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "coaches",
                schema: "public",
                columns: table => new
                {
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    specialization = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_coaches", x => x.user_id);
                    table.ForeignKey(
                        name: "FK_coaches_users_user_id",
                        column: x => x.user_id,
                        principalSchema: "public",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "managers",
                schema: "public",
                columns: table => new
                {
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    location_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_managers", x => x.user_id);
                    table.ForeignKey(
                        name: "FK_managers_locations_location_id",
                        column: x => x.location_id,
                        principalSchema: "public",
                        principalTable: "locations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_managers_users_user_id",
                        column: x => x.user_id,
                        principalSchema: "public",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "room_equipments",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    location_id = table.Column<Guid>(type: "uuid", nullable: false),
                    room_id = table.Column<Guid>(type: "uuid", nullable: false),
                    equipment_type = table.Column<string>(type: "text", nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_room_equipments", x => x.id);
                    table.ForeignKey(
                        name: "FK_room_equipments_locations_location_id",
                        column: x => x.location_id,
                        principalSchema: "public",
                        principalTable: "locations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_room_equipments_rooms_room_id",
                        column: x => x.room_id,
                        principalSchema: "public",
                        principalTable: "rooms",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "client_membership",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    membership_type_id = table.Column<Guid>(type: "uuid", nullable: false),
                    client_id = table.Column<Guid>(type: "uuid", nullable: false),
                    start_date = table.Column<DateOnly>(type: "date", nullable: false),
                    end_date = table.Column<DateOnly>(type: "date", nullable: false),
                    status = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_client_membership", x => x.id);
                    table.ForeignKey(
                        name: "FK_client_membership_clients_client_id",
                        column: x => x.client_id,
                        principalSchema: "public",
                        principalTable: "clients",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_client_membership_membership_types_membership_type_id",
                        column: x => x.membership_type_id,
                        principalSchema: "public",
                        principalTable: "membership_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "group_training_sessions",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    coach_id = table.Column<Guid>(type: "uuid", nullable: false),
                    room_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    capacity = table.Column<int>(type: "integer", nullable: false),
                    start_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    end_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_group_training_sessions", x => x.id);
                    table.ForeignKey(
                        name: "FK_group_training_sessions_coaches_coach_id",
                        column: x => x.coach_id,
                        principalSchema: "public",
                        principalTable: "coaches",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_group_training_sessions_rooms_room_id",
                        column: x => x.room_id,
                        principalSchema: "public",
                        principalTable: "rooms",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "personal_trainings_sessions",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    client_id = table.Column<Guid>(type: "uuid", nullable: false),
                    coach_id = table.Column<Guid>(type: "uuid", nullable: false),
                    room_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    start_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    end_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_personal_trainings_sessions", x => x.id);
                    table.ForeignKey(
                        name: "FK_personal_trainings_sessions_clients_client_id",
                        column: x => x.client_id,
                        principalSchema: "public",
                        principalTable: "clients",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_personal_trainings_sessions_coaches_coach_id",
                        column: x => x.coach_id,
                        principalSchema: "public",
                        principalTable: "coaches",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_personal_trainings_sessions_rooms_room_id",
                        column: x => x.room_id,
                        principalSchema: "public",
                        principalTable: "rooms",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "group_training_session_client",
                schema: "public",
                columns: table => new
                {
                    group_training_session_id = table.Column<Guid>(type: "uuid", nullable: false),
                    client_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_group_training_session_client", x => new { x.group_training_session_id, x.client_id });
                    table.ForeignKey(
                        name: "FK_group_training_session_client_clients_client_id",
                        column: x => x.client_id,
                        principalSchema: "public",
                        principalTable: "clients",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_group_training_session_client_group_training_sessions_group~",
                        column: x => x.group_training_session_id,
                        principalSchema: "public",
                        principalTable: "group_training_sessions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "occupied_equipments",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    session_id = table.Column<Guid>(type: "uuid", nullable: false),
                    equipment_id = table.Column<Guid>(type: "uuid", nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_occupied_equipments", x => x.id);
                    table.ForeignKey(
                        name: "FK_occupied_equipments_group_training_sessions_session_id",
                        column: x => x.session_id,
                        principalSchema: "public",
                        principalTable: "group_training_sessions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_occupied_equipments_room_equipments_equipment_id",
                        column: x => x.equipment_id,
                        principalSchema: "public",
                        principalTable: "room_equipments",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_client_membership_client_id",
                schema: "public",
                table: "client_membership",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "IX_client_membership_membership_type_id",
                schema: "public",
                table: "client_membership",
                column: "membership_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_equipments_location_id",
                schema: "public",
                table: "equipments",
                column: "location_id");

            migrationBuilder.CreateIndex(
                name: "IX_group_training_session_client_client_id",
                schema: "public",
                table: "group_training_session_client",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "IX_group_training_sessions_coach_id",
                schema: "public",
                table: "group_training_sessions",
                column: "coach_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_group_training_sessions_room_id",
                schema: "public",
                table: "group_training_sessions",
                column: "room_id");

            migrationBuilder.CreateIndex(
                name: "IX_managers_location_id",
                schema: "public",
                table: "managers",
                column: "location_id");

            migrationBuilder.CreateIndex(
                name: "IX_occupied_equipments_equipment_id",
                schema: "public",
                table: "occupied_equipments",
                column: "equipment_id");

            migrationBuilder.CreateIndex(
                name: "IX_occupied_equipments_session_id",
                schema: "public",
                table: "occupied_equipments",
                column: "session_id");

            migrationBuilder.CreateIndex(
                name: "IX_personal_trainings_sessions_client_id",
                schema: "public",
                table: "personal_trainings_sessions",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "IX_personal_trainings_sessions_coach_id",
                schema: "public",
                table: "personal_trainings_sessions",
                column: "coach_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_personal_trainings_sessions_room_id",
                schema: "public",
                table: "personal_trainings_sessions",
                column: "room_id");

            migrationBuilder.CreateIndex(
                name: "IX_room_equipments_location_id",
                schema: "public",
                table: "room_equipments",
                column: "location_id");

            migrationBuilder.CreateIndex(
                name: "IX_room_equipments_room_id",
                schema: "public",
                table: "room_equipments",
                column: "room_id");

            migrationBuilder.CreateIndex(
                name: "IX_rooms_location_id",
                schema: "public",
                table: "rooms",
                column: "location_id");

            migrationBuilder.CreateIndex(
                name: "IX_users_email",
                schema: "public",
                table: "users",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_phone_number",
                schema: "public",
                table: "users",
                column: "phone_number",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "admins",
                schema: "public");

            migrationBuilder.DropTable(
                name: "client_membership",
                schema: "public");

            migrationBuilder.DropTable(
                name: "equipments",
                schema: "public");

            migrationBuilder.DropTable(
                name: "group_training_session_client",
                schema: "public");

            migrationBuilder.DropTable(
                name: "managers",
                schema: "public");

            migrationBuilder.DropTable(
                name: "occupied_equipments",
                schema: "public");

            migrationBuilder.DropTable(
                name: "personal_trainings_sessions",
                schema: "public");

            migrationBuilder.DropTable(
                name: "membership_types",
                schema: "public");

            migrationBuilder.DropTable(
                name: "group_training_sessions",
                schema: "public");

            migrationBuilder.DropTable(
                name: "room_equipments",
                schema: "public");

            migrationBuilder.DropTable(
                name: "clients",
                schema: "public");

            migrationBuilder.DropTable(
                name: "coaches",
                schema: "public");

            migrationBuilder.DropTable(
                name: "rooms",
                schema: "public");

            migrationBuilder.DropTable(
                name: "users",
                schema: "public");

            migrationBuilder.DropTable(
                name: "locations",
                schema: "public");
        }
    }
}
