using System;
using System.Runtime.Serialization;

// ReSharper disable once CheckNamespace
namespace Core.Common
{
    public class DalException : Exception, ISerializable
    {
        public DalException()
        { }

        public DalException(string message) : base(message)
        { }

        public DalException(string message, Exception innerException) : base(message, innerException)
        { }

        protected DalException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }
}