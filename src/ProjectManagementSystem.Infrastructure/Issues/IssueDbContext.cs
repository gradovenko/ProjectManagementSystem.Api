using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Domain.Issues;

namespace ProjectManagementSystem.Infrastructure.Issues;

public sealed class IssueDbContext : DbContext
{
    public IssueDbContext(DbContextOptions<IssueDbContext> options) : base(options) { }

    internal DbSet<Project> Projects { get; set; }
    internal DbSet<User> Users { get; set; }
    internal DbSet<Issue> Issues { get; set; }
    internal DbSet<Reaction> Reactions { get; set; }
    internal DbSet<Label> Labels { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Project>(builder =>
        {
            builder.ToTable("Project");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id)
                .HasColumnName("ProjectId");
        });

            
        modelBuilder.Entity<User>(builder =>
        {
            builder.ToTable("User");
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id)
                .HasColumnName("UserId");
        });
            
        modelBuilder.Entity<Issue>(builder =>
        {
            builder.ToTable("Issue");
            builder.HasKey(i => i.Id);
            builder.Property(i => i.Id)
                .HasColumnName("IssueId")
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
            builder.Property(i => i.ClosedByUserId)
                .IsRequired(false);
            builder.Property(i => i.ProjectId)
                .IsRequired();
            builder.Property(i => i.AuthorId)
                .IsRequired();
            builder.Property("_concurrencyToken")
                .HasColumnName("ConcurrencyToken")
                .IsConcurrencyToken();
            // builder.HasMany(i => i.Assignees)
            //     .WithMany(u => u.Issues);
            // builder.HasMany(i => i.Labels)
            //     .WithMany(l => l.Issues);
            // builder.HasMany(i => i.Reactions)
            //     .WithMany(r => r.Issues);
        });
    }
}