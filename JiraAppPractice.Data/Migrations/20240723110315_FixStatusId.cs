using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JiraAppPractice.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixStatusId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Statuses_StatusesId",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Users_UserId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_StatusesId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_UserId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "StatusesId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Tasks");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_AsigneeId",
                table: "Tasks",
                column: "AsigneeId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_StatusId",
                table: "Tasks",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Statuses_StatusId",
                table: "Tasks",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Users_AsigneeId",
                table: "Tasks",
                column: "AsigneeId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Statuses_StatusId",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Users_AsigneeId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_AsigneeId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_StatusId",
                table: "Tasks");

            migrationBuilder.AddColumn<int>(
                name: "StatusesId",
                table: "Tasks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Tasks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_StatusesId",
                table: "Tasks",
                column: "StatusesId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_UserId",
                table: "Tasks",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Statuses_StatusesId",
                table: "Tasks",
                column: "StatusesId",
                principalTable: "Statuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Users_UserId",
                table: "Tasks",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
