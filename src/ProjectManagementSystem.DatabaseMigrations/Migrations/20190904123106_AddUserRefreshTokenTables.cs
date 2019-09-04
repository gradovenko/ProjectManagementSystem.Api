using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectManagementSystem.DatabaseMigrations.Migrations
{
    public partial class AddUserRefreshTokenTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RefreshToken",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ExpireDate = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshToken", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: false),
                    Email = table.Column<string>(maxLength: 256, nullable: false),
                    PasswordHash = table.Column<string>(maxLength: 1024, nullable: true),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    Role = table.Column<string>(nullable: false),
                    Status = table.Column<string>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: true),
                    ConcurrencyStamp = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "ConcurrencyStamp", "CreateDate", "Email", "FirstName", "LastName", "PasswordHash", "Role", "Status", "UpdateDate", "UserName" },
                values: new object[] { new Guid("0ae12bbd-58ef-4c2e-87a6-2c2cb3f9592d"), new Guid("8ffe879c-8f6c-42e8-853e-65a393a91d9b"), new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "admin@projectms.local", "Admin", "Admin", "AQAAAAEAACcQAAAAEDcxbCGbTbY1rUJBVafqc/qaL1rWXro6aoahEwrPF5zHb8DB11apWESUm5UyMRF3mA==", "Admin", "Active", null, "Admin" });

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "User",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "User",
                column: "UserName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RefreshToken");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
