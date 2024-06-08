using candidatehub.Domain.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace candidatehub.Infrastructure.Repository
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        private readonly CandidateHubContext _dbContext;
        protected Repository(CandidateHubContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<T> AddAsync(T t)
        {
            await _dbContext.Set<T>().AddAsync(t);
            return t;
        }
        public void Update(T t) => _dbContext.Update(t);
        public async Task AddRangeAsync(IEnumerable<T> t) => await _dbContext.AddRangeAsync(t);
        public void UpdateRange(IEnumerable<T> t) => _dbContext.UpdateRange(t);

    }
}
