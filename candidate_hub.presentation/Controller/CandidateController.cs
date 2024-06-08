using candidatehub.Application.DTO;
using candidatehub.Application.Interface;
using candidatehub.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace candidatehub.Presentation.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateController : ControllerBase
    {
        private readonly ICandidateService _candidateService;

        public CandidateController(ICandidateService candidateService)
        {
            _candidateService = candidateService;
        }

        [HttpPost]
        [SwaggerOperation(
        Summary = "Add/Update candidate",
           Description = "If email exist update otherwise add candidate",
           OperationId = "Contactus.createorupdate",
           Tags = new[] { "Candidate" })]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status200OK,type: typeof(long))]

        public async Task<IActionResult> AddOrUpdateCandidate([FromBody] CandidateDTO candidateDTO)
        {
            var candidateId = await _candidateService.AddOrUpdateCandidateAsync(candidateDTO);
            return Ok(candidateId);
        }

    }
}
