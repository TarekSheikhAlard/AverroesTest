using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.BaseDbContext
{
    public abstract class BaseDbContext<T> : DbContext where T : DbContext
    {
        protected BaseDbContext(DbContextOptions<T> options) : base(options)
        {
        }

        protected abstract string GetSchemaName();

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                return await base.SaveChangesAsync(cancellationToken);
            }
            catch (Exception)
            {
                HandleValidationErrors();
                throw;
            }

        }
        private void HandleValidationErrors()
        {
            foreach (var entry in ChangeTracker.Entries())
                if (entry.State == EntityState.Modified || entry.State == EntityState.Added)
                    entry.State = EntityState.Detached;
        }
    }
}
