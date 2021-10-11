using WebChatApp.ServicesApp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebChatApp.Models.Models;
using WebChatApp.Models.Models.InputModels;
using WebChatApp.Core;
using System.Threading.Tasks;

namespace TestDataBase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private IAuthenticationService _service;
        private ISecurityService _securityService;
        public AuthenticationController(IAuthenticationService authenticationService, ISecurityService securityService)
        {
            _service = authenticationService;
            _securityService = securityService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(AuthResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AuthResponse>> Authentificate([FromBody] AuthenticationInputDto login)
        {
            var user = await _service.GetAuthentificatedUser(login.Login);
            if (user != null && _securityService.VerifyHashAndPassword(user.Password, login.Password))
            {
                var token = _service.GenerateToken(user);
                return Ok(token);
            }
            return NotFound("Wrong credentials");
        }
    }
}
