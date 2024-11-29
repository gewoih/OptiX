using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OptiX.Domain.Entities.Asset;
using OptiX.Domain.Entities.Base;
using OptiX.Domain.Entities.Identity;
using OptiX.Domain.Entities.User;

namespace Optix.Infrastructure.Database;

public sealed class AppDbContext : IdentityDbContext<User, Role, Guid>
{
    public DbSet<UserProfile> UserProfiles { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Asset> Assets { get; set; }
    public DbSet<Tick> Ticks { get; set; }
    public DbSet<Trade> Trades { get; set; }
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public override int SaveChanges()
    {
        OnBeforeSaving();
        return base.SaveChanges();
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        OnBeforeSaving();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        OnBeforeSaving();
        return base.SaveChangesAsync(cancellationToken);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = new())
    {
        OnBeforeSaving();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    private void OnBeforeSaving()
    {
        var entries = ChangeTracker.Entries();
        foreach (var entry in entries)
        {
            var utcNow = DateTime.UtcNow;
            if (entry.Entity is Entity trackable)
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        trackable.ModifiedDate = utcNow;
                        break;
                    case EntityState.Added:
                        trackable.CreatedDate = utcNow;
                        break;
                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        trackable.IsDeleted = true;
                        trackable.DeletedDate = utcNow;
                        break;
                }
            }
        }
    }
}