using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitCoreAPI.Migrations
{
    /// <inheritdoc />
    public partial class ChangeLocationModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_coaches_locations_location_id",
                schema: "public",
                table: "coaches");

            migrationBuilder.DropForeignKey(
                name: "FK_equipments_locations_location_id",
                schema: "public",
                table: "equipments");

            migrationBuilder.DropForeignKey(
                name: "FK_managers_locations_location_id",
                schema: "public",
                table: "managers");

            migrationBuilder.DropForeignKey(
                name: "FK_room_equipments_locations_location_id",
                schema: "public",
                table: "room_equipments");

            migrationBuilder.DropForeignKey(
                name: "FK_rooms_locations_location_id",
                schema: "public",
                table: "rooms");

            migrationBuilder.DropIndex(
                name: "IX_rooms_location_id",
                schema: "public",
                table: "rooms");

            migrationBuilder.DropIndex(
                name: "IX_room_equipments_location_id",
                schema: "public",
                table: "room_equipments");

            migrationBuilder.DropIndex(
                name: "IX_managers_location_id",
                schema: "public",
                table: "managers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_locations",
                schema: "public",
                table: "locations");

            migrationBuilder.DropIndex(
                name: "IX_equipments_location_id",
                schema: "public",
                table: "equipments");

            migrationBuilder.DropIndex(
                name: "IX_coaches_location_id",
                schema: "public",
                table: "coaches");

            migrationBuilder.DropColumn(
                name: "location_id",
                schema: "public",
                table: "rooms");

            migrationBuilder.DropColumn(
                name: "location_id",
                schema: "public",
                table: "room_equipments");

            migrationBuilder.DropColumn(
                name: "location_id",
                schema: "public",
                table: "managers");

            migrationBuilder.DropColumn(
                name: "id",
                schema: "public",
                table: "locations");

            migrationBuilder.DropColumn(
                name: "location_id",
                schema: "public",
                table: "equipments");

            migrationBuilder.DropColumn(
                name: "location_id",
                schema: "public",
                table: "coaches");

            migrationBuilder.AddColumn<string>(
                name: "location_name",
                schema: "public",
                table: "rooms",
                type: "character varying(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "location_name",
                schema: "public",
                table: "room_equipments",
                type: "character varying(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "location_name",
                schema: "public",
                table: "managers",
                type: "character varying(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "location_name",
                schema: "public",
                table: "equipments",
                type: "character varying(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "location_name",
                schema: "public",
                table: "coaches",
                type: "character varying(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_locations",
                schema: "public",
                table: "locations",
                column: "name");

            migrationBuilder.CreateIndex(
                name: "IX_rooms_location_name",
                schema: "public",
                table: "rooms",
                column: "location_name");

            migrationBuilder.CreateIndex(
                name: "IX_room_equipments_location_name",
                schema: "public",
                table: "room_equipments",
                column: "location_name");

            migrationBuilder.CreateIndex(
                name: "IX_managers_location_name",
                schema: "public",
                table: "managers",
                column: "location_name");

            migrationBuilder.CreateIndex(
                name: "IX_equipments_location_name",
                schema: "public",
                table: "equipments",
                column: "location_name");

            migrationBuilder.CreateIndex(
                name: "IX_coaches_location_name",
                schema: "public",
                table: "coaches",
                column: "location_name");

            migrationBuilder.AddForeignKey(
                name: "FK_coaches_locations_location_name",
                schema: "public",
                table: "coaches",
                column: "location_name",
                principalSchema: "public",
                principalTable: "locations",
                principalColumn: "name",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_equipments_locations_location_name",
                schema: "public",
                table: "equipments",
                column: "location_name",
                principalSchema: "public",
                principalTable: "locations",
                principalColumn: "name",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_managers_locations_location_name",
                schema: "public",
                table: "managers",
                column: "location_name",
                principalSchema: "public",
                principalTable: "locations",
                principalColumn: "name",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_room_equipments_locations_location_name",
                schema: "public",
                table: "room_equipments",
                column: "location_name",
                principalSchema: "public",
                principalTable: "locations",
                principalColumn: "name",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_rooms_locations_location_name",
                schema: "public",
                table: "rooms",
                column: "location_name",
                principalSchema: "public",
                principalTable: "locations",
                principalColumn: "name",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_coaches_locations_location_name",
                schema: "public",
                table: "coaches");

            migrationBuilder.DropForeignKey(
                name: "FK_equipments_locations_location_name",
                schema: "public",
                table: "equipments");

            migrationBuilder.DropForeignKey(
                name: "FK_managers_locations_location_name",
                schema: "public",
                table: "managers");

            migrationBuilder.DropForeignKey(
                name: "FK_room_equipments_locations_location_name",
                schema: "public",
                table: "room_equipments");

            migrationBuilder.DropForeignKey(
                name: "FK_rooms_locations_location_name",
                schema: "public",
                table: "rooms");

            migrationBuilder.DropIndex(
                name: "IX_rooms_location_name",
                schema: "public",
                table: "rooms");

            migrationBuilder.DropIndex(
                name: "IX_room_equipments_location_name",
                schema: "public",
                table: "room_equipments");

            migrationBuilder.DropIndex(
                name: "IX_managers_location_name",
                schema: "public",
                table: "managers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_locations",
                schema: "public",
                table: "locations");

            migrationBuilder.DropIndex(
                name: "IX_equipments_location_name",
                schema: "public",
                table: "equipments");

            migrationBuilder.DropIndex(
                name: "IX_coaches_location_name",
                schema: "public",
                table: "coaches");

            migrationBuilder.DropColumn(
                name: "location_name",
                schema: "public",
                table: "rooms");

            migrationBuilder.DropColumn(
                name: "location_name",
                schema: "public",
                table: "room_equipments");

            migrationBuilder.DropColumn(
                name: "location_name",
                schema: "public",
                table: "managers");

            migrationBuilder.DropColumn(
                name: "location_name",
                schema: "public",
                table: "equipments");

            migrationBuilder.DropColumn(
                name: "location_name",
                schema: "public",
                table: "coaches");

            migrationBuilder.AddColumn<Guid>(
                name: "location_id",
                schema: "public",
                table: "rooms",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "location_id",
                schema: "public",
                table: "room_equipments",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "location_id",
                schema: "public",
                table: "managers",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "id",
                schema: "public",
                table: "locations",
                type: "uuid",
                nullable: false,
                defaultValueSql: "uuid_generate_v4()");

            migrationBuilder.AddColumn<Guid>(
                name: "location_id",
                schema: "public",
                table: "equipments",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "location_id",
                schema: "public",
                table: "coaches",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_locations",
                schema: "public",
                table: "locations",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_rooms_location_id",
                schema: "public",
                table: "rooms",
                column: "location_id");

            migrationBuilder.CreateIndex(
                name: "IX_room_equipments_location_id",
                schema: "public",
                table: "room_equipments",
                column: "location_id");

            migrationBuilder.CreateIndex(
                name: "IX_managers_location_id",
                schema: "public",
                table: "managers",
                column: "location_id");

            migrationBuilder.CreateIndex(
                name: "IX_equipments_location_id",
                schema: "public",
                table: "equipments",
                column: "location_id");

            migrationBuilder.CreateIndex(
                name: "IX_coaches_location_id",
                schema: "public",
                table: "coaches",
                column: "location_id");

            migrationBuilder.AddForeignKey(
                name: "FK_coaches_locations_location_id",
                schema: "public",
                table: "coaches",
                column: "location_id",
                principalSchema: "public",
                principalTable: "locations",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_equipments_locations_location_id",
                schema: "public",
                table: "equipments",
                column: "location_id",
                principalSchema: "public",
                principalTable: "locations",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_managers_locations_location_id",
                schema: "public",
                table: "managers",
                column: "location_id",
                principalSchema: "public",
                principalTable: "locations",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_room_equipments_locations_location_id",
                schema: "public",
                table: "room_equipments",
                column: "location_id",
                principalSchema: "public",
                principalTable: "locations",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_rooms_locations_location_id",
                schema: "public",
                table: "rooms",
                column: "location_id",
                principalSchema: "public",
                principalTable: "locations",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
