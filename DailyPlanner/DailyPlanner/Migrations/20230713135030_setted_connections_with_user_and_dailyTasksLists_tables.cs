using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DailyPlanner.Migrations
{
    /// <inheritdoc />
    public partial class setted_connections_with_user_and_dailyTasksLists_tables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserEntityId",
                table: "DailyTasksLists",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DailyTasksLists_UserEntityId",
                table: "DailyTasksLists",
                column: "UserEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_DailyTasksLists_Users_UserEntityId",
                table: "DailyTasksLists",
                column: "UserEntityId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DailyTasksLists_Users_UserEntityId",
                table: "DailyTasksLists");

            migrationBuilder.DropIndex(
                name: "IX_DailyTasksLists_UserEntityId",
                table: "DailyTasksLists");

            migrationBuilder.DropColumn(
                name: "UserEntityId",
                table: "DailyTasksLists");
        }
    }
}
