using Microsoft.AspNetCore.Identity;

namespace DAL.Entities
{
    public class ApplicationRole : IdentityRole<string>
    {
      public ApplicationRole()
        {
            Id = Guid.NewGuid().ToString();
        }

        public ApplicationRole(string roleName) : base(roleName)
        {
            Id = Guid.NewGuid().ToString();
        }

        public string? Description { get; set; }
    }
}

