using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace CRM.API.Areas.PublicArea.Controllers
{
    [Area("PublicArea")]
    [DisplayName("Public Controller")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    public class PublicController : Controller
    {

        public class AuthenticationController(IAuthenticationService authenticationService) : ControllerBase
        {
            private readonly IAuthenticationService authenticationService = authenticationService;  
            [HttpPost("login")]
            [DisplayName("Login")]
            public async Task<IActionResult> Login([FromBody] ApplicationUserLoginInputModel model)
            {
                var response = await authenticationService.LoginAsync(model);
                return response.IsSuccess ? Ok(response) : BadRequest(response);
            }

            [HttpPost("register")]
            [DisplayName("Register")]
            public async Task<IActionResult> Register([FromBody] ApplicationUserRegisterInputModel model)
            {
                var response = await authenticationService.RegisterAsync(model);
                return response ? Ok(response) : StatusCode(500);
            }

            [HttpPost("forgot-password")]
            [DisplayName("Forgot Password")]
            public async Task<IActionResult> ForgotPassword([FromBody] ApplicationUserRegisterInputModel model)
            {
                var response = await authenticationService.ForgotPasswordAsync(model);
                return response ? Ok(response) : StatusCode(500);
            }

            [HttpPost("reset-password")]
            [DisplayName("Reset Password")]
            public async Task<IActionResult> ResetPassword([FromBody] ApplicationUserRegisterInputModel model)
            {
                var response = await authenticationService.ResetPasswordAsync(model);
                return response ? Ok(response) : StatusCode(500);
            }

            [HttpPost("change-password")]
            [DisplayName("Change Password")]
            public async Task<IActionResult> ChangePassword([FromBody] ApplicationUserRegisterInputModel model)
            {
                var response = await authenticationService.ChangePasswordAsync(model);
                return response ? Ok(response) : StatusCode(500);
            }

            [HttpPost("refresh-token")]
            [DisplayName("Refresh Token")]
            public async Task<IActionResult> RefreshToken([FromBody] ApplicationUserRegisterInputModel model)
            {
                var response = await authenticationService.RefreshTokenAsync(model);
                return response ? Ok(response) : StatusCode(500);
            }
        }
    }
}
