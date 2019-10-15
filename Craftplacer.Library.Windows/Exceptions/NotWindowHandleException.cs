#pragma warning disable CA1032 // Implement standard exception constructors
#pragma warning disable CA2229 // Implement serialization constructors

using System;

namespace Craftplacer.Library.Windows.Exceptions
{
    [Serializable]
    public class NotWindowHandleException : Exception
    {
        public NotWindowHandleException(string message) : base(message)
        {
        }

        public NotWindowHandleException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}