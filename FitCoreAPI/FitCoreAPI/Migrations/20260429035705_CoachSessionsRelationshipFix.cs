using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitCoreAPI.Migrations
{
    /// <inheritdoc />
    public partial class CoachSessionsRelationshipFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_personal_trainings_sessions_coach_id",
                schema: "public",
                table: "personal_trainings_sessions");

            migrationBuilder.DropIndex(
                name: "IX_group_training_sessions_coach_id",
                schema: "public",
                table: "group_training_sessions");

            migrationBuilder.CreateIndex(
                name: "IX_personal_trainings_sessions_coach_id",
                schema: "public",
                table: "personal_trainings_sessions",
                column: "coach_id");

            migrationBuilder.CreateIndex(
                name: "IX_group_training_sessions_coach_id",
                schema: "public",
                table: "group_training_sessions",
                column: "coach_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_personal_trainings_sessions_coach_id",
                schema: "public",
                table: "personal_trainings_sessions");

            migrationBuilder.DropIndex(
                name: "IX_group_training_sessions_coach_id",
                schema: "public",
                table: "group_training_sessions");

            migrationBuilder.CreateIndex(
                name: "IX_personal_trainings_sessions_coach_id",
                schema: "public",
                table: "personal_trainings_sessions",
                column: "coach_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_group_training_sessions_coach_id",
                schema: "public",
                table: "group_training_sessions",
                column: "coach_id",
                unique: true);
        }
    }
}
