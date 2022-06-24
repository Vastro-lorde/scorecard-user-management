using Microsoft.EntityFrameworkCore;
using scorecard_user_mgt.Data;
using scorecard_user_mgt.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace scorecard_user_mgt.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly DbSet<User> _dbSet;
        public UserRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<User>();
        }
        public async Task<List<User>> CreateUserAsync(User user)
        {
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
            return await _dbContext.Users.ToListAsync();
        }
        public async Task<bool> Delete(User request)
        {
            _dbSet.Remove(request);
            return await _dbContext.SaveChangesAsync() > 0;
        }
        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _dbContext.Users.ToListAsync();
        }
        public async Task<User> GetUserByIdAsync(string id)
        {
            var user = await _dbContext.Users.FindAsync(id);
            return user;
        }
        public async Task<bool> UpdateUserAsync(User request)
        {
            _dbSet.Update(request);
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}