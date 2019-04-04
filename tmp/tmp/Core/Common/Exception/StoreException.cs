using System;
namespace Core.Common
{
    public class StoreException : Exception
    {

        public string Error;
        

        public StoreException(string error,string message):base(message)
        {
            Error = error;
        }

        public StoreException(string error,string message,Exception ex):base(message,ex)
        {
            Error = error;
        }
    }
}