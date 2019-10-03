using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectManagementSystem.DatabaseMigrations.Migrations
{
    public partial class AddProjectTrackerTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProjectTracker",
                columns: table => new
                {
                    ProjectId = table.Column<Guid>(nullable: false),
                    TrackerId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectTracker", x => new { x.ProjectId, x.TrackerId });
                    table.ForeignKey(
                        name: "FK_ProjectTracker_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectTracker_Tracker_TrackerId",
                        column: x => x.TrackerId,
                        principalTable: "Tracker",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("0ae12bbd-58ef-4c2e-87a6-2c2cb3f9592d"),
                column: "ConcurrencyStamp",
                value: new Guid("41951d66-5d68-4295-8912-c7c6152252de"));

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTracker_TrackerId",
                table: "ProjectTracker",
                column: "TrackerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectTracker");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("0ae12bbd-58ef-4c2e-87a6-2c2cb3f9592d"),
                column: "ConcurrencyStamp",
                value: new Guid("d7370165-879c-48ce-88d8-463ff69968ec"));
        }
    }
}
