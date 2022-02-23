using Microsoft.AspNetCore.Mvc;
using UserService.Utilities;
using UserService.IServices;
using UserService.DTO;

namespace UserService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpGet]
        [Authorize]
        public ActionResult<UserDTO> GetUser(Guid uuid)
        {
            var user = _userService.GetUser(uuid);

            if(user == null) return NotFound("User not found");

            return Ok(user);   
        }

        [HttpGet("activate/{token}")]
        public ActionResult RegisterUser(string token, string redirectUri)
        {
            var user = AppExstension.FromBase64<UserDTO>(token);

            if (user == null) return Problem("Expired Data");

            _userService.AddUser(user);

            using(var httpClient = new HttpClient())
            {
                httpClient.PutAsync("https://lvrio-identityprovider.azurewebsites.net/auth?uuid=" + user.UserId, null);
            }

            return Redirect(redirectUri);
        }

        [HttpPut]
        [Authorize]
        public ActionResult<bool> UpdateUser(UserDTO user)
        {
            var update = _userService.UpdateUser(user);

            return Ok(update);
        }
    }
}