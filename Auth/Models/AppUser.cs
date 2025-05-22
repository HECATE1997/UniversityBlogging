using Microsoft.AspNetCore.Identity;

namespace Auth.Models
{
    public class AppUser : IdentityUser<Guid>
    {
       public string FullName { get; set; } = string.Empty;
    }
}
