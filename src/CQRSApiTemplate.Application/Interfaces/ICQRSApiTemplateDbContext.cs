using CQRSApiTemplate.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace CQRSApiTemplate.Application.Interfaces
{
    public interface ICQRSApiTemplateDbContext
    {
        public DbSet<Category> Categories { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
