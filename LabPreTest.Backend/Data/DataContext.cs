using LabPreTest.Shared.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LabPreTest.Backend.Data
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            Database.SetCommandTimeout(600);
        }

        // for each database entity you need create a DbSet
        public DbSet<Country> Countries { get; set; }

        public DbSet<SectionImage> SectionImage { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<Medic> Medicians { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<OrderDetail> OrderAudits { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<PreanalyticCondition> PreanalyticConditions { get; set; }
        public DbSet<TemporalOrder> TemporalOrders { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<TestTube> TestTubes { get; set; }
        public DbSet<Section> Section { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Country>().HasIndex(x => x.Name).IsUnique();
            modelBuilder.Entity<City>().HasIndex(x => new { x.StateId, x.Name }).IsUnique();
            modelBuilder.Entity<State>().HasIndex(x => new { x.CountryId, x.Name }).IsUnique();
            modelBuilder.Entity<Patient>().HasIndex(x => x.Email).IsUnique();
            modelBuilder.Entity<Patient>().HasIndex(x => x.DocumentId).IsUnique();
            modelBuilder.Entity<Patient>().HasIndex(x => x.UserName).IsUnique();
            modelBuilder.Entity<PreanalyticCondition>().HasIndex(x => x.Name).IsUnique();
            modelBuilder.Entity<Medic>().HasIndex(x => x.Email).IsUnique();
            modelBuilder.Entity<Medic>().HasIndex(x => x.DocumentId).IsUnique();
            modelBuilder.Entity<Medic>().HasIndex(x => x.UserName).IsUnique();
            modelBuilder.Entity<Test>().HasIndex(x => x.TestID).IsUnique();
            modelBuilder.Entity<TestTube>().HasIndex(x => x.Name).IsUnique();
            modelBuilder.Entity<Order>().HasIndex(x => x.Id).IsUnique();
            modelBuilder.Entity<OrderAudit>().HasIndex(x => x.Id).IsUnique();
            modelBuilder.Entity<SectionImage>().HasIndex(x => x.Id).IsUnique();

            DisableCascadingDelete(modelBuilder);
        }

        private void DisableCascadingDelete(ModelBuilder modelBuilder)
        {
            var relationships = modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys());
            foreach (var relationship in relationships)
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }

        public override int SaveChanges()
        {
            var audits = new List<OrderAudit>();
            foreach (var entry in ChangeTracker.Entries<Order>())
            {
                if (entry.State == EntityState.Modified || entry.State == EntityState.Added || entry.State == EntityState.Deleted)
                {
                    var audit = new OrderAudit
                    {
                        OrderId = entry.Entity.Id,
                        ChangeType = Shared.Enums.ChangeType.
                        ChangeDate = DateTime.UtcNow,
                    }
                }
            }

            return base.SaveChanges();
        }
    }
}