using System;

namespace candidatehub.Domain.Infrastructure
{
    public interface IUnitOfWork:IDisposable
    {
        Task BeginTransactionAsync();
        Task SaveChangesAsync(CancellationToken cancellationtoken = default);
        Task CommitTransactionAsync();
        Task RollbackAsync();

    }
}
