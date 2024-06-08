using System.Reflection;
using candidatehub.Domain.Entity;

namespace candidatehub.Domain.Infrastructure
{
    public interface ICandidateRepository : IRepository<Candidate>
    {
        Task<Candidate> GetbyEmail(string email);
    }
}
