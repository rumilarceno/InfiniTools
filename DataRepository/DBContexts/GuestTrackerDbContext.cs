using DataRepository.Models;
using System.Data.Entity;

namespace DataRepository.DBContexts
{
    public class GuestTrackerDbContext : DbContext
    {
        public GuestTrackerDbContext() : base("InfinitToolsConnection")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<TimeTrackerDbContext>());
        }

        public DbSet<Guest> Guests { get; set; }
    }
}
