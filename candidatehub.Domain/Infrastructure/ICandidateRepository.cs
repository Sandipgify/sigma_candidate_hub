using System.Reflection;

namespace candidatehub.Domain.Infrastructure
{
    public interface ICandidateRepository : IRepository<Candidate>
    {
        Task<Candidate> GetbyEmail(string email);
    }
}
