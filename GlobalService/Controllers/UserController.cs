using Microsoft.AspNetCore.Mvc;
using GlobalService.Utilities;
using GlobalService.IServices;
using GlobalService.DTO;
using System;
using Microsoft.Extensions.Logging;
using System.Net.Http;

namespace GlobalService.Controllers
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

        [HttpPut]
        [Authorize]
        public ActionResult<UserDTO> UpdateUser(UpdateUserDTO user)
        {
            var contextUser = (UserDTO)HttpContext.Items["user"];
            var newUser = _userService.UpdateUser(new UserDTO()
            {
                UserId = contextUser.UserId,
                Username = user.Username,
                Name = user.Name,
                Surname = user.Surname
            });

            return Ok(newUser);
        }
    }
}