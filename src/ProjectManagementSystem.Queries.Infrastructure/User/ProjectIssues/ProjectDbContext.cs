using Microsoft.EntityFrameworkCore;

namespace ProjectManagementSystem.Queries.Infrastructure.User.ProjectIssues
{
    public sealed class ProjectDbContext : DbContext
    {
        public ProjectDbContext(DbContextOptions<ProjectDbContext> options) : base(options) { }

        internal DbSet<Project> Projects { get; set; }
        internal DbSet<Tracker> Trackers { get; set; }
        internal DbSet<IssueStatus> IssueStatuses { get; set; }
        internal DbSet<IssuePriority> IssuePriorities { get; set; }
        internal DbSet<User> Users { get; set; }
        internal DbSet<Issue> Issues { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Project>(builder =>
            {
                builder.ToTable("Project");
                builder.HasKey(p => p.Id);
                builder.Property(p => p.Id)
                    .HasColumnName("Id")
                    .ValueGeneratedNever();
            });

            modelBuilder.Entity<Tracker>(builder =>
            {
                builder.ToTable("Tracker");
                builder.HasKey(t => t.Id);
                builder.Property(t => t.Id)
                    .HasColumnName("Id")
                    .ValueGeneratedNever();
            });

            modelBuilder.Entity<IssueStatus>(builder =>
            {
                builder.ToTable("IssueStatus");
                builder.HasKey(@is => @is.Id);
                builder.Property(@is => @is.Id)
                    .HasColumnName("Id")
                    .ValueGeneratedNever();
            });
            
            modelBuilder.Entity<IssuePriority>(builder =>
            {
                builder.ToTable("IssuePriority");
                builder.HasKey(ip => ip.Id);
                builder.Property(ip => ip.Id)
                    .HasColumnName("Id")
                    .ValueGeneratedNever();
            });
            
            modelBuilder.Entity<User>(builder =>
            {
                builder.ToTable("User");
                builder.HasKey(u => u.Id);
                builder.Property(u => u.Id)
                    .HasColumnName("Id")
                    .ValueGeneratedNever();
            });
            
            modelBuilder.Entity<Issue>(builder =>
            {
                builder.ToTable("Issue");
                builder.HasKey(i => i.Id);
                builder.Property(i => i.Id)
                    .HasColumnName("Id")
                    .ValueGeneratedNever();
                builder.Property(i => i.Title)
                    .HasColumnName("Title")
                    .IsRequired();
                builder.Property(i => i.Description)
                    .HasColumnName("Description")
                    .IsRequired();
                builder.Property(i => i.CreateDate)
                    .HasColumnName("CreateDate")
                    .IsRequired();
                builder.Property(i => i.StartDate)
                    .HasColumnName("StartDate");
                builder.Property(i => i.EndDate)
                    .HasColumnName("EndDate");
                builder.Property(i => i.TrackerId)
                    .HasColumnName("TrackerId")
                    .IsRequired();
                builder.Property(i => i.StatusId)
                    .HasColumnName("StatusId")
                    .IsRequired();
                builder.Property(i => i.PriorityId)
                    .HasColumnName("PriorityId")
                    .IsRequired();
                builder.Property(i => i.AuthorId)
                    .HasColumnName("AuthorId")
                    .IsRequired();
                builder.Property(i => i.PerformerId)
                    .HasColumnName("PerformerId")
                    .IsRequired();
                builder.Property("_concurrencyStamp")
                    .HasColumnName("ConcurrencyStamp")
                    .IsConcurrencyToken();
                builder.HasOne(i => i.Tracker)
                    .WithMany()
                    .HasForeignKey(i => i.TrackerId)
                    .HasPrincipalKey(t => t.Id);
                builder.HasOne(i => i.Status)
                    .WithMany()
                    .HasForeignKey(i => i.StatusId)
                    .HasPrincipalKey(@is => @is.Id);
                builder.HasOne(i => i.Priority)
                    .WithMany()
                    .HasForeignKey(i => i.PriorityId)
                    .HasPrincipalKey(ip => ip.Id);
                builder.HasOne(i => i.Author)
                    .WithMany()
                    .HasForeignKey(i => i.AuthorId)
                    .HasPrincipalKey(a => a.Id);
                builder.HasOne(i => i.Performer)
                    .WithMany()
                    .HasForeignKey(i => i.PerformerId)
                    .HasPrincipalKey(p => p.Id);
            });
        }
    }
}