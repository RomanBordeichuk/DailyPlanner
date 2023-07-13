using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DailyPlanner.Migrations
{
    /// <inheritdoc />
    public partial class changed_date_column_in_dailyTasksLists_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DailyTasks_DailyTasksLists_DailyTasksListId",
                table: "DailyTasks");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "DailyTasksLists",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DailyTasksListId",
                table: "DailyTasks",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DailyTasks_DailyTasksLists_DailyTasksListId",
                table: "DailyTasks",
                column: "DailyTasksListId",
                principalTable: "DailyTasksLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DailyTasks_DailyTasksLists_DailyTasksListId",
                table: "DailyTasks");

            migrationBuilder.AlterColumn<string>(
                name: "Date",
                table: "DailyTasksLists",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<int>(
                name: "DailyTasksListId",
                table: "DailyTasks",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_DailyTasks_DailyTasksLists_DailyTasksListId",
                table: "DailyTasks",
                column: "DailyTasksListId",
                principalTable: "DailyTasksLists",
                principalColumn: "Id");
        }
    }
}
