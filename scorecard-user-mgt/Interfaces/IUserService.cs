using scorecard_user_mgt.DTOs;
using scorecard_user_mgt.Models;
using scorecard_user_mgt.Utilities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace scorecard_user_mgt.Interfaces
{
    public interface IUserService
    {
        Task<Response<UserDetailResponseDTO>> GetUserByIdAsync(string userId);
        Task<Response<string>> UpdateUserDetails(string Id, UpdateUserDto updateUserDto);
        Task<Response<PaginationModel<IEnumerable<GetAllUserResponseDto>>>> GetAllUsersAsync(int pageSize, int pageNumber);
        Task<Response<User>> DeleteUserAsync(string userId);
        Task<UserResponseDto> RegisterAsync(RegistrationDto registrationRequest);
    }
}