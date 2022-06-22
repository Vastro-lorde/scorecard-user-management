using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using scorecard_user_mgt.Data;
using scorecard_user_mgt.Interfaces;
using scorecard_user_mgt.Models;
using scorecard_user_mgt.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace scorecard_user_mgt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserRepository userRepository, IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            return Ok(await _userService.GetAllUsersAsync());
        }

        [HttpGet("GetUserById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _userService.GetUserByIdAsync(id));
        }

        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser(User user)
        {
            return Ok(await _userService.CreateUserAsync(user));
        }

        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser(User request)
        {
            return Ok(await _userService.UpdateUserAsync(request));
        }


        [HttpDelete("DeleteUser/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            return Ok(await _userService.DeleteUserAsync(id));
        }
    }
}
