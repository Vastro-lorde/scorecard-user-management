using scorecard_user_mgt.DTOs;
using System.Threading.Tasks;

namespace scorecard_user_mgt.Interfaces
{
    public interface IAuthServices
    {
        Task<Response<string>> EmailConfirmationAsync(ConfirmEmailRequestDTO confirmEmailRequest);
        Task<Response<string>> ForgotPasswordResetAsync(ForgotPasswordDto forgotPasswordDto);
        Task<Response<UserResponseDto>> Login(UserRequestDto userRequestDto);
        Task<Response<RefreshTokenResponse>> RefreshTokenAsync(RefreshTokenRequestDTO token);
        Task<UserResponseDto> RegisterAsync(RegistrationDto registrationRequest);
        Task<Response<string>> ResetPasswordAsync(ResetPasswordDto resetPassword);
        Task<Response<string>> UpdatePasswordAsync(string Id, UpdatePasswordDTO updatePasswordDto);
    }
}