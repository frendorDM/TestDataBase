using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebChatApp.Models.Models;
using WebChatApp.Models.Models.InputModels;

namespace TestDataBase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {

        public AuthenticationController()
        {
        }

        [HttpPost]
        [ProducesResponseType(typeof(AuthResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public ActionResult<AuthResponse> Authentificate([FromBody] AuthenticationInputDto login)
        {
            //var user = _service.GetAuthentificatedUser(login.Login);
            //if (user != null && _securityService.VerifyHashAndPassword(user.Password, login.Password))
            //{
            //    var token = _service.GenerateToken(user);
            //    return Ok(token);
            //}
            return NotFound("Wrong credentials");
        }
    }
}
