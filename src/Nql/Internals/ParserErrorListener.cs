using Antlr4.Runtime;
using Nql.Abstractions;

namespace Nql.Internals
{
    internal class ParserErrorListener : IAntlrErrorListener<IToken>
    {
        private readonly Diagnostics diagnostics;

        internal ParserErrorListener(Diagnostics diagnostics)
        {
            this.diagnostics = diagnostics;
        }

        public void SyntaxError(
            IRecognizer recognizer,
            IToken offendingSymbol,
            int line,
            int charPositionInLine,
            string msg,
            RecognitionException e)
        {
            diagnostics.AddError(msg, new NqlNodeLocation(line, charPositionInLine, 0, 0));
        }
    }
}