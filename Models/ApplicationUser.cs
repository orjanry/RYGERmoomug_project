using Microsoft.AspNetCore.Identity;

namespace moomug_project.Models;

public class ApplicationUser : IdentityUser
{
    public string Nickname { get; set; } = string.Empty;
}