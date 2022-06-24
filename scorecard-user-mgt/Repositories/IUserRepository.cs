using scorecard_user_mgt.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace scorecard_user_mgt.Repositories
{
    public interface IUserRepository
    {  
        Task<List<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(string id);
        Task<List<User>> CreateUserAsync(User user);
        Task<bool> UpdateUserAsync(User request);
        Task<bool> Delete(User request);
    }
}
