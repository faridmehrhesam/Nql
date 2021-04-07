using Nql.Abstractions.Enums;

namespace Nql.Abstractions
{
    public class Diagnostic
    {
        public Diagnostic(string message, NqlNodeLocation location, DiagnosticsLevel level)
        {
            Message = message;
            Location = location;
            Level = level;
        }

        public string Message { get; }

        public NqlNodeLocation Location { get; }

        public DiagnosticsLevel Level { get; }
    }
}