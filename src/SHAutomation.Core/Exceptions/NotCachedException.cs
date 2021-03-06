﻿using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace SHAutomation.Core.Exceptions
{
    [Serializable]
    public class NotCachedException : SHAutomationException
    {
        public NotCachedException()
        {
        }

        public NotCachedException(string message)
            : base(message)
        {
        }

        public NotCachedException(Exception innerException) :
            base(string.Empty, innerException)
        {
        }

        public NotCachedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        protected NotCachedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
