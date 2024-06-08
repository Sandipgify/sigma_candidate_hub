using candidatehub.Application.DTO;
using candidatehub.Application.Interface;
using candidatehub.Application.Validation;
using candidatehub.Domain;
using candidatehub.Domain.Infrastructure;
using FluentValidation;

namespace candidatehub.Application.Services
{
    public class CandidateService : ICandidateService
    {
        private readonly ICandidateRepository _candidateRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CandidateService(ICandidateRepository candidateRepository,
            IUnitOfWork unitOfWork)
        {
            _candidateRepository = candidateRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<long> AddOrUpdateCandidateAsync(CandidateDTO candidateDTO)
        {
            #region Validation
            var validation = new CandidateValidator();
            await validation.ValidateAndThrowAsync(candidateDTO);
            #endregion

            Candidate candidate = await _candidateRepository.GetbyEmail(candidateDTO.Email);
            if (candidate is null)
            {
                Candidate candateInfo = ToCandidate(candidateDTO);
                await _candidateRepository.AddAsync(candateInfo);
                await _unitOfWork.SaveChangesAsync();
                return candateInfo.Id;
            }
            else
            {
                candidate.FirstName = candidateDTO.FirstName;
                candidate.LastName = candidateDTO.LastName;
                candidate.PhoneNumber = candidateDTO.PhoneNumber;
                candidate.Email = candidateDTO.Email;
                candidate.CallTimeInterval = candidateDTO.CallTimeInterval;
                candidate.LinkedInUrl = candidateDTO.LinkedInUrl;
                candidate.GitHubUrl = candidateDTO.GitHubUrl;
                candidate.Comment = candidateDTO.Comment;
                _candidateRepository.Update(candidate);
                await _unitOfWork.SaveChangesAsync();
                return candidate.Id;
            }
        }

        private Candidate ToCandidate(CandidateDTO candidate)
        {
            return new Candidate
            {
                FirstName = candidate.FirstName,
                LastName = candidate.LastName,
                PhoneNumber = candidate.PhoneNumber,
                Email = candidate.Email,
                CallTimeInterval = candidate.CallTimeInterval,
                LinkedInUrl = candidate.LinkedInUrl,
                GitHubUrl = candidate.GitHubUrl,
                Comment = candidate.Comment
            };
        }
    }
}
