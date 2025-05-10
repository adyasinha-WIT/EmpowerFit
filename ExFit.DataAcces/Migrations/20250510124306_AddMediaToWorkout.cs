using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExFit.DataAcces.Migrations
{
    /// <inheritdoc />
    public partial class AddMediaToWorkout : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MediaUrl",
                table: "Workouts",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MediaUrl",
                table: "Workouts");
        }
    }
}
