using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace SymbolicManipulation {
	class Program {
		static void Main(string[] args) {
			string input = //Console.ReadLine();
				"5/2*3 + 4*-9 / 2 + 1".AddToLog(LogType.input);

			UserInterface.DisplayLog(LogType.input);
			double returnVal = new Tokenizer(input)
									.Scan()
									.parseTree.BuildParseTree()
									.Evaluate().AddToLog(LogType.output);
			UserInterface.DisplayLog(LogType.output);
			//TODO: Add every data structure possible to the log
		}
	}
}