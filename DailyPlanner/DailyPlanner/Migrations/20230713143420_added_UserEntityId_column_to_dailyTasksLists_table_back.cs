using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DailyPlanner.Migrations
{
    /// <inheritdoc />
    public partial class added_UserEntityId_column_to_dailyTasksLists_table_back : Migration
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
    }
}
