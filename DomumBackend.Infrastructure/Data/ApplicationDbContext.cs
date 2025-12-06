using DomumBackend.Domain.Entities;
using DomumBackend.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;



namespace DomumBackend.Infrastructure.Data
{

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions)
            : base(dbContextOptions)
        {
        }

        /* protected override void OnModelCreating(ModelBuilder modelBuilder)
         {
             base.OnModelCreating(modelBuilder);
             modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
         }
        */
        public DbSet<UserFacility> UserFacilities { get; set; }
        public DbSet<UserYoungPerson> UserYoungPersons { get; set; }
        public DbSet<Facility> Facilities { get; set; }
        public DbSet<YoungPerson> YoungPeople { get; set; }
        public DbSet<Room> Rooms { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // One-to-one: Room <-> YoungPerson
            modelBuilder.Entity<Room>()
                .HasOne(r => r.YoungPerson)
                .WithOne(yp => yp.Room)
                .HasForeignKey<YoungPerson>(yp => yp.RoomId);

            // Facility and Room: One-to-many
            modelBuilder.Entity<Facility>()
                .HasMany(f => f.Rooms)
                .WithOne(r => r.Facility)
                .HasForeignKey(r => r.FacilityId);

            // Facility and YoungPerson: One-to-many
            modelBuilder.Entity<Facility>()
                .HasMany(f => f.YoungPersons)
                .WithOne(yp => yp.Facility)
                .HasForeignKey(yp => yp.FacilityId);

            // UserFacility: Composite Key and Relationships
            modelBuilder.Entity<UserFacility>()
                .HasKey(uf => new { uf.UserId, uf.FacilityId });

            modelBuilder.Entity<UserFacility>()
                .HasOne(uf => uf.Facility)
                .WithMany(f => f.UserFacilities)
                .HasForeignKey(uf => uf.FacilityId);

            // UserYoungPerson: Composite Key and Relationships
            modelBuilder.Entity<UserYoungPerson>()
                .HasKey(uy => new { uy.UserId, uy.YoungPersonId });

            modelBuilder.Entity<UserYoungPerson>()
                .HasOne(uy => uy.YoungPerson)
                .WithMany(yp => yp.UserYoungPersons)
                .HasForeignKey(uy => uy.YoungPersonId);

            // ApplicationUser navigation properties
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.UserFacilities)
                .WithOne()
                .HasForeignKey(uf => uf.UserId);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.UserYoungPersons)
                .WithOne()
                .HasForeignKey(uy => uy.UserId);

            modelBuilder.Entity<YoungPerson>()
                .Property(y => y.Height)
                .HasPrecision(5, 2); // Allows values like 123.45
        }
    }
}
