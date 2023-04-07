using Microsoft.EntityFrameworkCore;

namespace ProjectManagementSystem.Queries.Infrastructure.Issues;

public sealed class IssueQueryDbContext : DbContext
{
    public IssueQueryDbContext(DbContextOptions<IssueQueryDbContext> options) : base(options) { }

    internal DbSet<Issue> Issues { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Project>(builder =>
        {
            builder.ToTable("Project");
            builder.HasKey(p => p.ProjectId); ;
        });

        modelBuilder.Entity<User>(builder =>
        {
            builder.ToTable("User");
            builder.HasKey(u => u.UserId);
        });
            
        modelBuilder.Entity<Issue>(builder =>
        {
            builder.ToTable("Issue");
            builder.HasKey(i => i.IssueId);
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
            
            builder.HasOne(i => i.Author)
                .WithMany()
                .HasForeignKey(i => i.AuthorId)
                .HasPrincipalKey(u => u.UserId);

            builder.HasMany(i => i.Assignees)
                .WithMany(u => u.Issues);
        });
    }
}