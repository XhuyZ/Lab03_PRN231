using BLL.DTOs;

namespace BLL.Services.Identity
{
    public interface IAuthService
    {
        Task<AuthResultDTO> RegisterAsync(RegisterDTO model);
        Task<AuthResultDTO> LoginAsync(LoginDTO model);
        Task<UserInfoDTO?> GetCurrentUserAsync(string userId);
    }
}

