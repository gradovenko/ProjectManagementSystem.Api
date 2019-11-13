using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectManagementSystem.DatabaseMigrations.Migrations
{
    public partial class AddNullablePerformerIssueTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Issue_User_PerformerId",
                table: "Issue");

            migrationBuilder.AlterColumn<Guid>(
                name: "PerformerId",
                table: "Issue",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("0ae12bbd-58ef-4c2e-87a6-2c2cb3f9592d"),
                column: "ConcurrencyStamp",
                value: new Guid("48515d83-90b4-4592-b0af-a6093a770997"));

            migrationBuilder.AddForeignKey(
                name: "FK_Issue_User_PerformerId",
                table: "Issue",
                column: "PerformerId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Issue_User_PerformerId",
                table: "Issue");

            migrationBuilder.AlterColumn<Guid>(
                name: "PerformerId",
                table: "Issue",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("0ae12bbd-58ef-4c2e-87a6-2c2cb3f9592d"),
                column: "ConcurrencyStamp",
                value: new Guid("1e7f3345-0841-441f-bcdb-1ab41d4453c7"));

            migrationBuilder.AddForeignKey(
                name: "FK_Issue_User_PerformerId",
                table: "Issue",
                column: "PerformerId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
