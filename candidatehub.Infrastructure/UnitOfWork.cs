using candidatehub.Domain.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;

namespace candidatehub.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CandidateHubContext _dbContext;
        private IDbContextTransaction _transaction;
        private bool disposed = false;
        public UnitOfWork(CandidateHubContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        public async Task BeginTransactionAsync()
        {
            _transaction = await _dbContext.Database.BeginTransactionAsync();
        }
        public async Task SaveChangesAsync(CancellationToken cancellationtoken = default) => await _dbContext.SaveChangesAsync(cancellationtoken);
        public async Task CommitTransactionAsync()
        {
            try
            {
                await _dbContext.SaveChangesAsync();
                await _transaction.CommitAsync();
            }
            catch
            {
                await RollbackAsync();
                throw;
            }
        }
        public async Task RollbackAsync() => await _dbContext.Database.RollbackTransactionAsync();

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
