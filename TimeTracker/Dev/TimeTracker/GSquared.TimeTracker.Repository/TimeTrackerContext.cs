using System.Data.Entity;
using GSquared.TimeTracker.Model.Entities;

namespace GSquared.TimeTracker.Repository
{
    public class TimeTrackerContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<TimeEntry> TimeEntries { get; set; }
        public DbSet<BillingCycle> BillingCycles { get; set; }
        public DbSet<BillingTerm> BillingTerms { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Set up foreignkey relationships
            modelBuilder.Entity<UserProfile>()
                .HasRequired(up => up.UserProfileUser)
                .WithRequiredDependent(u => u.Profile)
                .WillCascadeOnDelete(true);
        }
    }

    
}
