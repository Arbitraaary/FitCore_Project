using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitCoreAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddCoachLocation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "location_id",
                schema: "public",
                table: "coaches",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_coaches_locations_location_id",
                schema: "public",
                table: "coaches");

            migrationBuilder.DropIndex(
                name: "IX_coaches_location_id",
                schema: "public",
                table: "coaches");

            migrationBuilder.DropColumn(
                name: "location_id",
                schema: "public",
                table: "coaches");
        }
    }
}
