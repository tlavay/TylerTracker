using System;
using System.Runtime.Serialization;

namespace TylerTracker.Common.Exceptions
{
    public class TylerTrackerException : Exception
    {
        public TylerTrackerException()
        {
        }

        public TylerTrackerException(string message) : base(message)
        {
        }

        public TylerTrackerException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TylerTrackerException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
