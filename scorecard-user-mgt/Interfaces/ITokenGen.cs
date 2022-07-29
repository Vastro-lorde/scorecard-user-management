using scorecard_user_mgt.Models;

namespace scorecard_user_mgt.Interfaces
{
    public interface ITokenGen
    {
        string GenerateRefreshToken();
        string GenerateToken(User user);
    }
}