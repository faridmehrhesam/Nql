parser grammar NqlParser;

options {
	tokenVocab = NqlLexer;
}

compileUnit: nql EOF;

nql: (SEPARATOR nqlExpression)+;

nqlExpression: takeExpression # take | skipExpression # skip;

takeExpression: TAKE_IDENTIFIER integer;

skipExpression: SKIP_IDENTIFIER integer;

integer: (PLUS | MINUS)? INTEGER;