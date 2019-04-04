using System;

namespace Core.Common
{
    public class ServiceException : Exception
    {

        public string Error;
        

        public ServiceException(string error,string message):base(message)
        {
            Error = error;
        }

        public ServiceException(string error,string message,Exception ex):base(message,ex)
        {
            Error = error;
        }
    }
}