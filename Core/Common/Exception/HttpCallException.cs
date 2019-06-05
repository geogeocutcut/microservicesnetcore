using System;

namespace Core.Common
{
    public class HttpCallException:Exception
    {
        public HttpCallException(string message) : base(message)
        {
        }
    }
}