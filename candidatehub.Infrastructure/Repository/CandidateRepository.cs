using candidatehub.Domain;
using candidatehub.Domain.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;

namespace candidatehub.Infrastructure.Repository
{
    public class CandidateRepository: Repository<Candidate>, ICandidateRepository
    {
        private readonly CandidateHubContext _dbContext;

        public CandidateRepository(CandidateHubContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Candidate> GetbyEmail(string email)
        {
            return await _dbContext.Candidates.Where(x=>x.Email == email).FirstOrDefaultAsync();
        }
    }
}
