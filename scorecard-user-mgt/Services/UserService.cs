using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using scorecard_user_mgt.DTOs;
using scorecard_user_mgt.Interfaces;
using scorecard_user_mgt.Models;
using scorecard_user_mgt.Repositories;
using scorecard_user_mgt.Utilities;
using scorecard_user_mgt.Utilities.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace scorecard_user_mgt.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, UserManager<User> userManager, IMapper mapper )
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _userManager = userManager;
        }
        public async Task<Response<UserDetailResponseDTO>> GetUserByIdAsync(string userId)
        {
            User user = await _userRepository.GetUserByIdAsync(userId);

            if (user != null)
            {
                var result = _mapper.Map<UserDetailResponseDTO>(user);
                return new Response<UserDetailResponseDTO>()
                {
                    Data = result,
                    IsSuccessful = true,
                    Message = "Successful",
                    ResponseCode = HttpStatusCode.OK
                };
            }

            throw new ArgumentException("Resourse not found");

        }
        public async Task<UserResponseDto> RegisterAsync(RegistrationDto registrationRequest)
        {
            User user = _mapper.Map<User>(registrationRequest);
            user.UserName = registrationRequest.Email;
            IdentityResult result = await _userManager.CreateAsync(user, registrationRequest.Password);
            if (result.Succeeded)
            {
                var response = _mapper.Map<RegistrationDto>(user);
                var answer = new UserResponseDto
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    Email = user.Email
                };
                return answer;
            }
            string errors = result.Errors.Aggregate(string.Empty, (current, error) => current + (error.Description + Environment.NewLine));
            throw new ArgumentException(errors);
        }
        public async Task<Response<User>> DeleteUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return new Response<User>()
                {
                    Message = "User Not Found",
                    ResponseCode = HttpStatusCode.NoContent,
                    IsSuccessful = false
                };
            }
            user.IsActive = false;
            await _userRepository.Delete(user);
            await _userManager.UpdateAsync(user);

            return new Response<User>()
            {
                Message = "User Successfully Deleted",
                ResponseCode = HttpStatusCode.OK,
                IsSuccessful = true
            };
        }
        public async Task<Response<PaginationModel<IEnumerable<GetAllUserResponseDto>>>> GetAllUsersAsync(int pageSize, int pageNumber)
        {
            var users = await _userManager.Users.ToListAsync();
            var response = _mapper.Map<IEnumerable<GetAllUserResponseDto>>(users);

            if (users != null)
            {
                var result = PaginationClass.PaginationAsync(response, pageSize, pageNumber);
                return new Response<PaginationModel<IEnumerable<GetAllUserResponseDto>>>()
                {
                    Data = result,
                    Message = "List of All Users",
                    ResponseCode = HttpStatusCode.OK,
                    IsSuccessful = true
                };
            }
            return new Response<PaginationModel<IEnumerable<GetAllUserResponseDto>>>()
            {
                Data = null,
                Message = "No Registered Users",
                ResponseCode = HttpStatusCode.NoContent,
                IsSuccessful = false
            };
        }
        public async Task<Response<string>> UpdateUserDetails(string Id, UpdateUserDto updateUserDto)
        {
            var user = await _userRepository.GetUserByIdAsync(Id);

            if (user != null)
            {
                user.FirstName = updateUserDto.FirstName;
                user.LastName = updateUserDto.LastName;
                user.PhoneNumber = updateUserDto.PhoneNumber;
                user.Address = updateUserDto.Address;
                var result = await _userRepository.UpdateUserAsync(user);

                if (result)
                {
                    return new Response<string>()
                    {
                        IsSuccessful = true,
                        Message = "Profile updated",
                        ResponseCode = HttpStatusCode.OK
                    };
                }
                return new Response<string>()
                {
                    IsSuccessful = false,
                    Message = "Profile not updated",
                    ResponseCode = HttpStatusCode.BadRequest
                };
            }
            throw new ArgumentException("User not found");
        }
    }   
}