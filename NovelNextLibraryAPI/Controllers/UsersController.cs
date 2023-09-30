using Microsoft.AspNetCore.Mvc;
using NovelNestLibraryAPI.Models;
using NovelNestLibraryAPI.Services;

namespace NovelNestLibraryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserService _UserService;

        public UsersController(UserService UserService) =>
            _UserService = UserService;

        [HttpGet]
        public async Task<List<Users>> Get() =>
            await _UserService.GetAsync();

        // POST api/user/signup
        [HttpPost("signup")]
        public async Task<IActionResult> Signup([FromBody] Users user)
        {
            try
            {
                if (user == null)
                {
                    return BadRequest(new { message = "User data is missing" });
                }


                // Check if the username or email is already in use
                if (await _UserService.IsUsernameOrEmailTakenAsync(user.Username, user.Email))
                {
                    return BadRequest(new { message = "Username or email already in use" });
                }

                await Post(user);

                // Return a success message
                return Ok(new { message = "User registered successfully" });
            }
            catch (Exception ex)
            {
                // Log the exception for debugging
                // You may want to return a more user-friendly error message
                return StatusCode(500, new { message = "Internal server error" });
            }
        }

        // POST api/user/login
        [HttpPost("login")]
        public async Task<Users?> Login([FromBody] LoginRequestModel model)
        {
            try
            {
                if (model != null)
                {
                    BadRequest(new { message = "Login data is missing" });
                    return null;
                }

                var user = await _UserService.FindUserAsync(model.Email, model.Password);

                if (user == null)
                {
                    BadRequest(new { message = "Account Not Found" });
                    return null;
                }

                return user;
            }
            catch (Exception ex)
            {
                StatusCode(500, new { message = "Internal server error" });
                return null;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Users newUsers)
        {
            await _UserService.CreateAsync(newUsers);

            return CreatedAtAction(nameof(Get), new { id = newUsers.Id }, newUsers);
        }

    }
}
