using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebChatApp.Core;
using WebChatApp.Models.Entities;
using WebChatApp.Models.Models.InputModels;
using WebChatApp.Models.Models.OutputModels;
using WebChatApp.ServicesApp;

namespace TestDataBase.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    public class UserController : ControllerBase
    {
        private IUserService _service;

        public UserController(IUserService userService)
        {
            _service = userService;
        }

        // https://localhost:44365/api/user/42
        /// <summary>Get info of user</summary>
        /// <param name="userId">Id of user</param>
        /// <returns>Info of user</returns>
        [ProducesResponseType(typeof(UserOutputDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpGet("{userId}")]
        //[Authorize]
        public async Task<IActionResult> GetUser(int userId)
        {
            var user = await _service.GetUserById(userId);

            if (user == null)
            {
                return NotFound($"User with id {userId} is not found");
            }

            return Ok(user);
        }
        [HttpGet("current")]
        //[Authorize]
        public ActionResult<UserOutputDto> GetCurrentUser()
        {
            var userId = Convert.ToInt32(User.FindFirst("id").Value);
            //var user = _service.GetUserById(userId);
            return Ok();
        }
        // https://localhost:/api/user/register
        /// <summary>user registration</summary>
        /// <param name="inputModel">information about registered user</param>
        /// <returns>rReturn information about added user</returns>
        [ProducesResponseType(typeof(UserOutputDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [HttpPost("register")]
        //[Authorize(Roles = "Администратор, Менеджер")]
        public ActionResult<UserOutputDto> Register([FromBody] UserEntity inputModel)
        {

            //var userDto = _mapper.Map<User>(inputModel);
            var id = _service.AddUser(inputModel);
            // var user = _service.GetUserById(id);
            //var outputModel = _mapper.Map<UserOutputDto>();
            return Ok();

        }

        // [HttpPost("login")]
        // public ActionResult Login([FromBody] UserInputDto inputModel) 
        // {
        // 
        // }
        //
        // [HttpPost("logout")]
        // public ActionResult Logout() 
        // {
        // 
        // }

        // https://localhost:/api/user/42
        /// <summary>Update information about user</summary>
        /// <param name="userId">Id of user</param>
        /// /// <param name="inputModel">Nonupdated info about  user </param>
        /// <returns>Updated info about user</returns>
        [ProducesResponseType(typeof(UserOutputDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [HttpPut("{userId}")]
        //[Authorize(Roles = "Администратор, Пользователь")]
        public ActionResult<UserOutputDto> UpdateUserInfo(int userId, [FromBody] UpdateUserInputDto inputModel)
        {

            // var user = _service.GetUserById(userId);
            // if (user == null)
            // {
            //     return NotFound($"User with id {userId} is not found");
            // }
            // if (User.IsInRole("Администратор")
            //     || (User.IsInRole("Пользователь") && user.Roles.Contains(Core.Enums.Role.User)))
            // {
            //     var userDto = _mapper.Map<User>(inputModel);
            //     _service.UpdateUser(userId, userDto);
            //     var outputModel = _mapper.Map<UserOutputDto>(_service.GetUserById(userId));
            //     return Ok(outputModel);
            // }
            ///
            return Forbid("Updated user is not ChatUser");
            //
        }
    }
}
