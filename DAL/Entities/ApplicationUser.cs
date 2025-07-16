using Microsoft.AspNetCore.Identity;

namespace DAL.Entities
{
  public class ApplicationUser : IdentityUser<string>
  {
    public ApplicationUser()
    {
      Id = Guid.NewGuid().ToString();
    }
    public string FullName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public bool IsActive { get; set; } = true;
  }
}

