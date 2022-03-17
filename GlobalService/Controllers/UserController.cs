using Microsoft.AspNetCore.Mvc;
using GlobalService.Utilities;
using GlobalService.IServices;
using GlobalService.DTO;
using System;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Collections.Generic;
using System.Linq;

namespace GlobalService.Controllers
{
    [Authorize]
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

        [HttpGet(nameof(GetUser))]
        public ActionResult<UserDTO> GetUser(Guid uuid)
        {
            var user = _userService.GetUser(uuid);

            if(user == null) return NotFound("User not found");

            return Ok(user);   
        }

        [HttpGet(nameof(GetUsers))]
        public ActionResult<List<UserDTO>> GetUsers()
        {
            var users = _userService.GetUsers();

            if (!users.Any()) return NotFound("No users found");

            return Ok(users);
        }

        [HttpPut(nameof(UpdateUser))]
        public ActionResult<UserDTO> UpdateUser(UpdateUserDTO user)
        {
            var contextUser = (UserDTO)HttpContext.Items["user"];
            var newUser = _userService.UpdateUser(new UserDTO()
            {
                UserId = contextUser.UserId,
                Username = user.Username,
                Name = user.Name,
                Surname = user.Surname,
                ProfilePicture = user.ProfilePicture,
                BackgroundImage = user.BackgroundImage
            });

            return Ok(newUser);
        }
    }
}