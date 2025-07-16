using BLL.DTOs;
using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BLL.Services.Identity
{
  public class AuthService : IAuthService
  {
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IConfiguration _config;

    public AuthService(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IConfiguration config)
    {
      _userManager = userManager;
      _signInManager = signInManager;
      _config = config;
    }

    public async Task<AuthResultDTO> RegisterAsync(RegisterDTO model)
    {
      var user = new ApplicationUser
      {
        UserName = model.UserName,
        Email = model.Email,
        FullName = model.FullName
      };

      var result = await _userManager.CreateAsync(user, model.Password);
      if (!result.Succeeded)
      {
        return new AuthResultDTO
        {
          Success = false,
          Errors = result.Errors.Select(e => e.Description)
        };
      }

      // Gán role mặc định
      await _userManager.AddToRoleAsync(user, "USER"); // Chú ý: "USER" phải trùng 100% với role đã seed

      // ✅ Gọi generate token sau khi tạo user
      var token = await GenerateJwtTokenAsync(user);

      return new AuthResultDTO
      {
        Success = true,
        Token = token
      };
    }

    public async Task<AuthResultDTO> LoginAsync(LoginDTO model)
    {
      var user = await _userManager.FindByNameAsync(model.UserNameOrEmail)
              ?? await _userManager.FindByEmailAsync(model.UserNameOrEmail);

      if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
      {
        return new AuthResultDTO
        {
          Success = false,
          Errors = new[] { "Invalid credentials." }
        };
      }

      var token = await GenerateJwtTokenAsync(user);
      return new AuthResultDTO
      {
        Success = true,
        Token = token
      };
    }

    public async Task<UserInfoDTO?> GetCurrentUserAsync(string userId)
    {
      var user = await _userManager.FindByIdAsync(userId);
      if (user == null) return null;

      return new UserInfoDTO
      {
        Id = user.Id.ToString(),
        UserName = user.UserName ?? "",
        Email = user.Email ?? "",
        FullName = user.FullName
      };
    }

    private async Task<string> GenerateJwtTokenAsync(ApplicationUser user)
    {
      var roles = await _userManager.GetRolesAsync(user);
      var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName ?? ""),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email ?? "")
            };

      foreach (var role in roles)
        claims.Add(new Claim(ClaimTypes.Role, role));

      var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
      var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

      var token = new JwtSecurityToken(
          issuer: _config["Jwt:Issuer"],
          audience: _config["Jwt:Audience"],
          claims: claims,
          expires: DateTime.UtcNow.AddHours(2),
          signingCredentials: creds
      );

      return new JwtSecurityTokenHandler().WriteToken(token);
    }
  }
}

