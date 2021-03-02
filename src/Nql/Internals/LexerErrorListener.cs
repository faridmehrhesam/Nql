using Antlr4.Runtime;
using Nql.Abstractions;

namespace Nql.Internals
{
    internal class LexerErrorListener : IAntlrErrorListener<int>
    {
        private readonly Diagnostics diagnostics;

        internal LexerErrorListener(Diagnostics diagnostics)
        {
            this.diagnostics = diagnostics;
        }

        public void SyntaxError(
            IRecognizer recognizer,
            int offendingSymbol,
            int line,
            int charPositionInLine,
            string msg,
            RecognitionException e)
        {
            diagnostics.AddError(msg, new NqlNodeLocation(line, charPositionInLine, 0, 0));
        }
    }
}