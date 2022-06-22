using scorecard_user_mgt.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace scorecard_user_mgt.Interfaces
{
    public interface IUserService
    {
        Task<User> GetUserByIdAsync(int id);
        Task<List<User>> UpdateUserAsync(User request);
        Task<List<User>> GetAllUsersAsync();
        Task<List<User>> DeleteUserAsync(int id);
        Task<List<User>> CreateUserAsync(User user);
    }
}
