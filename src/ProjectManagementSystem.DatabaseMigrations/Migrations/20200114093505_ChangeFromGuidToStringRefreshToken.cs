using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectManagementSystem.DatabaseMigrations.Migrations
{
    public partial class ChangeFromGuidToStringRefreshToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RefreshTokenId",
                table: "RefreshToken",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserId",
                keyValue: new Guid("0ae12bbd-58ef-4c2e-87a6-2c2cb3f9592d"),
                column: "ConcurrencyStamp",
                value: new Guid("93fc1cfb-4720-4833-a1b0-99f0f9216f7c"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "RefreshTokenId",
                table: "RefreshToken",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "UserId",
                keyValue: new Guid("0ae12bbd-58ef-4c2e-87a6-2c2cb3f9592d"),
                column: "ConcurrencyStamp",
                value: new Guid("4583205c-1ac5-4b75-ab2d-56c13efc5f86"));
        }
    }
}
