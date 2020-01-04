using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ProjectManagementSystem.DatabaseMigrations.Migrations
{
    public partial class AddRenameAndRefactoringTableFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Issue_User_AssigneeId",
                table: "Issue");

            migrationBuilder.DropForeignKey(
                name: "FK_Issue_User_AuthorId",
                table: "Issue");

            migrationBuilder.DropForeignKey(
                name: "FK_Issue_IssuePriority_PriorityId",
                table: "Issue");

            migrationBuilder.DropForeignKey(
                name: "FK_Issue_Project_ProjectId",
                table: "Issue");

            migrationBuilder.DropForeignKey(
                name: "FK_Issue_IssueStatus_StatusId",
                table: "Issue");

            migrationBuilder.DropForeignKey(
                name: "FK_Issue_Tracker_TrackerId",
                table: "Issue");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectTracker_Project_ProjectId",
                table: "ProjectTracker");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectTracker_Tracker_TrackerId",
                table: "ProjectTracker");

            migrationBuilder.DropForeignKey(
                name: "FK_TimeEntry_TimeEntryActivity_ActivityId",
                table: "TimeEntry");

            migrationBuilder.DropForeignKey(
                name: "FK_TimeEntry_Issue_IssueId",
                table: "TimeEntry");

            migrationBuilder.DropForeignKey(
                name: "FK_TimeEntry_Project_ProjectId",
                table: "TimeEntry");

            migrationBuilder.DropForeignKey(
                name: "FK_TimeEntry_User_UserId",
                table: "TimeEntry");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tracker",
                table: "Tracker");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TimeEntryActivity",
                table: "TimeEntryActivity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TimeEntry",
                table: "TimeEntry");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RefreshToken",
                table: "RefreshToken");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Project",
                table: "Project");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IssueStatus",
                table: "IssueStatus");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IssuePriority",
                table: "IssuePriority");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Issue",
                table: "Issue");

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("0ae12bbd-58ef-4c2e-87a6-2c2cb3f9592d"));

            migrationBuilder.DropColumn(
                name: "Id",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Tracker");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "TimeEntryActivity");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "TimeEntry");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "RefreshToken");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "IssueStatus");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "IssuePriority");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Issue");

            migrationBuilder.DropColumn(
                name: "Index",
                table: "Issue");

            migrationBuilder.RenameIndex(
                name: "UserNameIndex",
                table: "User",
                newName: "IX_User_Name");

            migrationBuilder.RenameIndex(
                name: "EmailIndex",
                table: "User",
                newName: "IX_User_Email");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "User",
                maxLength: 64,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "User",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "TrackerId",
                table: "Tracker",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "TimeEntryActivityId",
                table: "TimeEntryActivity",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "TimeEntryId",
                table: "TimeEntry",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "RefreshTokenId",
                table: "RefreshToken",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ProjectId",
                table: "Project",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "IssueStatusId",
                table: "IssueStatus",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "IssuePriorityId",
                table: "IssuePriority",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "IssueId",
                table: "Issue",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<long>(
                name: "Number",
                table: "Issue",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tracker",
                table: "Tracker",
                column: "TrackerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TimeEntryActivity",
                table: "TimeEntryActivity",
                column: "TimeEntryActivityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TimeEntry",
                table: "TimeEntry",
                column: "TimeEntryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RefreshToken",
                table: "RefreshToken",
                column: "RefreshTokenId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Project",
                table: "Project",
                column: "ProjectId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IssueStatus",
                table: "IssueStatus",
                column: "IssueStatusId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IssuePriority",
                table: "IssuePriority",
                column: "IssuePriorityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Issue",
                table: "Issue",
                column: "IssueId");

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "UserId", "ConcurrencyStamp", "CreateDate", "Email", "FirstName", "LastName", "Name", "PasswordHash", "Role", "Status", "UpdateDate" },
                values: new object[] { new Guid("0ae12bbd-58ef-4c2e-87a6-2c2cb3f9592d"), new Guid("4583205c-1ac5-4b75-ab2d-56c13efc5f86"), new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "admin@projectms.local", "Admin", "Admin", "Admin", "AQAAAAEAACcQAAAAEDcxbCGbTbY1rUJBVafqc/qaL1rWXro6aoahEwrPF5zHb8DB11apWESUm5UyMRF3mA==", "Admin", "Active", null });

            migrationBuilder.AddForeignKey(
                name: "FK_Issue_User_AssigneeId",
                table: "Issue",
                column: "AssigneeId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Issue_User_AuthorId",
                table: "Issue",
                column: "AuthorId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Issue_IssuePriority_PriorityId",
                table: "Issue",
                column: "PriorityId",
                principalTable: "IssuePriority",
                principalColumn: "IssuePriorityId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Issue_Project_ProjectId",
                table: "Issue",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Issue_IssueStatus_StatusId",
                table: "Issue",
                column: "StatusId",
                principalTable: "IssueStatus",
                principalColumn: "IssueStatusId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Issue_Tracker_TrackerId",
                table: "Issue",
                column: "TrackerId",
                principalTable: "Tracker",
                principalColumn: "TrackerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectTracker_Project_ProjectId",
                table: "ProjectTracker",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectTracker_Tracker_TrackerId",
                table: "ProjectTracker",
                column: "TrackerId",
                principalTable: "Tracker",
                principalColumn: "TrackerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TimeEntry_TimeEntryActivity_ActivityId",
                table: "TimeEntry",
                column: "ActivityId",
                principalTable: "TimeEntryActivity",
                principalColumn: "TimeEntryActivityId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TimeEntry_Issue_IssueId",
                table: "TimeEntry",
                column: "IssueId",
                principalTable: "Issue",
                principalColumn: "IssueId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TimeEntry_Project_ProjectId",
                table: "TimeEntry",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TimeEntry_User_UserId",
                table: "TimeEntry",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Issue_User_AssigneeId",
                table: "Issue");

            migrationBuilder.DropForeignKey(
                name: "FK_Issue_User_AuthorId",
                table: "Issue");

            migrationBuilder.DropForeignKey(
                name: "FK_Issue_IssuePriority_PriorityId",
                table: "Issue");

            migrationBuilder.DropForeignKey(
                name: "FK_Issue_Project_ProjectId",
                table: "Issue");

            migrationBuilder.DropForeignKey(
                name: "FK_Issue_IssueStatus_StatusId",
                table: "Issue");

            migrationBuilder.DropForeignKey(
                name: "FK_Issue_Tracker_TrackerId",
                table: "Issue");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectTracker_Project_ProjectId",
                table: "ProjectTracker");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectTracker_Tracker_TrackerId",
                table: "ProjectTracker");

            migrationBuilder.DropForeignKey(
                name: "FK_TimeEntry_TimeEntryActivity_ActivityId",
                table: "TimeEntry");

            migrationBuilder.DropForeignKey(
                name: "FK_TimeEntry_Issue_IssueId",
                table: "TimeEntry");

            migrationBuilder.DropForeignKey(
                name: "FK_TimeEntry_Project_ProjectId",
                table: "TimeEntry");

            migrationBuilder.DropForeignKey(
                name: "FK_TimeEntry_User_UserId",
                table: "TimeEntry");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tracker",
                table: "Tracker");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TimeEntryActivity",
                table: "TimeEntryActivity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TimeEntry",
                table: "TimeEntry");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RefreshToken",
                table: "RefreshToken");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Project",
                table: "Project");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IssueStatus",
                table: "IssueStatus");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IssuePriority",
                table: "IssuePriority");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Issue",
                table: "Issue");

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "UserId",
                keyValue: new Guid("0ae12bbd-58ef-4c2e-87a6-2c2cb3f9592d"));

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "TrackerId",
                table: "Tracker");

            migrationBuilder.DropColumn(
                name: "TimeEntryActivityId",
                table: "TimeEntryActivity");

            migrationBuilder.DropColumn(
                name: "TimeEntryId",
                table: "TimeEntry");

            migrationBuilder.DropColumn(
                name: "RefreshTokenId",
                table: "RefreshToken");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "IssueStatusId",
                table: "IssueStatus");

            migrationBuilder.DropColumn(
                name: "IssuePriorityId",
                table: "IssuePriority");

            migrationBuilder.DropColumn(
                name: "IssueId",
                table: "Issue");

            migrationBuilder.DropColumn(
                name: "Number",
                table: "Issue");

            migrationBuilder.RenameIndex(
                name: "IX_User_Name",
                table: "User",
                newName: "UserNameIndex");

            migrationBuilder.RenameIndex(
                name: "IX_User_Email",
                table: "User",
                newName: "EmailIndex");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "User",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 64);

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "User",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Tracker",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "TimeEntryActivity",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "TimeEntry",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "RefreshToken",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Project",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "IssueStatus",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "IssuePriority",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Issue",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<long>(
                name: "Index",
                table: "Issue",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tracker",
                table: "Tracker",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TimeEntryActivity",
                table: "TimeEntryActivity",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TimeEntry",
                table: "TimeEntry",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RefreshToken",
                table: "RefreshToken",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Project",
                table: "Project",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IssueStatus",
                table: "IssueStatus",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IssuePriority",
                table: "IssuePriority",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Issue",
                table: "Issue",
                column: "Id");

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "ConcurrencyStamp", "CreateDate", "Email", "FirstName", "LastName", "Name", "PasswordHash", "Role", "Status", "UpdateDate" },
                values: new object[] { new Guid("0ae12bbd-58ef-4c2e-87a6-2c2cb3f9592d"), new Guid("8d150a60-5a55-4e14-a43a-a1bd43d21ab0"), new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "admin@projectms.local", "Admin", "Admin", "Admin", "AQAAAAEAACcQAAAAEDcxbCGbTbY1rUJBVafqc/qaL1rWXro6aoahEwrPF5zHb8DB11apWESUm5UyMRF3mA==", "Admin", "Active", null });

            migrationBuilder.AddForeignKey(
                name: "FK_Issue_User_AssigneeId",
                table: "Issue",
                column: "AssigneeId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Issue_User_AuthorId",
                table: "Issue",
                column: "AuthorId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Issue_IssuePriority_PriorityId",
                table: "Issue",
                column: "PriorityId",
                principalTable: "IssuePriority",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Issue_Project_ProjectId",
                table: "Issue",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Issue_IssueStatus_StatusId",
                table: "Issue",
                column: "StatusId",
                principalTable: "IssueStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Issue_Tracker_TrackerId",
                table: "Issue",
                column: "TrackerId",
                principalTable: "Tracker",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectTracker_Project_ProjectId",
                table: "ProjectTracker",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectTracker_Tracker_TrackerId",
                table: "ProjectTracker",
                column: "TrackerId",
                principalTable: "Tracker",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TimeEntry_TimeEntryActivity_ActivityId",
                table: "TimeEntry",
                column: "ActivityId",
                principalTable: "TimeEntryActivity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TimeEntry_Issue_IssueId",
                table: "TimeEntry",
                column: "IssueId",
                principalTable: "Issue",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TimeEntry_Project_ProjectId",
                table: "TimeEntry",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TimeEntry_User_UserId",
                table: "TimeEntry",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
