using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using scorecard_user_mgt.DTOs;
using scorecard_user_mgt.Interfaces;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace scorecard_user_mgt.Controllers
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthServices _authentication;
        public AuthController(IAuthServices authentication)
        {
            _authentication = authentication;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] UserRequestDto userRequest)
        {
            try
            {
                var response = await _authentication.Login(userRequest);
                return response.IsSuccessful? Ok(response) : BadRequest(response);
                
            }
            catch (AccessViolationException)
            {
                return BadRequest();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [HttpPost]
        [Route("register")]
        public async Task<ActionResult> CreateUserAsync(RegistrationDto userRequest)
        {
            try
            {
                if (!TryValidateModel(userRequest))
                {
                    return BadRequest();
                }
                await _authentication.RegisterAsync((userRequest));
                return Ok();
            }
            catch (ArgumentException argex)
            {
                return BadRequest(argex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occured we are working on it");
            }
        }

        [Authorize]
        [HttpPatch]
        [Route("Update-password")]
        public async Task<IActionResult> UpdatePassword(UpdatePasswordDTO updatePasswordDto)
        {
            var userId = HttpContext.User.FindFirst(user => user.Type == ClaimTypes.NameIdentifier).Value;
            try
            {

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (ModelState.IsValid)
                {
                    var result = await _authentication.UpdatePasswordAsync(userId, updatePasswordDto);
                    return Ok(result);
                }

                return BadRequest(ModelState);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }



        [HttpPost]
        [Route("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmailAsync([FromBody] ConfirmEmailRequestDTO confirmEmailRequestDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var result = await _authentication.EmailConfirmationAsync(confirmEmailRequestDTO);
                if (!result.IsSuccessful)
                {
                    return BadRequest(result);
                }
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured we are working on it");
            }
        }

        [HttpPost]
        [Route("RefreshToken")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequestDTO request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var result = await _authentication.RefreshTokenAsync(request);
                if (!result.IsSuccessful)
                {
                    return BadRequest(result);
                }
                return Ok(result);

            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured we are working on it");
            }
        }

        [HttpPost]
        [Route("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto resetPassword)
        {
            try
            {
                return Ok(await _authentication.ResetPasswordAsync(resetPassword));
            }
            catch (AccessViolationException)
            {
                return BadRequest();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("ForgotPassword")]
        public async Task<IActionResult> ForgotPasswordReset(ForgotPasswordDto forgotPasswordDto)
        {
            try
            {
                return Ok(await _authentication.ForgotPasswordResetAsync(forgotPasswordDto));
            }
            catch (AccessViolationException)
            {
                return BadRequest();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
