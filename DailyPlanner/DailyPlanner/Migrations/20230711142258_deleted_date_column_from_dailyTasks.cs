using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DailyPlanner.Migrations
{
    /// <inheritdoc />
    public partial class deleted_date_column_from_dailyTasks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "DailyTasks");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Date",
                table: "DailyTasks",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
