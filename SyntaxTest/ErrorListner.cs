using Antlr4.Runtime;
using Antlr4.Runtime.Misc;

namespace SyntaxTest {
	internal class ErrorListner : BaseErrorListener, IAntlrErrorListener<int> {
		public int ErrorCount { get; private set; }
		public override void SyntaxError([NotNull] IRecognizer recognizer, [Nullable] IToken offendingSymbol, int line, int charPositionInLine, [NotNull] string msg, [Nullable] RecognitionException e) {
			this.ErrorCount++;
			Program.Log($"({line}, {charPositionInLine + 1}) syntax error: {msg}");
		}

		public void SyntaxError([NotNull] IRecognizer recognizer, [Nullable] int offendingSymbol, int line, int charPositionInLine, [NotNull] string msg, [Nullable] RecognitionException e) {
			this.ErrorCount++;
			Program.Log($"({line}, {charPositionInLine + 1}) syntax error: {msg}");
		}
	}
}
