using System.Net;

namespace JiraAppPractice.Services.Exceptions;

public class BoardNotFound : BaseApplicationException
{
    public BoardNotFound() : base("Board not found!", HttpStatusCode.NotFound)
    {
    }
}
