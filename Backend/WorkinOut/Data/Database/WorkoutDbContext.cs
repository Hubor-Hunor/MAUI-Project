using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WorkinOut.Models;

namespace WorkinOut.Data.Database
{
    public class WorkoutDbContext : IdentityDbContext<Account, IdentityRole, string>
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Workout> Workouts { get; set; }
        public WorkoutDbContext(DbContextOptions<WorkoutDbContext> opt) : base(opt)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Person>()
                .HasOne(p => p.Account)
                .WithMany(a => a.Person)
                .IsRequired(false);

            modelBuilder.Entity<Workout>()
                .HasOne(w => w.Person)
                .WithMany(p => p.Workouts)
                .HasForeignKey(w => w.PersonId)
                .IsRequired(false);

            modelBuilder.Entity<Account>()
                .Property(a => a.Role).HasDefaultValue("User");

            modelBuilder.Entity<IdentityRole>().HasData(
              new { Id = "b0219923-3536-4da3-aeca-6878448434a5", Name = "User", NormalizedName = "USER" },
              new { Id = "83ac2b19-50eb-469d-9be1-c007048cccbf", Name = "Admin", NormalizedName = "ADMIN" },
              new { Id = "053aa2f9-5e15-4cbe-81a4-518f320b10fa", Name = "Trainer", NormalizedName = "TRAINER" }
            );

            PasswordHasher<Account> ph = new PasswordHasher<Account>();
            Account hunor = new Account
            {
                Id = "153aa2f9-3536-469d-81a4-6878448434a5",
                Email = "hunor@gmail.com",
                EmailConfirmed = true,
                FirstName = "Kozma",
                LastName = "Hunor",
                UserName = "hunor",
                NormalizedEmail = "HUNOR@GMAIL.COM",
                NormalizedUserName = "HUNOR"
            };
            hunor.PasswordHash = ph.HashPassword(hunor, "Hunor1!");
            modelBuilder.Entity<Account>().HasData(hunor);
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = "b0219923-3536-4da3-aeca-6878448434a5",
                UserId = hunor.Id
            });

            base.OnModelCreating(modelBuilder);
        }

    }
}