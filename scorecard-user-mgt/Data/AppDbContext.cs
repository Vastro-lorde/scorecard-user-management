using Microsoft.EntityFrameworkCore;
using scorecard_user_mgt.Models;

namespace scorecard_user_mgt.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
    }
}
