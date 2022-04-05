using CQRSApiTemplate.Application.Interfaces;
using CQRSApiTemplate.Domain.Abstraction;
using CQRSApiTemplate.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CQRSApiTemplate.Infrastructure.Persistence;

public class CQRSApiTemplateDbContext : DbContext, ICQRSApiTemplateDbContext
{
    public DbSet<Category> Categories { get; set; }

    private readonly IDateTimeService _dateTimeService;
    private readonly ICurrentUser _currentUser;

    public CQRSApiTemplateDbContext(DbContextOptions<CQRSApiTemplateDbContext> options) : base(options)
    {
        _dateTimeService = this.GetService<IDateTimeService>();
        _currentUser = this.GetService<ICurrentUser>();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var modifiedEntries = ChangeTracker.Entries()
           .Where(x => x.Entity is AuditableEntity
           && (x.State == EntityState.Added || x.State == EntityState.Modified));
        
        var now = _dateTimeService.Now;
        foreach (var entry in modifiedEntries)
        {
            var entity = entry.Entity as AuditableEntity;
            if (entry.State == EntityState.Added)
            {
                entity.Created = now;
                entity.CreatedBy = _currentUser.GetUserId();
            }
            else
            {
                entity.LastModified = now;
                entity.LastModifiedBy = _currentUser.GetUserId();
            }
        }

        return await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Seed();
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CQRSApiTemplateDbContext).Assembly);
    }
}
