using CQRSApiTemplate.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CQRSApiTemplate.Application.Interfaces;

public interface ICQRSApiTemplateDbContext
{
    public DbSet<Category> Categories { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
