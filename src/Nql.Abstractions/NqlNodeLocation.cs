namespace Nql.Abstractions
{
    public class NqlNodeLocation
    {
        public NqlNodeLocation(int startLine, int startColumn, int stopLine, int stopColumn)
        {
            StartLine = startLine;
            StartColumn = startColumn;
            StopLine = stopLine;
            StopColumn = stopColumn;
        }

        public int StartLine { get; }

        public int StartColumn { get; }

        public int StopLine { get; }

        public int StopColumn { get; }

        public override string ToString()
        {
            return $"{StartLine}:{StartColumn} -> {StopLine}:{StopColumn}";
        }
    }
}