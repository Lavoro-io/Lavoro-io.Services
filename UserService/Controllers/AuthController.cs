using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserService.DAL;
using UserService.DTO;
using UserService.IServices;

namespace UserService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IUserService _userService;

        public AuthController(ILogger<AuthController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpPost("token")]
        public ActionResult<AuthDTO> GetToken(LoginFormDTO loginForm)
        {
            var result = _userService.GetToken(loginForm);

            return Ok(result);
        }

        [HttpPost("register")]
        public ActionResult RegisterUser(RegisterFormDTO registerForm)
        {
            var added = _userService.RegisterUser(registerForm);

            return Ok(added);
        }
    }
}
