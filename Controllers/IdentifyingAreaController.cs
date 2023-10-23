using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NovelNestLibraryAPI.Services;

namespace NovelNestLibraryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentifyingAreaController : ControllerBase
    {
        private readonly IdentifyingAreaService _identifyingAreaService;

        public IdentifyingAreaController(IdentifyingAreaService identifyingAreaService) => 
            _identifyingAreaService = identifyingAreaService;

        [HttpGet("generateIdentifyingAreaQuiz")]
        public ActionResult<object> GetQuizQuestions()
        {
            var (questions, possibleAnswers) = _identifyingAreaService.GetQuizQuestions();

            return new
            {
                Questions = questions,
                PossibleAnswers = possibleAnswers
            };
        }
        [HttpPost("check-answers")]
        public IActionResult CheckAnswers([FromBody] Dictionary<string, string> userResponses)
        {
            if (userResponses == null || userResponses.Count == 0)
            {
                return BadRequest("User responses are missing or empty.");
            }

            if (userResponses.Keys.First().Length == 3)
            {
                bool isCorrect = _identifyingAreaService.CheckCallNumberAnswers(userResponses);

                if (isCorrect)
                {
                    return Ok(true);
                }
                else
                {
                    return BadRequest(false);
                }
            }
            else
            {
                bool isCorrect = _identifyingAreaService.CheckDescriptionAnswers(userResponses);

                if (isCorrect)
                {
                    return Ok(true);
                }
                else
                {
                    return BadRequest(false);
                }
            }

        }

    }
}
