using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using scorecard_user_mgt.DTOs;
using scorecard_user_mgt.Interfaces;
using scorecard_user_mgt.Models;
using System;
using System.Threading.Tasks;

namespace scorecard_user_mgt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IImageService _imageService;
        private readonly UserManager<User> _userManager;
        public UserController(IUserService userService, IImageService imageService, UserManager<User> userManager)
        {
            _userService = userService;
            _imageService = imageService;
            _userManager = userManager;
        }

        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers(int pageSize, int pageNumber)
        {
            var response = await _userService.GetAllUsersAsync(pageSize, pageNumber);
            return StatusCode((int)response.ResponseCode, response);
        }

        [HttpGet("GetUserById/{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                return Ok(await _userService.GetUserByIdAsync(id));
            }
            catch (ArgumentException argex)
            {
                return BadRequest(argex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser(RegistrationDto registrationRequest)
        {
            return Ok(await _userService.RegisterAsync(registrationRequest));
        }

        [HttpPut("UpdateUserBy/{id}")]
        public async Task<IActionResult> UpdateUser(string id, UpdateUserDto updateUserdDto)
        {
            try
            { 
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (ModelState.IsValid)
                {
                    var result = await _userService.UpdateUserDetails(id, updateUserdDto);
                    return Ok(result);
                }
                return BadRequest(ModelState);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Try again after 5 minutes");
            }
        }

        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> DeleteAUser(string userId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var deletedUser = await _userService.DeleteUserAsync(userId);
            return StatusCode((int)deletedUser.ResponseCode, deletedUser);
        }

        [HttpPatch("Id/UploadImage")]
        public async Task<IActionResult> UploadImage(string Id, [FromForm] AddImageDto imageDto)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(Id);

                if (user != null)
                {
                    var upload = await _imageService.UploadAsync(imageDto.Image);
                    var result = new ImageAddedDto()
                    {
                        PublicId = upload.PublicId,
                        Url = upload.Url.ToString()
                    };

                    user.Avatar = result.Url;
                    user.PublicId = upload.PublicId;
                    await _userManager.UpdateAsync(user);
                    return Ok(result);
                }
                return NotFound("User not found");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}