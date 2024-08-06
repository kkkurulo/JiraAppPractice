using Microsoft.AspNetCore.Identity;

namespace JiraAppPractice.Data.Models;

public class User : IdentityUser
{
    public ICollection<Tasks> Tasks { get; set; }

}