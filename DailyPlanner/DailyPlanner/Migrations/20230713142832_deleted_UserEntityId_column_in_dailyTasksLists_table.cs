using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DailyPlanner.Migrations
{
    /// <inheritdoc />
    public partial class deleted_UserEntityId_column_in_dailyTasksLists_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DailyTasksLists_Users_UserEntityId",
                table: "DailyTasksLists");

            migrationBuilder.AlterColumn<int>(
                name: "UserEntityId",
                table: "DailyTasksLists",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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

            migrationBuilder.AlterColumn<int>(
                name: "UserEntityId",
                table: "DailyTasksLists",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DailyTasksLists_Users_UserEntityId",
                table: "DailyTasksLists",
                column: "UserEntityId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
