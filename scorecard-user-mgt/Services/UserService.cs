using scorecard_user_mgt.Interfaces;
using scorecard_user_mgt.Models;
using scorecard_user_mgt.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace scorecard_user_mgt.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<User> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                throw new ArgumentException("Resource not found");
            }
            return user;
        }

        public async Task<List<User>> CreateUserAsync(User user)
        {
            var users = await _userRepository.CreateUserAsync(user);
            return users;
        }

        public async Task<List<User>> DeleteUserAsync(int id)
        {
            var dbUser = await _userRepository.DeleteUserAsync(id);
            if (dbUser == null)
            {
                throw new ArgumentException("Resource not found");
            }
            var users = await _userRepository.GetAllUsersAsync();
            return users;
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllUsersAsync();
        }

        public async Task<List<User>> UpdateUserAsync(User request)
        {
            await _userRepository.UpdateUserAsync(request);
            var users = await _userRepository.GetAllUsersAsync();
            return users;
        }
    }   
    
}
