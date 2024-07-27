using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace JiraAppPractice.Services.Exceptions
{
    public class ItemNotFound : BaseApplicationException
    {
        public ItemNotFound() : base("Item not found!", HttpStatusCode.NotFound)
        {
            
        }
    }
}
