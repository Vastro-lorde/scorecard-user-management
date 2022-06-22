using scorecard_user_mgt.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace scorecard_user_mgt.Repositories
{
    public interface IUserRepository
    {  
        Task<List<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(int id);
        Task<List<User>> CreateUserAsync(User user);
        Task<List<User>> UpdateUserAsync(User request);
        Task<List<User>> DeleteUserAsync(int id);
    }
}
