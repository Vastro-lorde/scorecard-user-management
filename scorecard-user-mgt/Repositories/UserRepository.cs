using Microsoft.EntityFrameworkCore;
using scorecard_user_mgt.Data;
using scorecard_user_mgt.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace scorecard_user_mgt.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _dbContext;
        public UserRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<User>> CreateUserAsync(User user)
        {
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<List<User>> DeleteUserAsync(int id)
        {
            var dbUser = await _dbContext.Users.FindAsync(id);
            _dbContext.Users.Remove(dbUser);
            await _dbContext.SaveChangesAsync();
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            var user = await _dbContext.Users.FindAsync(id);
            return user;
        }

        public async Task<List<User>> UpdateUserAsync(User request)
        {
            var dbUser = await _dbContext.Users.FindAsync(request.Id);
            if (dbUser == null)
            {
                throw new ArgumentException("Resource not found");
            }
            dbUser.FirstName = request.FirstName;
            dbUser.LastName = request.LastName;
            dbUser.PhoneNumber = request.PhoneNumber;
            dbUser.Email = request.Email;

            await _dbContext.SaveChangesAsync();

            return await _dbContext.Users.ToListAsync();
        }
    }
}
