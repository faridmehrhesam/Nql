lexer grammar NqlLexer;

// Fragments

fragment A: [aA];
fragment B: [bB];
fragment C: [cC];
fragment D: [dD];
fragment E: [eE];
fragment F: [fF];
fragment G: [gG];
fragment H: [hH];
fragment I: [iI];
fragment J: [jJ];
fragment K: [kK];
fragment L: [lL];
fragment M: [mM];
fragment N: [nN];
fragment O: [oO];
fragment P: [pP];
fragment Q: [qQ];
fragment R: [rR];
fragment S: [sS];
fragment T: [tT];
fragment U: [uU];
fragment V: [vV];
fragment W: [wW];
fragment X: [xX];
fragment Y: [yY];
fragment Z: [zZ];

SEPARATOR: '|';

// Identifiers

TAKE_IDENTIFIER: T A K E;

SKIP_IDENTIFIER: S K I P;

// Signs

PLUS: '+';

MINUS: '-';

// Constants

INTEGER: [0-9]+;

// Channels

WHITESPACE: [ \t\r\n] -> channel(HIDDEN);