using Microsoft.AspNetCore.Mvc;
using UserService.Utilities;
using UserService.IServices;
using UserService.DTO;

namespace UserService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
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
        public ActionResult<UserDTO> GetUser(Guid uuid)
        {
            var user = _userService.GetUser(uuid);

            if(user == null) return NotFound("User not found");

            return Ok(user);   
        }
    }
}