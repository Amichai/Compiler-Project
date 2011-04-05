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
										UI.DisplayLog(LogType.input);
			new Tokenizer(input)
					.Scan()
					.parseTree.BuildParseTree()
					.Evaluate()			.AddToLog(LogType.output);
										UI.DisplayLog(LogType.output);
			//TODO: Add every data structure to the log
		}
	}
}