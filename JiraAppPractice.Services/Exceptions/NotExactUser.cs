using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace JiraAppPractice.Services.Exceptions
{
    public class NotExactUser : BaseApplicationException
    {
        public NotExactUser() : base("Not an exact user", HttpStatusCode.Locked)
        {
        }
    }
}
