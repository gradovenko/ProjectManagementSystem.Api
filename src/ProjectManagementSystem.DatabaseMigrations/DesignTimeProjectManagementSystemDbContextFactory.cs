using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ProjectManagementSystem.DatabaseMigrations
{
    public class DesignTimeProjectManagementSystemDbContextFactory : IDesignTimeDbContextFactory<ProjectManagementSystemDbContext>
    {
        public ProjectManagementSystemDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ProjectManagementSystemDbContext>();
            optionsBuilder.UseNpgsql("migrations");
            return new ProjectManagementSystemDbContext(optionsBuilder.Options);
        }
    }
}