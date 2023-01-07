using System.Diagnostics;
using Antlr4.Runtime;

namespace SyntaxTest {
	internal class Program {
		static void Main(string[] args) {
			string file = @"Test.txt";
			ErrorListner errorListner = new ErrorListner();
			using TextReader reader = new StreamReader(file);
			AntlrInputStream stream = new AntlrInputStream(reader);
			FusionLexer lexer = new FusionLexer(stream);
			lexer.RemoveErrorListeners();
			lexer.AddErrorListener(errorListner);
			CommonTokenStream tokens = new CommonTokenStream(lexer);
			FusionParser parser = new FusionParser(tokens);
			parser.RemoveErrorListeners();
			parser.AddErrorListener(errorListner);
			FusionParser.FusionProgramContext fusionProgram = parser.fusionProgram();
			Log($"Parsed with {errorListner.ErrorCount} syntax errors");

			if(errorListner.ErrorCount == 0) {
				Log(File.ReadAllText(file));
				Log(fusionProgram.ToStringTree(parser));
			}
		}

		public static void Log(string text) {
			Debug.WriteLine(text);
			Console.WriteLine(text);
		}
	}
}