using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ProjectManagementSystem.DatabaseMigrations.Migrations
{
    public partial class AddIssueTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Issue",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Index = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: true),
                    ProjectId = table.Column<Guid>(nullable: false),
                    TrackerId = table.Column<Guid>(nullable: false),
                    StatusId = table.Column<Guid>(nullable: false),
                    PriorityId = table.Column<Guid>(nullable: false),
                    AuthorId = table.Column<Guid>(nullable: false),
                    PerformerId = table.Column<Guid>(nullable: false),
                    ConcurrencyStamp = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Issue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Issue_User_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Issue_User_PerformerId",
                        column: x => x.PerformerId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Issue_IssuePriority_PriorityId",
                        column: x => x.PriorityId,
                        principalTable: "IssuePriority",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Issue_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Issue_IssueStatus_StatusId",
                        column: x => x.StatusId,
                        principalTable: "IssueStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Issue_Tracker_TrackerId",
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
                value: new Guid("1e7f3345-0841-441f-bcdb-1ab41d4453c7"));

            migrationBuilder.CreateIndex(
                name: "IX_Issue_AuthorId",
                table: "Issue",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Issue_PerformerId",
                table: "Issue",
                column: "PerformerId");

            migrationBuilder.CreateIndex(
                name: "IX_Issue_PriorityId",
                table: "Issue",
                column: "PriorityId");

            migrationBuilder.CreateIndex(
                name: "IX_Issue_ProjectId",
                table: "Issue",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Issue_StatusId",
                table: "Issue",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Issue_TrackerId",
                table: "Issue",
                column: "TrackerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Issue");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("0ae12bbd-58ef-4c2e-87a6-2c2cb3f9592d"),
                column: "ConcurrencyStamp",
                value: new Guid("3b11ed20-a513-4012-ac74-bbb934a47fdd"));
        }
    }
}
