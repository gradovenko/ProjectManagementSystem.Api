using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.DatabaseMigrations.Entities;

namespace ProjectManagementSystem.DatabaseMigrations;

public sealed class MigrationDbContext : DbContext
{ 
    public MigrationDbContext(DbContextOptions<MigrationDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(builder =>
        {
            builder.ToTable("User");
            builder.HasKey(u => u.UserId);
            builder.Property(u => u.UserId)
                .ValueGeneratedNever();
            builder.Property(u => u.Name)
                .HasMaxLength(256)
                .IsRequired();
            builder.Property(u => u.Email)
                .HasMaxLength(256)
                .IsRequired();
            builder.Property(u => u.PasswordHash)
                .HasMaxLength(1024)
                .IsRequired();
            builder.Property(u => u.Role)
                .IsRequired();
            builder.Property(u => u.CreateDate)
                .IsRequired();
            builder.Property(u => u.UpdateDate)
                .IsRequired();
            builder.Property(u => u.State)
                .HasMaxLength(64)
                .IsRequired();
            builder.Property(u => u.ConcurrencyToken)
                .IsConcurrencyToken();
            
            builder.HasMany(u => u.TimeEntries)
                .WithOne(te => te.User)
                .HasForeignKey(te => te.UserId)
                .HasPrincipalKey(i => i.UserId);

            builder.HasData(new User
            {
                UserId = new Guid("0ae12bbd-58ef-4c2e-87a6-2c2cb3f9592d"),
                Name = "Admin",
                Email = "admin@projectms.local",
                PasswordHash = "AQAAAAEAACcQAAAAEDcxbCGbTbY1rUJBVafqc/qaL1rWXro6aoahEwrPF5zHb8DB11apWESUm5UyMRF3mA==",
                Role = "Admin",
                CreateDate = DateTime.UnixEpoch,
                State = "Active",
                ConcurrencyToken = Guid.NewGuid()
            });
        });
            
        modelBuilder.Entity<RefreshToken>(builder =>
        {
            builder.ToTable("RefreshToken");
            builder.HasKey(rt => rt.RefreshTokenId);
            builder.Property(rt => rt.RefreshTokenId)
                .ValueGeneratedNever();
            builder.Property(rt => rt.ExpireDate)
                .IsRequired();
            builder.Property(rt => rt.UserId)
                .IsRequired()
                .HasMaxLength(32);
        });

        modelBuilder.Entity<Project>(builder =>
        {
            builder.ToTable("Project");
            builder.HasKey(p => p.ProjectId);
            builder.Property(p => p.ProjectId)
                .ValueGeneratedNever();
            builder.Property(p => p.Name)
                .IsRequired();
            builder.Property(p => p.Description)
                .IsRequired();
            builder.Property(p => p.Path)
                .IsRequired();
            builder.Property(p => p.Visibility)
                .HasMaxLength(64)
                .IsRequired();
            builder.Property(p => p.CreateDate)
                .IsRequired();
            builder.Property(p => p.UpdateDate)
                .IsRequired();
            builder.Property(u => u.ConcurrencyToken)
                .IsConcurrencyToken();
            builder.HasMany(p => p.Issues)
                .WithOne(i => i.Project)
                .HasForeignKey(i => i.ProjectId)
                .HasPrincipalKey(p => p.ProjectId);
        });

        modelBuilder.Entity<Issue>(builder =>
        {
            builder.ToTable("Issue");
            builder.HasKey(i => i.IssueId);
            builder.Property(i => i.IssueId)
                .ValueGeneratedNever();
            builder.Property(i => i.Title)
                .IsRequired();
            builder.Property(i => i.Description)
                .IsRequired(false);
            builder.Property(i => i.CreateDate)
                .IsRequired();
            builder.Property(i => i.UpdateDate)
                .IsRequired();
            builder.Property(i => i.DueDate)
                .IsRequired(false);
            builder.Property(i => i.ConcurrencyToken)
                .IsConcurrencyToken();
            builder.HasOne(i => i.Project)
                .WithMany(p => p.Issues)
                .HasForeignKey(i => i.ProjectId)
                .HasPrincipalKey(p => p.ProjectId);
            builder.HasOne(i => i.Author)
                .WithMany(u => u.Issues)
                .HasForeignKey(i => i.AuthorId)
                .HasPrincipalKey(u => u.UserId);
            builder.HasOne(i => i.ClosedByUser)
                .WithMany(u => u.Issues)
                .HasForeignKey(i => i.ClosedByUserId)
                .HasPrincipalKey(u => u.UserId);

            builder.HasMany(i => i.TimeEntries)
                .WithOne(te => te.Issue)
                .HasForeignKey(te => te.IssueId)
                .HasPrincipalKey(i => i.IssueId);

            builder.HasMany(i => i.Assignees)
                .WithMany(u => u.Issues);
            builder.HasMany(i => i.Labels)
                .WithMany(l => l.Issues);
            builder.HasMany(i => i.Reactions)
                .WithMany(r => r.Issues);
        });

        modelBuilder.Entity<Reaction>(builder =>
        {
            builder.ToTable("Reaction");
            builder.HasKey(r => r.ReactionId);
            builder.Property(r => r.ReactionId)
                .ValueGeneratedNever();
        });

        modelBuilder.Entity<TimeEntry>(builder =>
        {
            builder.ToTable("TimeEntry");
            builder.HasKey(te => te.TimeEntryId);
            builder.Property(te => te.TimeEntryId)
                .ValueGeneratedNever();
            builder.Property(te => te.Hours)
                .IsRequired();
            builder.Property(te => te.Description)
                .IsRequired(false);
            builder.Property(te => te.DueDate)
                .IsRequired(false);
            builder.Property(te => te.CreateDate)
                .IsRequired();
            builder.Property(te => te.IsDeleted)
                .IsRequired();
            builder.Property(te => te.ConcurrencyToken)
                .IsConcurrencyToken();
            builder.HasOne(te => te.Issue)
                .WithMany()
                .HasForeignKey(te => te.IssueId)
                .HasPrincipalKey(p => p.IssueId);
            builder.HasOne(te => te.User)
                .WithMany()
                .HasForeignKey(te => te.UserId)
                .HasPrincipalKey(p => p.UserId);
        });

        modelBuilder.Entity<Label>(builder =>
        {
            builder.ToTable("Label");
            builder.HasKey(l => l.LabelId);
            builder.Property(l => l.LabelId)
                .ValueGeneratedNever();
            builder.Property(l => l.Title)
                .IsRequired();
            builder.Property(l => l.Description)
                .IsRequired(false);
            builder.Property(l => l.BackgroundColor)
                .IsRequired();
            builder.Property(l => l.IsDeleted)
                .IsRequired();
            builder.Property(l => l.CreateDate)
                .IsRequired();
            builder.Property(l => l.UpdateDate)
                .IsRequired();
            builder.Property(te => te.ConcurrencyToken)
                .IsConcurrencyToken();
        });
    }
}