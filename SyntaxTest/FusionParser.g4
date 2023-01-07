parser grammar FusionParser;

options {
	tokenVocab = FusionLexer;
}


fusionProgram: (binaryDecalration | include | macro)* EOF;

binaryDecalration: Binary outputBitsPerNumber;
outputBitsPerNumber: NumberLiteral;

include: Include filePath;
filePath: StringLiteral;

macro: Atomic? Macro macroName parameterList? Begin macroBody End;
macroName: name;
parameterList: parameterName (Comma parameterName)*;
parameterName: name;

macroBody: exprList;
exprList: (label | expr)*;

label: labelName Colon;
labelName: name;

expr: If Open cond=expr Close Begin trueBranch=exprList End (Else Begin falseBranch=exprList End)? #If
	| macroName arguments				#Call
	| Open expr Close					#ParenExpr
	| (Add | Not) expr					#Unary
	| left=expr op=Mul right=expr		#Bin
	| left=expr op=Add right=expr		#Bin
	| left=expr op=BitShift right=expr	#Bin
	| left=expr op=Compare right=expr	#Bin
	| left=expr op=Equality right=expr	#Bin
	| left=expr op=BitAnd right=expr	#Bin
	| left=expr op=BitXor right=expr	#Bin
	| left=expr op=BitOr right=expr		#Bin
	| left=expr op=BoolAnd right=expr	#Bin
	| left=expr op=BoolOr right=expr	#Bin
	| Print expr						#Print
	| (parameterName | labelName)		#LocalName
	| NumberLiteral						#Literal
	| StringLiteral						#Literal
	;

arguments: expr (Comma expr)*;
name: (Identifier | Atomic | Macro | Binary | Include);
