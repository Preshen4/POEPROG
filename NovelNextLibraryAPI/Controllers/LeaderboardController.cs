using Microsoft.AspNetCore.Mvc;
using NovelNestLibraryAPI.Models;
using NovelNestLibraryAPI.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NovelNestLibraryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaderboardController : ControllerBase
    {
        private readonly LeaderboardService _leaderboardService;

        public LeaderboardController(LeaderboardService leaderboardService) =>
            _leaderboardService = leaderboardService;

        [HttpGet]
        public async Task<List<LeaderBoard>> Get() =>
            await _leaderboardService.GetAllLeaderboardEntriesAsync();

        [HttpPost("AddOrUpdateLeaderboardEntry")]
        public async Task<IActionResult> AddOrUpdateLeaderboardEntry([FromBody] LeaderBoard leaderboardEntry)
        {
            if (leaderboardEntry == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check if the user exists in the leaderboard
            var existingEntry = await _leaderboardService.GetLeaderboardEntryAsync(leaderboardEntry.UserName);

            if (existingEntry != null)
            {
                // User exists, update their score
                await _leaderboardService.UpdateLeaderboardEntryAsync(existingEntry.UserName, leaderboardEntry.Score);
                return Ok(existingEntry); // Return 200 OK for updates
            }
            else
            {
                // User does not exist, add a new entry
                await _leaderboardService.AddLeaderboardEntryAsync(leaderboardEntry.UserName, leaderboardEntry.Score);
                return CreatedAtAction(nameof(GetLeaderboardEntry), new { userName = leaderboardEntry.UserName }, leaderboardEntry); // Return 201 Created for new entries
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddLeaderboardEntry([FromBody] LeaderBoard leaderboardEntry)
        {
            if (leaderboardEntry == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _leaderboardService.AddLeaderboardEntryAsync(leaderboardEntry.UserName, leaderboardEntry.Score);
            return CreatedAtAction(nameof(GetLeaderboardEntry), new { userName = leaderboardEntry.UserName }, leaderboardEntry);
        }

        [HttpPut("UpdateLeaderboardEntry")]
        public async Task<IActionResult> UpdateLeaderboardEntry(string userName, int newScore)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _leaderboardService.UpdateLeaderboardEntryAsync(userName, newScore);
            return NoContent();
        }

        [HttpGet("GetLeaderboardEntry")]
        public async Task<IActionResult> GetLeaderboardEntry(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                return BadRequest("Username cannot be empty.");
            }
            var leaderboardEntry = await _leaderboardService.GetLeaderboardEntryAsync(userName);
            if (leaderboardEntry == null)
            {
                return NotFound();
            }
            return Ok(leaderboardEntry);
        }
    }
}
