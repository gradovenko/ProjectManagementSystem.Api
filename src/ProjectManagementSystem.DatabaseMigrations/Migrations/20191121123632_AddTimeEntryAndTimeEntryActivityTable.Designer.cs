﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ProjectManagementSystem.DatabaseMigrations;

namespace ProjectManagementSystem.DatabaseMigrations.Migrations
{
    [DbContext(typeof(ProjectManagementSystemDbContext))]
    [Migration("20191121123632_AddTimeEntryAndTimeEntryActivityTable")]
    partial class AddTimeEntryAndTimeEntryActivityTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("ProjectManagementSystem.DatabaseMigrations.Entities.Issue", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnName("Id")
                        .HasColumnType("uuid");

                    b.Property<Guid>("AuthorId")
                        .HasColumnName("AuthorId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnName("ConcurrencyStamp")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnName("CreateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnName("Description")
                        .HasColumnType("text");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnName("EndDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<long>("Index")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Index")
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<Guid?>("PerformerId")
                        .HasColumnName("PerformerId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("PriorityId")
                        .HasColumnName("PriorityId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ProjectId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnName("StartDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("StatusId")
                        .HasColumnName("StatusId")
                        .HasColumnType("uuid");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnName("Title")
                        .HasColumnType("text");

                    b.Property<Guid>("TrackerId")
                        .HasColumnName("TrackerId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnName("UpdateDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("PerformerId");

                    b.HasIndex("PriorityId");

                    b.HasIndex("ProjectId");

                    b.HasIndex("StatusId");

                    b.HasIndex("TrackerId");

                    b.ToTable("Issue");
                });

            modelBuilder.Entity("ProjectManagementSystem.DatabaseMigrations.Entities.IssuePriority", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnName("Id")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsActive")
                        .HasColumnName("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("IssuePriority");
                });

            modelBuilder.Entity("ProjectManagementSystem.DatabaseMigrations.Entities.IssueStatus", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnName("Id")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsActive")
                        .HasColumnName("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("IssueStatus");
                });

            modelBuilder.Entity("ProjectManagementSystem.DatabaseMigrations.Entities.Project", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnName("Id")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnName("ConcurrencyStamp")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnName("CreateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnName("Description")
                        .HasColumnType("text");

                    b.Property<bool>("IsPrivate")
                        .HasColumnName("IsPrivate")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("Name")
                        .HasColumnType("text");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnName("Status")
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

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
                    b.Property<Guid>("Id")
                        .HasColumnName("Id")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("ExpireDate")
                        .HasColumnName("ExpireDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("UserId")
                        .HasColumnName("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("RefreshToken");
                });

            modelBuilder.Entity("ProjectManagementSystem.DatabaseMigrations.Entities.TimeEntry", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnName("Id")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ActivityId")
                        .HasColumnName("ActivityId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnName("ConcurrencyStamp")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnName("CreateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnName("Description")
                        .HasColumnType("text");

                    b.Property<DateTime>("DueDate")
                        .HasColumnName("DueDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<decimal>("Hours")
                        .HasColumnName("Hours")
                        .HasColumnType("numeric");

                    b.Property<Guid>("IssueId")
                        .HasColumnName("IssueId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ProjectId")
                        .HasColumnName("ProjectId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnName("UpdateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("UserId")
                        .HasColumnName("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ActivityId");

                    b.HasIndex("IssueId");

                    b.HasIndex("ProjectId");

                    b.HasIndex("UserId");

                    b.ToTable("TimeEntry");
                });

            modelBuilder.Entity("ProjectManagementSystem.DatabaseMigrations.Entities.TimeEntryActivity", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnName("Id")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnName("ConcurrencyStamp")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsActive")
                        .HasColumnName("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("TimeEntryActivity");
                });

            modelBuilder.Entity("ProjectManagementSystem.DatabaseMigrations.Entities.Tracker", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnName("Id")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnName("ConcurrencyStamp")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Tracker");
                });

            modelBuilder.Entity("ProjectManagementSystem.DatabaseMigrations.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnName("Id")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnName("ConcurrencyStamp")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnName("CreateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnName("Email")
                        .HasColumnType("character varying(256)")
                        .HasMaxLength(256);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnName("FirstName")
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnName("LastName")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("Name")
                        .HasColumnType("character varying(256)")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnName("PasswordHash")
                        .HasColumnType("character varying(1024)")
                        .HasMaxLength(1024);

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnName("Role")
                        .HasColumnType("text");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnName("Status")
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnName("UpdateDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasName("EmailIndex");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("User");

                    b.HasData(
                        new
                        {
                            Id = new Guid("0ae12bbd-58ef-4c2e-87a6-2c2cb3f9592d"),
                            ConcurrencyStamp = new Guid("24a3ac21-18e1-468a-931c-538acb6391b4"),
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
                    b.HasOne("ProjectManagementSystem.DatabaseMigrations.Entities.User", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjectManagementSystem.DatabaseMigrations.Entities.User", "Performer")
                        .WithMany()
                        .HasForeignKey("PerformerId");

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
