﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ProjectManagementSystem.DatabaseMigrations;

#nullable disable

namespace ProjectManagementSystem.DatabaseMigrations.Migrations
{
    [DbContext(typeof(MigrationDbContext))]
    [Migration("20230420081402_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ProjectManagementSystem.DatabaseMigrations.Entities.Comment", b =>
                {
                    b.Property<Guid>("CommentId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ConcurrencyToken")
                        .IsConcurrencyToken()
                        .HasColumnType("uuid");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<Guid>("IssueId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ParentCommentId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("CommentId");

                    b.HasIndex("AuthorId");

                    b.HasIndex("IssueId");

                    b.HasIndex("ParentCommentId");

                    b.ToTable("Comment", (string)null);
                });

            modelBuilder.Entity("ProjectManagementSystem.DatabaseMigrations.Entities.CommentUserReaction", b =>
                {
                    b.Property<Guid>("CommentId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ReactionId")
                        .HasColumnType("uuid");

                    b.HasKey("CommentId", "UserId", "ReactionId");

                    b.HasIndex("UserId");

                    b.ToTable("CommentUserReaction", (string)null);
                });

            modelBuilder.Entity("ProjectManagementSystem.DatabaseMigrations.Entities.Issue", b =>
                {
                    b.Property<Guid>("IssueId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ConcurrencyToken")
                        .IsConcurrencyToken()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<DateTime?>("DueDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("ProjectId")
                        .HasColumnType("uuid");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("UserIdWhoClosed")
                        .HasColumnType("uuid");

                    b.HasKey("IssueId");

                    b.HasIndex("AuthorId");

                    b.HasIndex("ProjectId");

                    b.HasIndex("UserIdWhoClosed");

                    b.ToTable("Issue", (string)null);
                });

            modelBuilder.Entity("ProjectManagementSystem.DatabaseMigrations.Entities.IssueAssignee", b =>
                {
                    b.Property<Guid>("IssueId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("AssigneeId")
                        .HasColumnType("uuid");

                    b.HasKey("IssueId", "AssigneeId");

                    b.HasIndex("AssigneeId");

                    b.ToTable("IssueAssignee", (string)null);
                });

            modelBuilder.Entity("ProjectManagementSystem.DatabaseMigrations.Entities.IssueLabel", b =>
                {
                    b.Property<Guid>("IssueId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("LabelId")
                        .HasColumnType("uuid");

                    b.HasKey("IssueId", "LabelId");

                    b.HasIndex("LabelId");

                    b.ToTable("IssueLabel", (string)null);
                });

            modelBuilder.Entity("ProjectManagementSystem.DatabaseMigrations.Entities.IssueUserReaction", b =>
                {
                    b.Property<Guid>("IssueId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ReactionId")
                        .HasColumnType("uuid");

                    b.HasKey("IssueId", "UserId", "ReactionId");

                    b.HasIndex("ReactionId");

                    b.HasIndex("UserId");

                    b.ToTable("IssueUserReaction", (string)null);
                });

            modelBuilder.Entity("ProjectManagementSystem.DatabaseMigrations.Entities.Label", b =>
                {
                    b.Property<Guid>("LabelId")
                        .HasColumnType("uuid");

                    b.Property<string>("BackgroundColor")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("ConcurrencyToken")
                        .IsConcurrencyToken()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("LabelId");

                    b.ToTable("Label", (string)null);
                });

            modelBuilder.Entity("ProjectManagementSystem.DatabaseMigrations.Entities.Project", b =>
                {
                    b.Property<Guid>("ProjectId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ConcurrencyToken")
                        .IsConcurrencyToken()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Visibility")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.HasKey("ProjectId");

                    b.ToTable("Project", (string)null);
                });

            modelBuilder.Entity("ProjectManagementSystem.DatabaseMigrations.Entities.Reaction", b =>
                {
                    b.Property<Guid>("ReactionId")
                        .HasColumnType("uuid");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("ConcurrencyToken")
                        .IsConcurrencyToken()
                        .HasColumnType("uuid");

                    b.Property<string>("Emoji")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ReactionId");

                    b.ToTable("Reaction", (string)null);
                });

            modelBuilder.Entity("ProjectManagementSystem.DatabaseMigrations.Entities.RefreshToken", b =>
                {
                    b.Property<string>("RefreshTokenId")
                        .HasColumnType("text");

                    b.Property<DateTime>("ExpireDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("UserId")
                        .HasMaxLength(32)
                        .HasColumnType("uuid");

                    b.HasKey("RefreshTokenId");

                    b.ToTable("RefreshToken", (string)null);
                });

            modelBuilder.Entity("ProjectManagementSystem.DatabaseMigrations.Entities.TimeEntry", b =>
                {
                    b.Property<Guid>("TimeEntryId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ConcurrencyToken")
                        .IsConcurrencyToken()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<DateTime?>("DueDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<decimal>("Hours")
                        .HasColumnType("numeric");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<Guid>("IssueId")
                        .HasColumnType("uuid");

                    b.HasKey("TimeEntryId");

                    b.HasIndex("AuthorId");

                    b.HasIndex("IssueId");

                    b.ToTable("TimeEntry", (string)null);
                });

            modelBuilder.Entity("ProjectManagementSystem.DatabaseMigrations.Entities.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ConcurrencyToken")
                        .IsConcurrencyToken()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(1024)
                        .HasColumnType("character varying(1024)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("UserId");

                    b.ToTable("User", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = new Guid("0ae12bbd-58ef-4c2e-87a6-2c2cb3f9592d"),
                            ConcurrencyToken = new Guid("0ae12bbd-58ef-4c2e-87a6-2c2cb3f9592d"),
                            CreateDate = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
                            Email = "admin@projectms.local",
                            IsDeleted = false,
                            Name = "Admin",
                            PasswordHash = "AQAAAAEAACcQAAAAEDcxbCGbTbY1rUJBVafqc/qaL1rWXro6aoahEwrPF5zHb8DB11apWESUm5UyMRF3mA==",
                            Role = "Admin",
                            State = "Active",
                            UpdateDate = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)
                        });
                });

            modelBuilder.Entity("ProjectManagementSystem.DatabaseMigrations.Entities.Comment", b =>
                {
                    b.HasOne("ProjectManagementSystem.DatabaseMigrations.Entities.User", "Author")
                        .WithMany("Comments")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjectManagementSystem.DatabaseMigrations.Entities.Issue", "Issue")
                        .WithMany("Comments")
                        .HasForeignKey("IssueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjectManagementSystem.DatabaseMigrations.Entities.Comment", "ParentComment")
                        .WithMany("ChildComments")
                        .HasForeignKey("ParentCommentId");

                    b.Navigation("Author");

                    b.Navigation("Issue");

                    b.Navigation("ParentComment");
                });

            modelBuilder.Entity("ProjectManagementSystem.DatabaseMigrations.Entities.CommentUserReaction", b =>
                {
                    b.HasOne("ProjectManagementSystem.DatabaseMigrations.Entities.Comment", "Comment")
                        .WithMany("CommentUserReactions")
                        .HasForeignKey("CommentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjectManagementSystem.DatabaseMigrations.Entities.Reaction", "Reaction")
                        .WithMany("CommentReactions")
                        .HasForeignKey("CommentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjectManagementSystem.DatabaseMigrations.Entities.User", "User")
                        .WithMany("CommentUserReactions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Comment");

                    b.Navigation("Reaction");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ProjectManagementSystem.DatabaseMigrations.Entities.Issue", b =>
                {
                    b.HasOne("ProjectManagementSystem.DatabaseMigrations.Entities.User", "Author")
                        .WithMany("CreatedIssues")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjectManagementSystem.DatabaseMigrations.Entities.Project", "Project")
                        .WithMany("Issues")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjectManagementSystem.DatabaseMigrations.Entities.User", "UserWhoClosed")
                        .WithMany("ClosedIssues")
                        .HasForeignKey("UserIdWhoClosed");

                    b.Navigation("Author");

                    b.Navigation("Project");

                    b.Navigation("UserWhoClosed");
                });

            modelBuilder.Entity("ProjectManagementSystem.DatabaseMigrations.Entities.IssueAssignee", b =>
                {
                    b.HasOne("ProjectManagementSystem.DatabaseMigrations.Entities.User", "Assignee")
                        .WithMany("IssueAssignees")
                        .HasForeignKey("AssigneeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjectManagementSystem.DatabaseMigrations.Entities.Issue", "Issue")
                        .WithMany("IssueAssignees")
                        .HasForeignKey("IssueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Assignee");

                    b.Navigation("Issue");
                });

            modelBuilder.Entity("ProjectManagementSystem.DatabaseMigrations.Entities.IssueLabel", b =>
                {
                    b.HasOne("ProjectManagementSystem.DatabaseMigrations.Entities.Issue", "Issue")
                        .WithMany("IssueLabels")
                        .HasForeignKey("IssueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjectManagementSystem.DatabaseMigrations.Entities.Label", "Label")
                        .WithMany("IssueLabels")
                        .HasForeignKey("LabelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Issue");

                    b.Navigation("Label");
                });

            modelBuilder.Entity("ProjectManagementSystem.DatabaseMigrations.Entities.IssueUserReaction", b =>
                {
                    b.HasOne("ProjectManagementSystem.DatabaseMigrations.Entities.Issue", "Issue")
                        .WithMany("IssueUserReactions")
                        .HasForeignKey("IssueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjectManagementSystem.DatabaseMigrations.Entities.Reaction", "Reaction")
                        .WithMany("IssueUserReactions")
                        .HasForeignKey("ReactionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjectManagementSystem.DatabaseMigrations.Entities.User", "User")
                        .WithMany("IssueUserReactions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Issue");

                    b.Navigation("Reaction");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ProjectManagementSystem.DatabaseMigrations.Entities.TimeEntry", b =>
                {
                    b.HasOne("ProjectManagementSystem.DatabaseMigrations.Entities.User", "Author")
                        .WithMany("TimeEntries")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjectManagementSystem.DatabaseMigrations.Entities.Issue", "Issue")
                        .WithMany("TimeEntries")
                        .HasForeignKey("IssueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("Issue");
                });

            modelBuilder.Entity("ProjectManagementSystem.DatabaseMigrations.Entities.Comment", b =>
                {
                    b.Navigation("ChildComments");

                    b.Navigation("CommentUserReactions");
                });

            modelBuilder.Entity("ProjectManagementSystem.DatabaseMigrations.Entities.Issue", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("IssueAssignees");

                    b.Navigation("IssueLabels");

                    b.Navigation("IssueUserReactions");

                    b.Navigation("TimeEntries");
                });

            modelBuilder.Entity("ProjectManagementSystem.DatabaseMigrations.Entities.Label", b =>
                {
                    b.Navigation("IssueLabels");
                });

            modelBuilder.Entity("ProjectManagementSystem.DatabaseMigrations.Entities.Project", b =>
                {
                    b.Navigation("Issues");
                });

            modelBuilder.Entity("ProjectManagementSystem.DatabaseMigrations.Entities.Reaction", b =>
                {
                    b.Navigation("CommentReactions");

                    b.Navigation("IssueUserReactions");
                });

            modelBuilder.Entity("ProjectManagementSystem.DatabaseMigrations.Entities.User", b =>
                {
                    b.Navigation("ClosedIssues");

                    b.Navigation("CommentUserReactions");

                    b.Navigation("Comments");

                    b.Navigation("CreatedIssues");

                    b.Navigation("IssueAssignees");

                    b.Navigation("IssueUserReactions");

                    b.Navigation("TimeEntries");
                });
#pragma warning restore 612, 618
        }
    }
}