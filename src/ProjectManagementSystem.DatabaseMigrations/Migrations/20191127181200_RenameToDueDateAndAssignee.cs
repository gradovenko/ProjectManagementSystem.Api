using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectManagementSystem.DatabaseMigrations.Migrations
{
    public partial class RenameToDueDateAndAssignee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Issue_User_PerformerId",
                table: "Issue");

            migrationBuilder.DropIndex(
                name: "IX_Issue_PerformerId",
                table: "Issue");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Issue");

            migrationBuilder.DropColumn(
                name: "PerformerId",
                table: "Issue");

            migrationBuilder.AddColumn<Guid>(
                name: "AssigneeId",
                table: "Issue",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DueDate",
                table: "Issue",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("0ae12bbd-58ef-4c2e-87a6-2c2cb3f9592d"),
                column: "ConcurrencyStamp",
                value: new Guid("8d150a60-5a55-4e14-a43a-a1bd43d21ab0"));

            migrationBuilder.CreateIndex(
                name: "IX_Issue_AssigneeId",
                table: "Issue",
                column: "AssigneeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Issue_User_AssigneeId",
                table: "Issue",
                column: "AssigneeId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Issue_User_AssigneeId",
                table: "Issue");

            migrationBuilder.DropIndex(
                name: "IX_Issue_AssigneeId",
                table: "Issue");

            migrationBuilder.DropColumn(
                name: "AssigneeId",
                table: "Issue");

            migrationBuilder.DropColumn(
                name: "DueDate",
                table: "Issue");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Issue",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PerformerId",
                table: "Issue",
                type: "uuid",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("0ae12bbd-58ef-4c2e-87a6-2c2cb3f9592d"),
                column: "ConcurrencyStamp",
                value: new Guid("24a3ac21-18e1-468a-931c-538acb6391b4"));

            migrationBuilder.CreateIndex(
                name: "IX_Issue_PerformerId",
                table: "Issue",
                column: "PerformerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Issue_User_PerformerId",
                table: "Issue",
                column: "PerformerId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
