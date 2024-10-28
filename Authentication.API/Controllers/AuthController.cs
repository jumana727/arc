using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Arc.Common.Enums;
using Arc.Common.Models;
using Authentication.API.Models;
using Authentication.Application.DTOs;
using Authentication.Application.Interfaces;
using Logging.Core;

namespace Authentication.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {

        #region Declaration

        private readonly IAuthService authservice;

        #endregion

        #region Ctor

        public AuthController(IAuthService _authservice)
        {
            authservice = _authservice;
        }

        #endregion

        #region Actions

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] LoginRequest request)
        {
            Logger.LogMessage("AuthApi", "Information", "Login method of controller called");
            DataModel<AuthResponse> response = await authservice.VerifyUser(new AuthRequest() { GrantType = "password", ClientId = "myclient", UserName = request.UserName, Password = request.Password });
            Logger.LogMessage("AuthApi", "Information", "response  " + response);
            Console.WriteLine("response = " + response);
            if (response != null && response.Status == HttpStatusCode.OK)
            {
                Logger.LogMessage("AuthApi", "Information", "Login Successfull");
                return Ok(response);
            }
            else if (response != null && response.Status == HttpStatusCode.Unauthorized)
            {
                Logger.LogMessage("AuthApi", "Error", "User Unauthorized");
                return Unauthorized(response);
            }
            else
            {
                Logger.LogMessage("AuthApi", "Error", "BadRequest");
                return BadRequest(response);
            }
        }

        // [Authorize]
        [HttpGet("validatetoken")]
        public IActionResult ValidateToken()
        {
            Logger.LogMessage("AuthApi", "Information", "Token is valid!!");
            return Ok(new { message = "Token is valid" });
        }

        #endregion

    }

}