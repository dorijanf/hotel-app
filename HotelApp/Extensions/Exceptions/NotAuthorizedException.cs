using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelApp.API.Extensions.Exceptions
{
    public class NotAuthorizedException : Exception
    {
        public NotAuthorizedException(string message) : base(message)
        {

        }
    }
}
