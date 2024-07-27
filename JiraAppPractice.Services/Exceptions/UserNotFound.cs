using System.Net;
namespace JiraAppPractice.Services.Exceptions
{
    public class UserNotFound : BaseApplicationException
    {
        public UserNotFound(): base("User not found!", HttpStatusCode.NotFound)
        {
            
        }
    }
}
