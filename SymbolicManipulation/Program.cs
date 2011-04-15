using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace SymbolicManipulation {
	class Program {
		static void Main(string[] args) {
			string input = "5(3+1)1";

			new Tokenizer(input).Scan()				.AddToLog(LogType.allTokens)
							.ConvertToPostfix()		.AddToLog(LogType.postFixedTokens)
							.Evaluate()				.AddToLog(LogType.output);

			UI.DisplayLog(LogType.allTokens);
			UI.DisplayLog(LogType.output);
		}
	}
}