using Microsoft.AspNetCore.Mvc;
using NovelNestLibraryAPI.Models;
using NovelNestLibraryAPI.Services;

namespace NovelNestLibraryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReplacingBookQuizController : ControllerBase
    {
        private readonly ReplacingBookQuizService _quizService;

        public ReplacingBookQuizController(ReplacingBookQuizService quizService) =>
            _quizService = quizService;

        [HttpGet("generateQuiz")]
        public async Task<ActionResult<ReplacingBookQuiz>> GenerateQuiz()
        {
            try
            {
                var quiz = await _quizService.GenerateQuiz();
                return Ok(quiz);
            }
            catch (Exception ex)
            {

                return StatusCode(500, new { message = ex.Message });
            }

        }

        [HttpPost("calculatePoints")]
        public async Task<ActionResult<ReplacingBookQuiz>> CalculatePoints([FromBody] ReplacingBookQuiz quiz)
        {
            var points = await _quizService.CalculatePoints(quiz);
            return Ok(points);
        }

        [HttpPost("generateCorrectOrder")]
        public async Task<ActionResult<List<string>>> GenerateCorrectOrder([FromBody] List<string> callNumbers)
        {
            var correctOrder = await _quizService.GenerateCorrectOrder(callNumbers);
            return Ok(correctOrder);
        }

    }
}
