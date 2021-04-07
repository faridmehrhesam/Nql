parser grammar NqlParser;

options {
	tokenVocab = NqlLexer;
}

compileUnit: nql EOF;

nql: (SEPARATOR nqlExpression)+;

nqlExpression:
	selectExpression	# select
	| skipExpression	# skip
	| takeExpression	# take;

selectExpression:
	SELECT_IDENTIFIER selectField (COMMA selectField)*;

skipExpression: SKIP_IDENTIFIER integer;

takeExpression: TAKE_IDENTIFIER integer;

selectField: value AS Name = WORD;

value: field | integer;

integer: (PLUS | MINUS)? INTEGER;

field: WORD (DOT WORD)*;