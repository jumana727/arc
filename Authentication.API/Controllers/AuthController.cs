using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Arc.Common.Enums;
using Arc.Common.Models;
using Authentication.API.Models;
using Authentication.Application.DTOs;
using Authentication.Application.Interfaces;

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
            DataModel<AuthResponse> response = await authservice.VerifyUser(new AuthRequest() { GrantType = "password", ClientId = "myclient", UserName = request.UserName, Password = request.Password });

            if (response != null && response.Status == HttpStatusCode.OK)
                return Ok(response);
            else if (response != null && response.Status == HttpStatusCode.Unauthorized)
                return Unauthorized(response);
            else
                return BadRequest(response);
        }

        // [Authorize]
        [HttpGet("validatetoken")]
        public IActionResult ValidateToken()
        {
            return Ok(new { message = "Token is valid" });
        }

        #endregion

    }

}