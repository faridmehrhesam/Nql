using System;
using System.Runtime.Serialization;
using Nql.Abstractions;

namespace Nql.Exceptions
{
    [Serializable]
    public class NqlParseException : Exception
    {
        public NqlParseException(Diagnostic[] diagnostics)
            : base("Invalid NQL string is provided.")
        {
            Diagnostics = diagnostics;
        }

        protected NqlParseException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public Diagnostic[] Diagnostics { get; }
    }
}