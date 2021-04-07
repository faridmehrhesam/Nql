using Antlr4.Runtime;
using Nql.Abstractions;
using Nql.Abstractions.Nodes;
using Nql.Internals;
using Nql.Internals.Grammers;
using Nql.Internals.ParserVisitors;

namespace Nql
{
    public class NqlExpressionParser : INqlExpressionParser
    {
        public NqlNode Parse(string nqlString, Diagnostics diagnostics)
        {
            var parserVisitor = new NqlParserVisitor();
            var inputStream = string.IsNullOrWhiteSpace(nqlString)
                ? new AntlrInputStream()
                : new AntlrInputStream(nqlString);
            var lexer = new NqlLexer(inputStream);
            var parser = new NqlParser(new CommonTokenStream(lexer));

            lexer.RemoveErrorListeners();
            parser.RemoveErrorListeners();

            lexer.AddErrorListener(new LexerErrorListener(diagnostics));
            parser.AddErrorListener(new ParserErrorListener(diagnostics));

            return parserVisitor.Visit(parser.compileUnit());
        }
    }
}