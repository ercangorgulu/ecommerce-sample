using ECommerce.Domain.Core.Models;
using ECommerce.Domain.Models;
using ECommerce.Domain.Services;
using ECommerce.Infra.Data.Maps;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Infra.Data.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Campaign> Campaigns { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CampaignMap());
            modelBuilder.ApplyConfiguration(new OrderMap());
            modelBuilder.ApplyConfiguration(new ProductMap());

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            OnBeforeSaving();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            OnBeforeSaving();
            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void OnBeforeSaving()
        {
            var entities = ChangeTracker.Entries()
                .Where(x => x.Entity is EntityAudit)
                .ToList();
            UpdateSoftDelete(entities);
            UpdateTimestamps(entities);
        }

        private void UpdateSoftDelete(List<EntityEntry> entries)
        {
            var filtered = entries
                .Where(x => x.State == EntityState.Added
                    || x.State == EntityState.Deleted);

            foreach (var entry in filtered)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        ((EntityAudit)entry.Entity).IsDeleted = false;
                        break;
                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        ((EntityAudit)entry.Entity).IsDeleted = true;
                        break;
                }
            }
        }

        private void UpdateTimestamps(List<EntityEntry> entries)
        {
            var filtered = entries
                .Where(x => x.State == EntityState.Added
                    || x.State == EntityState.Modified);

            // TODO: Get real current user id
            var currentUserId = 1;

            foreach (var entry in filtered)
            {
                if (entry.State == EntityState.Added)
                {
                    ((EntityAudit)entry.Entity).CreatedAt = DateTimeService.Current;
                    ((EntityAudit)entry.Entity).CreatedBy = currentUserId;
                }

                ((EntityAudit)entry.Entity).UpdatedAt = DateTimeService.Current;
                ((EntityAudit)entry.Entity).UpdatedBy = currentUserId;
            }
        }
    }
}
