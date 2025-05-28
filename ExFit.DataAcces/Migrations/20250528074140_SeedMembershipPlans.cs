using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ExFit.DataAcces.Migrations
{
    /// <inheritdoc />
    public partial class SeedMembershipPlans : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "MembershipPlans",
                columns: new[] { "Id", "DurationInDays", "Name", "Price" },
                values: new object[,]
                {
                    { 1, 30, "1 Month", 9.99m },
                    { 2, 90, "3 Months", 24.99m },
                    { 3, 180, "6 Months", 44.99m },
                    { 4, 365, "1 Year", 79.99m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MembershipPlans",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "MembershipPlans",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "MembershipPlans",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "MembershipPlans",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
