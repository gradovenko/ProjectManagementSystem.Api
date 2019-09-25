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
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("ProjectManagementSystem.DatabaseMigrations.Entities.IssuePriority", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnName("Id");

                    b.Property<bool>("IsActive")
                        .HasColumnName("IsActive");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("Name");

                    b.HasKey("Id");

                    b.ToTable("IssuePriority");
                });

            modelBuilder.Entity("ProjectManagementSystem.DatabaseMigrations.Entities.RefreshToken", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnName("Id");

                    b.Property<DateTime>("ExpireDate")
                        .HasColumnName("ExpireDate");

                    b.Property<Guid>("UserId")
                        .HasColumnName("UserId");

                    b.HasKey("Id");

                    b.ToTable("RefreshToken");
                });

            modelBuilder.Entity("ProjectManagementSystem.DatabaseMigrations.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnName("Id");

                    b.Property<Guid>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnName("ConcurrencyStamp");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnName("CreateDate");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnName("Email")
                        .HasMaxLength(256);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnName("FirstName");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnName("LastName");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("Name")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnName("PasswordHash")
                        .HasMaxLength(1024);

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnName("Role");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnName("Status");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnName("UpdateDate");

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
                            ConcurrencyStamp = new Guid("a5fc1266-2030-46d4-82c2-694e0d279713"),
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
#pragma warning restore 612, 618
        }
    }
}
