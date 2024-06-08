using candidatehub.Application.DTO;

namespace candidatehub.Application.Interface
{
    public interface ICandidateService
    {
        Task<long> AddOrUpdateCandidateAsync(CandidateDTO candidateDTO);
    }
}
