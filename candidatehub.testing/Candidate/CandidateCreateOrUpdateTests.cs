using FluentValidation;

namespace candidatehub.testing.Candidate
{
    public class CandidateCreateOrUpdateTests
    {
        private Fixture _fixture;
        private Mock<ICandidateRepository> _candidateRepositoryMock;
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private CandidateService _candidateService;
        private CandidateDTO _candidateDTO;
        public CandidateCreateOrUpdateTests()
        {
            _fixture = new Fixture();
            _candidateDTO = _fixture.Create<CandidateDTO>();
            SetCandidate(_candidateDTO);
        }

        [SetUp]
        public void Setup()
        {
            _candidateRepositoryMock = new Mock<ICandidateRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _candidateService = new CandidateService(_candidateRepositoryMock.Object,_unitOfWorkMock.Object);
        }

        [Test]
        public async Task Create_Candidate_When_Not_Exists_Should_Succeed()
        {
            _candidateRepositoryMock.Setup(repo => repo.GetbyEmail(_candidateDTO.Email))
                .ReturnsAsync((Domain.Entity.Candidate)null);

            var result = await _candidateService.AddOrUpdateCandidateAsync(_candidateDTO);
            _candidateRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Domain.Entity.Candidate>()), Times.Once);
            _candidateRepositoryMock.Verify(repo => repo.Update(It.IsAny<Domain.Entity.Candidate>()), Times.Never);
            _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }


        [Test]
        public async Task Update_Existing_Candidate_Should_Succeed()
        {
            var existingCandidate = _fixture.Create<Domain.Entity.Candidate>();
            _candidateRepositoryMock.Setup(repo => repo.GetbyEmail(_candidateDTO.Email))
                .ReturnsAsync(existingCandidate);

            var result = await _candidateService.AddOrUpdateCandidateAsync(_candidateDTO);
            result.ShouldBe(existingCandidate.Id);
            _candidateRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Domain.Entity.Candidate>()), Times.Never);
            _candidateRepositoryMock.Verify(repo => repo.Update(existingCandidate), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }


        [Test]
        public async Task FirstName_Not_Empty()
        {
            var candidate = _fixture.Create<CandidateDTO>();
            SetCandidate(candidate);
            candidate.FirstName = null;
            await ValidateAndVerifyAsync(candidate);
        }

        [Test]
        public async Task LastName_Not_Empty()
        {
            var candidate = _fixture.Create<CandidateDTO>();
            SetCandidate(candidate);
            candidate.LastName = null;
            await ValidateAndVerifyAsync(candidate);
        }

        [Test]
        public async Task Email_Not_Empty()
        {
            var candidate = _fixture.Create<CandidateDTO>();
            SetCandidate(candidate);
            candidate.Email = null;
            await ValidateAndVerifyAsync(candidate);
        }

        [Test]
        public async Task Email_Valid_Format()
        {
            var candidate = _fixture.Create<CandidateDTO>();
            SetCandidate(candidate);
            candidate.Email = "invalidemail";
            await ValidateAndVerifyAsync(candidate);
        }

        [Test]
        public async Task PhoneNumber_Invalid_Format()
        {
            var candidate = _fixture.Create<CandidateDTO>();
            SetCandidate(candidate);
            candidate.PhoneNumber = "89465646";
            await ValidateAndVerifyAsync(candidate);
        }

        [Test]
        public async Task LinkedInUrl_Invalid_Format()
        {
            var candidate = _fixture.Create<CandidateDTO>();
            SetCandidate(candidate);
            candidate.LinkedInUrl = "invalidurl";
            await ValidateAndVerifyAsync(candidate);
        }

        [Test]
        public async Task GitHubUrl_Invalid_Format()
        {
            var candidate = _fixture.Create<CandidateDTO>();
            SetCandidate(candidate);
            candidate.GitHubUrl = "invalidurl";

            await ValidateAndVerifyAsync(candidate);
        }

        #region Helper
        private CandidateDTO SetCandidate(CandidateDTO candidateDTO)
        {
            candidateDTO.Email = "test@email.com";
            candidateDTO.PhoneNumber = "9816454545";
            candidateDTO.GitHubUrl = "https://www.github.com/testdata";
            candidateDTO.LinkedInUrl = "https://www.linkedin.com/in/testdata";
            return candidateDTO;
        }

        private async Task ValidateAndVerifyAsync(CandidateDTO candidate)
        {
            await Should.ThrowAsync<ValidationException>(async () => await _candidateService.AddOrUpdateCandidateAsync(candidate));
            _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        }
        #endregion
    }
}
