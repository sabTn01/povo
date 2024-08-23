using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using POVO.Backend.Domains;

namespace POVO.Backend.Infrastructure.Contexts { 
    public abstract class BaseDbContext : DbContext
    {
        protected BaseDbContext(DbContextOptions options) : base(options)
        {
        }

        public override int SaveChanges()
        {
            StampTimeFields();

            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            StampTimeFields();

            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            AlterDateTimeMaterializing(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private static void AlterDateTimeMaterializing(ModelBuilder modelBuilder)
        {
            var dateTimeConverter = new ValueConverter<DateTime, DateTime>(
                v => v.ToUniversalTime(),
                v => DateTime.SpecifyKind(v, DateTimeKind.Utc)
            );

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties())
                {
                    if (property.ClrType == typeof(DateTime) || property.ClrType == typeof(DateTime?))
                        property.SetValueConverter(dateTimeConverter);
                }
            }
        }

        private void StampTimeFields()
        {
            var entities = ChangeTracker.Entries().Where(x =>
                x.Entity is BaseModel && (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((BaseModel)entity.Entity).CreatedAtClient = DateTime.UtcNow;
                    ((BaseModel)entity.Entity).CreatedAtServer = DateTime.UtcNow;
                }

                ((BaseModel)entity.Entity).UpdatedAtServer = DateTime.UtcNow;
            }
        }
    }
}
