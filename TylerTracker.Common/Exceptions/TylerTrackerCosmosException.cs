using System;
using System.Runtime.Serialization;

namespace TylerTracker.Common.Exceptions
{
    public sealed class TylerTrackerCosmosException : TylerTrackerException
    {
        public TylerTrackerCosmosException()
        {
        }

        public TylerTrackerCosmosException(string message) : base(message)
        {
        }

        public TylerTrackerCosmosException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public TylerTrackerCosmosException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
