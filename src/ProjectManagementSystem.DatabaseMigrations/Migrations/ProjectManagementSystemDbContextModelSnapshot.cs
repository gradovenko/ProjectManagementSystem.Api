﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ProjectManagementSystem.DatabaseMigrations;

namespace ProjectManagementSystem.DatabaseMigrations.Migrations
{
    [DbContext(typeof(ProjectManagementSystemDbContext))]
    partial class ProjectManagementSystemDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("ProjectManagementSystem.DatabaseMigrations.Entities.Issue", b =>
                {
                    b.Property<Guid>("IssueId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("AssigneeId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("DueDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<long>("Number")
                        .HasColumnType("bigint");

                    b.Property<Guid>("PriorityId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ProjectId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("StatusId")
                        .HasColumnType("uuid");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("TrackerId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("IssueId");

                    b.HasIndex("AssigneeId");

                    b.HasIndex("AuthorId");

                    b.HasIndex("PriorityId");

                    b.HasIndex("ProjectId");

                    b.HasIndex("StatusId");

                    b.HasIndex("TrackerId");

                    b.ToTable("Issue");
                });

            modelBuilder.Entity("ProjectManagementSystem.DatabaseMigrations.Entities.IssuePriority", b =>
                {
                    b.Property<Guid>("IssuePriorityId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("IssuePriorityId");

                    b.ToTable("IssuePriority");
                });

            modelBuilder.Entity("ProjectManagementSystem.DatabaseMigrations.Entities.IssueStatus", b =>
                {
                    b.Property<Guid>("IssueStatusId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("IssueStatusId");

                    b.ToTable("IssueStatus");
                });

            modelBuilder.Entity("ProjectManagementSystem.DatabaseMigrations.Entities.Project", b =>
                {
                    b.Property<Guid>("ProjectId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsPrivate")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("ProjectId");

                    b.ToTable("Project");
                });

            modelBuilder.Entity("ProjectManagementSystem.DatabaseMigrations.Entities.ProjectTracker", b =>
                {
                    b.Property<Guid>("ProjectId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("TrackerId")
                        .HasColumnType("uuid");

                    b.HasKey("ProjectId", "TrackerId");

                    b.HasIndex("TrackerId");

                    b.ToTable("ProjectTracker");
                });

            modelBuilder.Entity("ProjectManagementSystem.DatabaseMigrations.Entities.RefreshToken", b =>
                {
                    b.Property<string>("RefreshTokenId")
                        .HasColumnType("text");

                    b.Property<DateTime>("ExpireDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("RefreshTokenId");

                    b.ToTable("RefreshToken");
                });

            modelBuilder.Entity("ProjectManagementSystem.DatabaseMigrations.Entities.TimeEntry", b =>
                {
                    b.Property<Guid>("TimeEntryId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ActivityId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<decimal>("Hours")
                        .HasColumnType("numeric");

                    b.Property<Guid>("IssueId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ProjectId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("TimeEntryId");

                    b.HasIndex("ActivityId");

                    b.HasIndex("IssueId");

                    b.HasIndex("ProjectId");

                    b.HasIndex("UserId");

                    b.ToTable("TimeEntry");
                });

            modelBuilder.Entity("ProjectManagementSystem.DatabaseMigrations.Entities.TimeEntryActivity", b =>
                {
                    b.Property<Guid>("TimeEntryActivityId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("uuid");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("TimeEntryActivityId");

                    b.ToTable("TimeEntryActivity");
                });

            modelBuilder.Entity("ProjectManagementSystem.DatabaseMigrations.Entities.Tracker", b =>
                {
                    b.Property<Guid>("TrackerId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("TrackerId");

                    b.ToTable("Tracker");
                });

            modelBuilder.Entity("ProjectManagementSystem.DatabaseMigrations.Entities.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("character varying(256)")
                        .HasMaxLength(256);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("character varying(256)")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("character varying(1024)")
                        .HasMaxLength(1024);

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("character varying(64)")
                        .HasMaxLength(64);

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("UserId");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("User");

                    b.HasData(
                        new
                        {
                            UserId = new Guid("0ae12bbd-58ef-4c2e-87a6-2c2cb3f9592d"),
                            ConcurrencyStamp = new Guid("93fc1cfb-4720-4833-a1b0-99f0f9216f7c"),
                            CreateDate = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
                            Email = "admin@projectms.local",
                            FirstName = "Admin",
                            LastName = "Admin",
                            Name = "Admin",
                            PasswordHash = "AQAAAAEAACcQAAAAEDcxbCGbTbY1rUJBVafqc/qaL1rWXro6aoahEwrPF5zHb8DB11apWESUm5UyMRF3mA==",
                            Role = "Admin",
                            Status = "Active"
                        });
                });

            modelBuilder.Entity("ProjectManagementSystem.DatabaseMigrations.Entities.Issue", b =>
                {
                    b.HasOne("ProjectManagementSystem.DatabaseMigrations.Entities.User", "Assignee")
                        .WithMany()
                        .HasForeignKey("AssigneeId");

                    b.HasOne("ProjectManagementSystem.DatabaseMigrations.Entities.User", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjectManagementSystem.DatabaseMigrations.Entities.IssuePriority", "Priority")
                        .WithMany()
                        .HasForeignKey("PriorityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjectManagementSystem.DatabaseMigrations.Entities.Project", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjectManagementSystem.DatabaseMigrations.Entities.IssueStatus", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjectManagementSystem.DatabaseMigrations.Entities.Tracker", "Tracker")
                        .WithMany()
                        .HasForeignKey("TrackerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProjectManagementSystem.DatabaseMigrations.Entities.ProjectTracker", b =>
                {
                    b.HasOne("ProjectManagementSystem.DatabaseMigrations.Entities.Project", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjectManagementSystem.DatabaseMigrations.Entities.Tracker", "Tracker")
                        .WithMany()
                        .HasForeignKey("TrackerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProjectManagementSystem.DatabaseMigrations.Entities.TimeEntry", b =>
                {
                    b.HasOne("ProjectManagementSystem.DatabaseMigrations.Entities.TimeEntryActivity", "Activity")
                        .WithMany()
                        .HasForeignKey("ActivityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjectManagementSystem.DatabaseMigrations.Entities.Issue", "Issue")
                        .WithMany()
                        .HasForeignKey("IssueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjectManagementSystem.DatabaseMigrations.Entities.Project", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjectManagementSystem.DatabaseMigrations.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
