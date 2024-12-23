using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OptiX.Domain.Entities.Asset;
using OptiX.Domain.Entities.Base;
using OptiX.Domain.Entities.Identity;
using OptiX.Domain.Entities.Trading;
using OptiX.Domain.Entities.User;

namespace Optix.Infrastructure.Database;

public sealed class AppDbContext : IdentityDbContext<User, Role, Guid>
{
    public DbSet<UserProfile> UserProfiles { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Tick> Ticks { get; set; }
    public DbSet<Trade> Trades { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Tick>().HasIndex(t => new { t.Symbol, t.Date });
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
                        trackable.ModifiedAt = utcNow;
                        break;
                    case EntityState.Added:
                        trackable.CreatedAt = utcNow;
                        break;
                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        trackable.IsDeleted = true;
                        trackable.DeletedAt = utcNow;
                        break;
                }
            }
        }
    }
}