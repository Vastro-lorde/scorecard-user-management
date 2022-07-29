using scorecard_user_mgt.DTOs;
using System.Threading.Tasks;

namespace scorecard_user_mgt.Interfaces
{
    public interface IConfirmationMailService
    {
        Task SendAConfirmationEmail(UserResponseDto user, string templatefile);
    }
}