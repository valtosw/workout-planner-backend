using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WorkoutPlanner.Models;

namespace WorkoutPlanner.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<TrainerRequest> TrainerRequests { get; set; }
        public DbSet<WorkoutPlan> WorkoutPlans { get; set; }
        public DbSet<ProgressLog> ProgressLogs { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<MuscleGroup> MuscleGroups { get; set; }
        public DbSet<WorkoutPlanEntry> WorkoutPlanEntries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>()
                .UseTphMappingStrategy();

            // Trainer - Customer many-to-many relationship
            modelBuilder.Entity<Trainer>()
                .HasMany(t => t.Customers)
                .WithMany(c => c.Trainers)
                .UsingEntity<Dictionary<string, object>>(
                    "TrainerCustomer",
                    j => j.HasOne<Customer>().WithMany().HasForeignKey("CustomerId"),
                    j => j.HasOne<Trainer>().WithMany().HasForeignKey("TrainerId"),
                    j => j.HasKey("CustomerId", "TrainerId")
                );

            // Trainer - TrainerRequest one-to-many relationship
            modelBuilder.Entity<Trainer>()
                .HasMany(t => t.ReceivedRequests)
                .WithOne(r => r.Trainer)
                .HasForeignKey(r => r.TrainerId)
                .OnDelete(DeleteBehavior.Cascade);

            // Customer - TrainerRequest one-to-many relationship
            modelBuilder.Entity<Customer>()
                .HasMany(c => c.SentRequests)
                .WithOne(r => r.Customer)
                .HasForeignKey(r => r.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            // Customer - ProgressLog one-to-many relationship
            modelBuilder.Entity<Customer>()
                .HasMany(c => c.ProgressLogs)
                .WithOne(pl => pl.Customer)
                .HasForeignKey(pl => pl.CustomerId);

            // Exercise - ProgressLog one-to-many relationship
            modelBuilder.Entity<Exercise>()
                .HasMany(e => e.ProgressLogs)
                .WithOne(pl => pl.Exercise)
                .HasForeignKey(pl => pl.ExerciseId);

            // WorkoutPlan - WorkoutPlanEntry one-to-many relationship
            modelBuilder.Entity<WorkoutPlan>()
                .HasMany(wp => wp.WorkoutPlanEntries)
                .WithOne(wpe => wpe.WorkoutPlan)
                .HasForeignKey(wpe => wpe.WorkoutPlanId);

            // Exercise - WorkoutPlanEntry one-to-many relationship
            modelBuilder.Entity<Exercise>()
                .HasMany(e => e.WorkoutPlanEntries)
                .WithOne(wpe => wpe.Exercise)
                .HasForeignKey(wpe => wpe.ExerciseId);

            modelBuilder.Entity<WorkoutPlan>()
                .HasOne(wp => wp.CreatedBy)
                .WithMany()
                .HasForeignKey(wp => wp.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<WorkoutPlan>()
               .HasOne(wp => wp.AssignedTo)
               .WithMany(c => c.WorkoutPlans)
               .HasForeignKey(wp => wp.AssignedToId)
               .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
