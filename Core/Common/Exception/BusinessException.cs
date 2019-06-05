using System;

namespace Core.Common
{
    public class BusinessException : Exception
    {

        public string Error;
        

        public BusinessException(string error,string message):base(message)
        {
            Error = error;
        }
    }
}