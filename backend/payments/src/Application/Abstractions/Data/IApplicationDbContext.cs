
using Microsoft.EntityFrameworkCore;

namespace Application.Abstractions.Data
{
    public interface IApplicationDbContext
    {


        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
