using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace JiraAppPractice.Services.Exceptions
{
    public class InvalidStatus : BaseApplicationException
    {
        public InvalidStatus():base("Invalid status or impossible transition between them", HttpStatusCode.Forbidden)
        {
        }
    }
}
