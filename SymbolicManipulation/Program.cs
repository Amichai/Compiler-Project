using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace SymbolicManipulation {
	class Program {
		static void Main(string[] args) {
			string input =
				"5/2*3 + 4*-9 / 2 + 1"	.AddToLog(LogType.input);
			
			new Tokenizer(input)
					.Scan()						.AddToLog(LogType.allTokens)
					.parseTree.BuildParseTree()	.AddToLog(LogType.parseTree)
					.Evaluate()					.AddToLog(LogType.output);
										
										UI.DisplayLog(LogType.input);
										UI.DisplayLog(LogType.parseTree);
										UI.DisplayLog(LogType.allTokens);
										UI.DisplayLog(LogType.output);
		}
	}
}