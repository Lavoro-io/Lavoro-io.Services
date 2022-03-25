using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Saml;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GlobalService.DAL;
using GlobalService.DTO;
using GlobalService.IServices;
using Microsoft.Extensions.Logging;
using System;

namespace GlobalService.Controllers
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

        [HttpGet("ping")]
        public ActionResult Ping()
        {
            return Ok("pong");
        }

        [HttpPost("token")]
        public ActionResult<AuthDTO> GetToken(LoginFormDTO loginForm)
        {
            var result = _userService.GetToken(loginForm);

            return Ok(result);
        }

        [HttpGet("saml"), Obsolete] //For future purpose
        public ActionResult SamlRequest(string baseHref, string redirectUri)
        {
            var redirectUrl = _userService.SamlRequest(baseHref, redirectUri);

            //redirect the user to the SAML provider
            return Redirect(redirectUrl);
        }

        [HttpPost, Obsolete] //For future purpose
        public ActionResult SamlConsume()
        {
            var auth = _userService.SamlResponse(Request);

            Response.Cookies.Append("Authorization", "Bearer " + auth.Token);

            return Ok();
        }

        [HttpPost("register")]
        public ActionResult RegisterUser(RegisterFormDTO registerForm)
        {
            var added = _userService.RegisterUser(registerForm);

            return Ok(added);
        }

        [HttpPost(nameof(NotificationFromDevOps))]
        public ActionResult NotificationFromDevOps(object json)
        {
            _logger.LogInformation(json.ToString());
            return Ok();
        }
    }
}
