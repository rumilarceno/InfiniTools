using DataRepository.Models;
using System.Data.Entity;

namespace DataRepository.DBContexts
{
    public class TimeTrackerDbContext : DbContext
    {
        public TimeTrackerDbContext() : base("InfinitToolsConnection")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<TimeTrackerDbContext>());
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeTimeRecord> EmployeeTimeRecord { get; set; }
        public DbSet<ProjectTask> ProjectTasks { get; set; }
        public DbSet<Project> Project { get; set; }
    }
}
