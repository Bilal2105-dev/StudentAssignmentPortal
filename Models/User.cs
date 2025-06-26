using Microsoft.AspNetCore.Identity;

public class User : IdentityUser<int>
{
    public string? FullName { get; set; }
}