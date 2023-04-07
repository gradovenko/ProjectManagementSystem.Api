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
    [Migration("20230402200944_Initialization")]
    partial class Initialization
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("IssueLabel", b =>
                {
                    b.Property<Guid>("IssuesIssueId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("LabelsLabelId")
                        .HasColumnType("uuid");

                    b.HasKey("IssuesIssueId", "LabelsLabelId");

                    b.HasIndex("LabelsLabelId");

                    b.ToTable("IssueLabel");
                });

            modelBuilder.Entity("IssueReaction", b =>
                {
                    b.Property<Guid>("IssuesIssueId")
                        .HasColumnType("uuid");

                    b.Property<string>("ReactionsReactionId")
                        .HasColumnType("text");

                    b.HasKey("IssuesIssueId", "ReactionsReactionId");

                    b.HasIndex("ReactionsReactionId");

                    b.ToTable("IssueReaction");
                });

            modelBuilder.Entity("IssueUser", b =>
                {
                    b.Property<Guid>("AssigneesUserId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("IssuesIssueId")
                        .HasColumnType("uuid");

                    b.HasKey("AssigneesUserId", "IssuesIssueId");

                    b.HasIndex("IssuesIssueId");

                    b.ToTable("IssueUser");
                });

            modelBuilder.Entity("ProjectManagementSystem.DatabaseMigrations.Entities.Issue", b =>
                {
                    b.Property<Guid>("IssueId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ClosedByUserId")
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

                    b.HasKey("IssueId");

                    b.HasIndex("AuthorId");

                    b.HasIndex("ClosedByUserId");

                    b.HasIndex("ProjectId");

                    b.ToTable("Issue", (string)null);
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
                    b.Property<string>("ReactionId")
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

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("TimeEntryId");

                    b.HasIndex("IssueId");

                    b.HasIndex("UserId");

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
                        .HasColumnType("text");

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
                            ConcurrencyToken = new Guid("254e4385-2a78-410d-88c5-7d41dfaf27d3"),
                            CreateDate = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
                            Email = "admin@projectms.local",
                            Name = "Admin",
                            PasswordHash = "AQAAAAEAACcQAAAAEDcxbCGbTbY1rUJBVafqc/qaL1rWXro6aoahEwrPF5zHb8DB11apWESUm5UyMRF3mA==",
                            Role = "Admin",
                            State = "Active",
                            UpdateDate = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)
                        });
                });

            modelBuilder.Entity("IssueLabel", b =>
                {
                    b.HasOne("ProjectManagementSystem.DatabaseMigrations.Entities.Issue", null)
                        .WithMany()
                        .HasForeignKey("IssuesIssueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjectManagementSystem.DatabaseMigrations.Entities.Label", null)
                        .WithMany()
                        .HasForeignKey("LabelsLabelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("IssueReaction", b =>
                {
                    b.HasOne("ProjectManagementSystem.DatabaseMigrations.Entities.Issue", null)
                        .WithMany()
                        .HasForeignKey("IssuesIssueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjectManagementSystem.DatabaseMigrations.Entities.Reaction", null)
                        .WithMany()
                        .HasForeignKey("ReactionsReactionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("IssueUser", b =>
                {
                    b.HasOne("ProjectManagementSystem.DatabaseMigrations.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("AssigneesUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjectManagementSystem.DatabaseMigrations.Entities.Issue", null)
                        .WithMany()
                        .HasForeignKey("IssuesIssueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProjectManagementSystem.DatabaseMigrations.Entities.Issue", b =>
                {
                    b.HasOne("ProjectManagementSystem.DatabaseMigrations.Entities.User", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjectManagementSystem.DatabaseMigrations.Entities.User", "ClosedByUser")
                        .WithMany()
                        .HasForeignKey("ClosedByUserId");

                    b.HasOne("ProjectManagementSystem.DatabaseMigrations.Entities.Project", "Project")
                        .WithMany("Issues")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("ClosedByUser");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("ProjectManagementSystem.DatabaseMigrations.Entities.TimeEntry", b =>
                {
                    b.HasOne("ProjectManagementSystem.DatabaseMigrations.Entities.Issue", "Issue")
                        .WithMany("TimeEntries")
                        .HasForeignKey("IssueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjectManagementSystem.DatabaseMigrations.Entities.User", null)
                        .WithMany("TimeEntries")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Issue");
                });

            modelBuilder.Entity("ProjectManagementSystem.DatabaseMigrations.Entities.Issue", b =>
                {
                    b.Navigation("TimeEntries");
                });

            modelBuilder.Entity("ProjectManagementSystem.DatabaseMigrations.Entities.Project", b =>
                {
                    b.Navigation("Issues");
                });

            modelBuilder.Entity("ProjectManagementSystem.DatabaseMigrations.Entities.User", b =>
                {
                    b.Navigation("TimeEntries");
                });
#pragma warning restore 612, 618
        }
    }
}
