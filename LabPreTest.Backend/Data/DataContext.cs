using LabPreTest.Shared.Entities;
using LabPreTest.Shared.Enums;
using LabPreTest.Shared.Messages;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LabPreTest.Backend.Data
{
    public class DataContext : IdentityDbContext<User>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DataContext(DbContextOptions<DataContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            Database.SetCommandTimeout(600);
            _httpContextAccessor = httpContextAccessor;
        }

        // for each database entity you need create a DbSet
        public DbSet<Country> Countries { get; set; }

        public DbSet<SectionImage> SectionImage { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<Medic> Medicians { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<OrderAudit> OrderAudits { get; set; }
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

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var transaction = await Database.BeginTransactionAsync(cancellationToken);
            await ApplyAuditInfoAsync(cancellationToken);
            var r = await base.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
            return r;
        }

        public override int SaveChanges()
        {
            var transaction = Database.BeginTransaction();
            ApplyAuditInfo();
            var r = base.SaveChanges();
            transaction.Commit();
            return r;
        }

        private void ApplyAuditInfo()
        {
            //var userName = _httpContextAccessor.HttpContext?.User?.Identity?.Name;
            var user = _httpContextAccessor.HttpContext?.User;

            var audits = new List<OrderAudit>();

            // catch all changes related to Orders
            foreach (var entry in ChangeTracker.Entries<Order>())
            {
                // TODO: Throw an exception
                if (user == null || user.Identity == null || user.Identity.Name == null || !user.Identity.IsAuthenticated)
                    return;

                if (entry.State == EntityState.Modified || entry.State == EntityState.Added || entry.State == EntityState.Deleted)
                {
                    ChangeType changeType = entry.State == EntityState.Modified ? ChangeType.Update :
                                            entry.State == EntityState.Deleted ? ChangeType.Delete :
                                            ChangeType.Insert;

                    if (entry.State == EntityState.Added)
                        base.SaveChanges();
                    
                    audits.Add(new OrderAudit
                    {
                        OrderId = entry.Entity.Id,
                        ChangeType = changeType,
                        ChangeDate = DateTime.UtcNow,
                        ChangeBy = user.Identity.Name,
                        OldValues = "null",
                        NewValues = "null"
                    });
                }
            }

            // Add information related to changes to Orders
            OrderAudits.AddRange(audits);
        }

        private async Task ApplyAuditInfoAsync(CancellationToken cancellationToken)
        {
            //var userName = _httpContextAccessor.HttpContext?.User?.Identity?.Name;
            var user = _httpContextAccessor.HttpContext?.User;

            var audits = new List<OrderAudit>();

            // catch all changes related to Orders
            foreach (var entry in ChangeTracker.Entries<Order>())
            {
                if (user == null || user.Identity == null || user.Identity.Name == null || !user.Identity.IsAuthenticated)
                    throw new UnauthorizedAccessException(MessageStrings.UserDoesNotHavePermissions);

                if (entry.State == EntityState.Modified || entry.State == EntityState.Added || entry.State == EntityState.Deleted)
                {
                    ChangeType changeType = entry.State == EntityState.Modified ? ChangeType.Update :
                                            entry.State == EntityState.Deleted ? ChangeType.Delete :
                                            ChangeType.Insert;

                    if (entry.State == EntityState.Added)
                        await base.SaveChangesAsync(cancellationToken);

                    audits.Add(new OrderAudit
                    {
                        OrderId = entry.Entity.Id,
                        ChangeType = changeType,
                        ChangeDate = DateTime.UtcNow,
                        ChangeBy = user.Identity.Name,
                        OldValues = "null",
                        NewValues = "null"
                    });
                }
            }

            // Add information related to changes to Orders
            OrderAudits.AddRange(audits);
        }
    }
}