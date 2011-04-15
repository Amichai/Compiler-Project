using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace SymbolicManipulation {
	class Program {
		static void Main(string[] args) {
			string input = "5 + ((1 + 2*2*3 / 14) * 4^2) - 3 * 2";
			//"5 + 3/(2 / ((4 - 1) * 2))"	.AddToLog(LogType.input);

			//input = AddParenthesis(input);
			//Debug.Print(input);

			//TODO: Teach the tokenizer to handle "^"
			//TODO: BUG:
			//   "5 + 3/2 / (5 - 1) - 2/-7 + (3 - 2 / -5) + -1"
			// is being read as:
			//	"5 + 3/(2 / (5 - 1)) - 2/-7 + (3 - 2 / -5) + -1"
			// I don't know why

			new Tokenizer(input).Scan()
							.ConvertToPostfix()		.AddToLog(LogType.allTokens)
							.Evaluate()				.AddToLog(LogType.output);

			/*
			new Tokenizer(input)
					.Scan().AddToLog(LogType.allTokens)
					.parseTree
					.Evaluate().AddToLog(LogType.output);
										
										UI.DisplayLog(LogType.input);
										UI.DisplayLog(LogType.parseTree);
										UI.DisplayLog(LogType.allTokens);
										UI.DisplayLog(LogType.output);
			 */
			UI.DisplayLog(LogType.allTokens);
			UI.DisplayLog(LogType.output);
		}
	}
}