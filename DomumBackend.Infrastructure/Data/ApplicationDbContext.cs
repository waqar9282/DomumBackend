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

        // Phase 1: Compliance & Audit
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<Incident> Incidents { get; set; }
        public DbSet<SafetyAlert> SafetyAlerts { get; set; }
        public DbSet<SafeguardingConcern> SafeguardingConcerns { get; set; }

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

            // ===== PHASE 1: COMPLIANCE & AUDIT =====

            // AuditLog configuration
            modelBuilder.Entity<AuditLog>()
                .HasKey(a => a.Id);
            modelBuilder.Entity<AuditLog>()
                .HasIndex(a => a.EntityId);
            modelBuilder.Entity<AuditLog>()
                .HasIndex(a => a.UserId);
            modelBuilder.Entity<AuditLog>()
                .HasIndex(a => a.Timestamp);

            // Incident configuration
            modelBuilder.Entity<Incident>()
                .HasKey(i => i.Id);
            modelBuilder.Entity<Incident>()
                .HasOne(i => i.Facility)
                .WithMany()
                .HasForeignKey(i => i.FacilityId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Incident>()
                .HasOne(i => i.YoungPerson)
                .WithMany()
                .HasForeignKey(i => i.YoungPersonId)
                .OnDelete(DeleteBehavior.NoAction); // Incident records must persist even if young person is deleted (compliance)
            modelBuilder.Entity<Incident>()
                .HasIndex(i => i.FacilityId);
            modelBuilder.Entity<Incident>()
                .HasIndex(i => i.YoungPersonId);
            modelBuilder.Entity<Incident>()
                .HasIndex(i => i.IncidentDate);
            modelBuilder.Entity<Incident>()
                .HasIndex(i => i.Status);

            // SafetyAlert configuration
            modelBuilder.Entity<SafetyAlert>()
                .HasKey(sa => sa.Id);
            modelBuilder.Entity<SafetyAlert>()
                .HasOne(sa => sa.Facility)
                .WithMany()
                .HasForeignKey(sa => sa.FacilityId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<SafetyAlert>()
                .HasOne(sa => sa.YoungPerson)
                .WithMany()
                .HasForeignKey(sa => sa.YoungPersonId)
                .OnDelete(DeleteBehavior.NoAction); // Safety alerts must persist for audit trail
            modelBuilder.Entity<SafetyAlert>()
                .HasIndex(sa => sa.FacilityId);
            modelBuilder.Entity<SafetyAlert>()
                .HasIndex(sa => sa.YoungPersonId);
            modelBuilder.Entity<SafetyAlert>()
                .HasIndex(sa => sa.AlertLevel);
            modelBuilder.Entity<SafetyAlert>()
                .HasIndex(sa => sa.IsActive);

            // SafeguardingConcern configuration
            modelBuilder.Entity<SafeguardingConcern>()
                .HasKey(sc => sc.Id);
            modelBuilder.Entity<SafeguardingConcern>()
                .HasOne(sc => sc.Facility)
                .WithMany()
                .HasForeignKey(sc => sc.FacilityId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<SafeguardingConcern>()
                .HasOne(sc => sc.YoungPerson)
                .WithMany()
                .HasForeignKey(sc => sc.YoungPersonId)
                .OnDelete(DeleteBehavior.NoAction); // Safeguarding records must persist for compliance
            
            // Many-to-many: Incident <-> SafeguardingConcern (with NoAction on join table)
            modelBuilder.Entity<SafeguardingConcern>()
                .HasMany(sc => sc.LinkedIncidents)
                .WithMany(i => i.LinkedConcerns)
                .UsingEntity<Dictionary<string, object>>(
                    "IncidentSafeguardingConcern",
                    j => j.HasOne<Incident>().WithMany().OnDelete(DeleteBehavior.NoAction),
                    j => j.HasOne<SafeguardingConcern>().WithMany().OnDelete(DeleteBehavior.NoAction));
            
            modelBuilder.Entity<SafeguardingConcern>()
                .HasIndex(sc => sc.FacilityId);
            modelBuilder.Entity<SafeguardingConcern>()
                .HasIndex(sc => sc.YoungPersonId);
            modelBuilder.Entity<SafeguardingConcern>()
                .HasIndex(sc => sc.ConcernType);
            modelBuilder.Entity<SafeguardingConcern>()
                .HasIndex(sc => sc.Status);
        }
    }
}
