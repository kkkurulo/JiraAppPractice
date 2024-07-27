using System.Net;

namespace JiraAppPractice.Services.Exceptions;

public class BaseApplicationException : Exception
{ 
    public HttpStatusCode StatusCode { get; }

    public BaseApplicationException(string message, HttpStatusCode statusCode) : base(message)
    {
        StatusCode = statusCode;
    }
}
